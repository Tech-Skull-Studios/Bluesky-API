/*
 BLUESKY API
 HELPERS UTILITY CLASS
 AJAX RECIEVER
 v1.1
 LAST EDITED: SUNDAY FEBRUARY 19, 2023
 COPYRIGHT © TECH SKULL STUDIOS
*/

using Bluesky.Data;
using static Bluesky.Helpers.AJAX;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Bluesky.Helpers
{
    /// <summary>
    /// Used to handle requests sent from AJAX.
    /// </summary>
    class AjaxReceiver : Behaviour
    {
        internal static AjaxReceiver Instance { get; private set; }

        internal delegate UnityWebRequest RequestSender(string uri, string body);
        internal delegate UnityWebRequest RequestReciever(string uri);

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Run<T>(RequestSender sender, string url, string body, SuccessCallback<T> success, ErrorCallback error)
        where T : IResponse
        {
            StartCoroutine(RequestCoroutine(sender, url, body, success, error));
        }

        public void Run<T>(RequestReciever receiver, string url, SuccessCallback<T> success, ErrorCallback error)
        where T : IResponse
        {
            StartCoroutine(RequestCoroutine(receiver, url, success, error));
        }

        public void Run(RequestReciever receiver, string url, SuccessCallbackString success, ErrorCallback error)
        {
            StartCoroutine(RequestCoroutine(receiver, url, success, error));
        }

        private static IEnumerator RequestCoroutine<T>(RequestSender sender, string url, string body, SuccessCallback<T> success, ErrorCallback error)
        where T : IResponse
        {
            Debug.Log(url);
            Debug.Log(body);
            var www = sender?.Invoke(url, body);
            www.certificateHandler = new CertificateWhore();
            www.disposeCertificateHandlerOnDispose = true;
            yield return www.SendWebRequest();
            HandleRequest(www, success, error);
        }

        private static IEnumerator RequestCoroutine<T>(RequestReciever reciever, string url, SuccessCallback<T> success, ErrorCallback error)
        where T : IResponse
        {
            Debug.Log(url);
            var www = reciever?.Invoke(url);
            www.certificateHandler = new CertificateWhore();
            www.disposeCertificateHandlerOnDispose = true;
            //www.SetRequestHeader("Content-Type", ContentType);
            //www.SetRequestHeader("Accept", ContentType);
            //www.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
            //www.SetRequestHeader("Connection", "keep-alive");
            yield return www.SendWebRequest();
            HandleRequest(www, success, error);
        }

        private static IEnumerator RequestCoroutine(RequestReciever reciever, string url, SuccessCallbackString success, ErrorCallback error)
        {
            Debug.Log(url);
            var www = reciever?.Invoke(url);
            www.certificateHandler = new CertificateWhore();
            www.disposeCertificateHandlerOnDispose = true;
            //www.SetRequestHeader("Content-Type", ContentType);
            //www.SetRequestHeader("Accept", ContentType);
            //www.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
            //www.SetRequestHeader("Connection", "keep-alive");
            yield return www.SendWebRequest();
            HandleRequest(www, success, error);
        }

        private static void HandleRequest<T>(UnityWebRequest www, SuccessCallback<T> success, ErrorCallback error)
        where T : IResponse
        {
            HandleRequest(
                www,
                (string response) => ParseResponse(response, success, error),
                error
            );
            return;
            try
            {
                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.responseCode != 200)
                {
                    Debug.LogError(www.error);
                    Debug.Log(www.responseCode);
                    Debug.Log(www.downloadHandler.text);
                    //throw new CodedException((int)www.responseCode, www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    ParseResponse(www.downloadHandler.text, success, error);
                    //LovenseExceptions.CheckForErrors(www.responseCode);
                }
            }
            catch (Exception e)
            {
                error?.Invoke(e.HResult, e.Message);
                //LovenseExceptions.CheckForErrors(www.responseCode);
            }
        }

        private static void HandleRequest(UnityWebRequest www, SuccessCallbackString success, ErrorCallback error)
        {
            try
            {
                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.responseCode != 200)
                {
                    Debug.LogError(www.error);
                    Debug.Log(www.responseCode);
                    Debug.Log(www.downloadHandler.text);
                    //throw new CodedException((int)www.responseCode, www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    //LovenseExceptions.CheckForErrors(www.responseCode);
                    success?.Invoke(www.downloadHandler.text);
                }
            }
            catch (Exception e)
            {
                error?.Invoke(e.HResult, e.Message);
                //LovenseExceptions.CheckForErrors(www.responseCode);
            }
        }

        private static void ParseResponse<T>(string data, SuccessCallback<T> success, ErrorCallback error)
        where T : IResponse
        {
            var parsedData = JsonUtility.FromJson<T>(data/*, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }*/);
            //var d = (LovenseGetToysResponse)(parsedData as IResponseData<GetToysData>);
            
            //Debug.Log("Code: " + d.code);
            //Debug.Log("App Type: " + d.data.appType);
            //Debug.Log("Platform: " + d.data.platform);
            //Debug.Log("Game ID: " + d.data.gameAppId);
            //Debug.Log("Toys: " + d.data.toys);
            //Debug.Log("Type: " + d.type);
            //
            //Debug.Log(d.toyData?.Length);
            //Debug.Log("Name:" + d.toyData[0].name);
            //Debug.Log("ID: " + d.toyData[0].id);
            //Debug.Log("Nickname: " + d.toyData[0].nickName);
            //Debug.Log("Battery: " + d.toyData[0].battery);
            //Debug.Log("Status: " + d.toyData[0].status);

            success?.Invoke(parsedData);
        }
    }
}