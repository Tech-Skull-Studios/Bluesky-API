/*
 BLUESKY API
 BLUESKY CLASS
 BLUESKY API
 v1.0
 LAST EDITED: WEDNESDAY FEBRUARY 7, 2024
 COPYRIGHT © TECH SKULL STUDIOS
*/

using Bluesky.Data;
using Bluesky.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;

public static class BlueskyAPI
{
    const string BaseURI = "https://bsky.social/xrpc";

    static string GetEndpointURL(string endpoint)
    => $"{BaseURI}/{endpoint}";

    static string GetEndpointURL(string endpoint, Dictionary<string, string> URIs)
    {
        var url = GetEndpointURL(endpoint);
        if(URIs?.Count > 0)
        {
            url += "?";
            foreach(var uri in URIs)
                url += $"{uri.Key}={uri.Value}&";
        }
        return url;
    }

    public static async void Login(LoginSession credentials)
    {
        var content = new StringContent("{\n  \"identifier\": \"thejoblesscoder@gmail.com\",\n  \"password\": \"4849hadTTOSBT\"\n}", null, "application/json");
        var body = await content.ReadAsStringAsync();
        Debug.Log(body);
        var endpoint = GetEndpointURL("com.atproto.server.createSession");
        AJAX.POST(
        endpoint,
        body,
        (LoginResponse r) =>
        {
            Debug.Log(JsonUtility.ToJson(r));
        }
        );
        return;
        var client = new HttpClient();
        var headerVal = Convert.ToBase64String(Encoding.UTF8.GetBytes("admin@client:admin"));
        var header = new AuthenticationHeaderValue("Basic", headerVal);
        client.DefaultRequestHeaders.Authorization = header;
        var request = new HttpRequestMessage(HttpMethod.Post, "https://bsky.social/xrpc/com.atproto.server.createSession");
        request.Headers.Add("Accept", "application/json");
        content = new StringContent("{\n  \"identifier\": \"drakebates\",\n  \"password\": \"$1Mistake\"\n}", null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Debug.Log(await response.Content.ReadAsStringAsync());
    }

    public static void GetPreferences()
    {
        var endpoint = GetEndpointURL("app.bsky.actor.getPreferences");
        AJAX.GET(
        endpoint,
        (string r) =>
        {
            Debug.Log(r);
            //TODO: Parse preferences
        }
        );
    }

    public static void GetProfile(string handle)
    {
        var endpoint = GetEndpointURL("app.bsky.actor.getProfile", new Dictionary<string, string> { { "actor", handle } });
        AJAX.GET(
        endpoint,
        (DetailedProfile r) =>
        {
            Debug.Log(JsonUtility.ToJson(r, true));
        }
        );
    }

    public static void GetProfiles(params string[] handles)
    {
        var actors = "";
        var numActors = Mathf.Min(25, handles.Length);
        for (var i = 0; i < numActors; i++)
        {
            actors += handles[i];
            if((i + 1) < numActors)
                actors += ",";
        }
        var endpoint = GetEndpointURL("app.bsky.actor.getProfiles", new Dictionary<string, string> { { "actors", actors } });
        AJAX.GET(
        endpoint,
        (GetProfilesResponse r) =>
        {
            Debug.Log(JsonUtility.ToJson(r, true));
        }
        );
    }

    public static void GetSuggested()
    {
        var uri = new Dictionary<string, string>();
        if (false)
            uri.Add("limit", Mathf.Clamp(50, 1, 100).ToString());
        if(false)
            uri.Add("cursor", "");
        var endpoint = GetEndpointURL("app.bsky.actor.getSuggestions", uri);
        AJAX.GET(
        endpoint,
        (GetSuggestedResponse r) =>
        {
            Debug.Log(JsonUtility.ToJson(r, true));
        }
        );
    }

    public static void GetFeed(string handle)
    {
        var uri = new Dictionary<string, string> { {"actor", handle} };
        if (false)
            uri.Add("limit", Mathf.Clamp(50, 1, 100).ToString());
        if (false)
            uri.Add("cursor", "");
        var endpoint = GetEndpointURL("app.bsky.feed.getActorFeeds", uri);
        AJAX.GET(
        endpoint,
        (GetFeedResponse r) =>
        {
            Debug.Log(JsonUtility.ToJson(r, true));
        }
        );
    }

    public static void GetLikedPosts(string handle)
    {
        var uri = new Dictionary<string, string> { { "actor", handle } };
        if (false)
            uri.Add("limit", Mathf.Clamp(50, 1, 100).ToString());
        if (false)
            uri.Add("cursor", "");
        var endpoint = GetEndpointURL("app.bsky.feed.getActorLikes", uri);
        AJAX.GET(
        endpoint,
        (GetLikedPostsResponse r) =>
        {
            Debug.Log(JsonUtility.ToJson(r, true));
        }
        );
    }
}

[Serializable]
public struct LoginSession
{
    public string identifier;
    public string password;

