/*
 BLUESKY API
 HELPERS UTILITY CLASS
 AJAX
 v1.2
 LAST EDITED: WEDNESDAY FEBRUARY 7, 2024
 COPYRIGHT © TECH SKULL STUDIOS
*/

using Bluesky.Data;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Bluesky.Helpers
{
    /// <summary>
    /// AJAX Controller
    /// </summary>
    public static class AJAX
    {
        public const string ContentType = "application/json";

        public delegate void SuccessCallback(IResponse response);
        public delegate void SuccessCallback<T>(T response) where T : IResponse;
        public delegate void SuccessCallbackString(string response);
        public delegate void ErrorCallback(long error, string message);

        public static void POST<T>(string uri, string body, SuccessCallback<T> success = null, ErrorCallback error = null)
        where T : IResponse
        {
            if (AjaxReceiver.Instance == null)
                new GameObject("@ajax Receiver", typeof(AjaxReceiver));

            AjaxReceiver.Instance.Run((url, body) => {
                var www = new UnityWebRequest(uri, "POST");
                www.method = UnityWebRequest.kHttpVerbPOST;
                www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", ContentType);
                www.SetRequestHeader("Accept", ContentType);
                return www;
            }, uri, body, success, error);
        }

        public static void GET<T>(string uri, SuccessCallback<T> success = null, ErrorCallback error = null)
        where T : IResponse
        {
            if (AjaxReceiver.Instance == null)
                new GameObject("@ajax Receiver", typeof(AjaxReceiver));

            AjaxReceiver.Instance.Run((url) => {
                var www = new UnityWebRequest(uri, "GET");
                www.method = UnityWebRequest.kHttpVerbGET;
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", ContentType);
                www.SetRequestHeader("Accept", ContentType);
                var TOKEN = "";
                www.SetRequestHeader("Authorization", $"Bearer {TOKEN}");
                return www;
            }, uri, success, error);
        }

        public static void GET(string uri, SuccessCallbackString success = null, ErrorCallback error = null)
        {
            if (AjaxReceiver.Instance == null)
                new GameObject("@ajax Receiver", typeof(AjaxReceiver));

            AjaxReceiver.Instance.Run((url) => {
                var www = new UnityWebRequest(uri, "GET");
                www.method = UnityWebRequest.kHttpVerbGET;
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", ContentType);
                www.SetRequestHeader("Accept", ContentType);
                var TOKEN = "";
                www.SetRequestHeader("Authorization", $"Bearer {TOKEN}");
                return www;
            }, uri, success, error);
        }

        public static void PUT<T>(string uri, string body, SuccessCallback<T> success = null, ErrorCallback error = null)
        where T : IResponse
        {
            if (AjaxReceiver.Instance == null)
                new GameObject("@ajax Receiver", typeof(AjaxReceiver));

            AjaxReceiver.Instance.Run<T>(UnityWebRequest.Put, uri, body, success, error);
        }

        public static void DELETE<T>(string uri, SuccessCallback<T> success = null, ErrorCallback error = null)
        where T : IResponse
        {
            if (AjaxReceiver.Instance == null)
                new GameObject("@ajax Receiver", typeof(AjaxReceiver));

            AjaxReceiver.Instance.Run<T>(UnityWebRequest.Delete, uri, success, error);
        }
    }
}