    //public bool RememberCredentials { get; private set; }

    public LoginSession(string username, string password, bool rememberCredentials = false)
    {
        identifier = username;
        this.password = password;
        //RememberCredentials = rememberCredentials;
    }
}

[Serializable]
public struct LoginResponse : IResponse
{
    [SerializeField] string accessJwt;
    [SerializeField] string refreshJwt;
    [SerializeField] string handle;
    [SerializeField] string did;
    [SerializeField] didDoc didDoc;
    [SerializeField] string email;
    [SerializeField] bool emailConfirmed;
}

[Serializable]
public struct didDoc
{
    [SerializeField] string[] @context;
    [SerializeField] string id;
    [SerializeField] string[] alsoKnownAs;
    [SerializeField] VerificationMethod[] verificationMethod;
    [SerializeField] Service[] service;
}

[Serializable]
public struct VerificationMethod
{
    [SerializeField] string id;
    [SerializeField] string type;
    [SerializeField] string controller;
    [SerializeField] string publicKeyMultibase;
}

[Serializable]
public struct Service
{
    [SerializeField] string id;
    [SerializeField] string type;
    [SerializeField] string serviceEndpoint;
}

[Serializable]
public class Profile : IResponse
{
    [SerializeField] string did;
    [SerializeField] string handle;
    [SerializeField] string displayName;
    [SerializeField] string description;
    [SerializeField] string avatar;
    [SerializeField] DateTime indexedAt;
    [SerializeField] ProfileViewer viewer;
    [SerializeField] Label[] labels;
}
[Serializable]
public class DetailedProfile : Profile
{
    [SerializeField] string banner;
    [SerializeField] int followersCount;
    [SerializeField] int followsCount;
    [SerializeField] int postsCount;
}

[Serializable]
public struct ProfileViewer
{
    [SerializeField] bool muted;
    [SerializeField] bool blockedBy;
}
[Serializable]
public struct Label
{
    [SerializeField] string src;
    [SerializeField] string uri;
    [SerializeField] string cid;
    [SerializeField] string val;
    [SerializeField] bool neg;
    [SerializeField] DateTime cts;
}

[Serializable]
public struct GetProfilesResponse : IResponse
{
    [SerializeField] DetailedProfile[] profiles;
}

[Serializable]
public struct GetSuggestedResponse : IResponse
{
    [SerializeField] Profile[] actors;
    [SerializeField] string cursor;
}

[Serializable]
public struct GetPreferencesResponse : IResponse
{
    [SerializeField] object[] preferences;
}
[Serializable]
public struct Preference
{
    //string $type;
}

[Serializable]
public struct GetFeedResponse : IResponse
{
    [SerializeField] Feed[] feeds;
}
[Serializable]
public struct Feed
{
    [SerializeField] string uri;
    [SerializeField] string cid;
    [SerializeField] string did;
    [SerializeField] Profile creator;
    [SerializeField] string displayName;
    [SerializeField] string description;
    [SerializeField] object[] descriptionFacets;
    [SerializeField] string avatar;
    [SerializeField] int likeCount;
    [SerializeField] FeedViewer viewer;
    [SerializeField] DateTime indexedAt;
}
[Serializable]
public struct FeedViewer
{
    [SerializeField] string like;
}

[Serializable]
public struct GetLikedPostsResponse : IResponse
{
    [SerializeField] PostData[] feed;
}
[Serializable]
public struct PostData
{
    [SerializeField] Post post;
    [SerializeField] Reply reply;
    [SerializeField] Reason reason;
}
[Serializable]
public struct Post
{
}
[Serializable]
public struct Reply
{
}
[Serializable]
public struct Reason
{
}