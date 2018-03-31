using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Request_Type : byte
{
    GET = 1,
    POST,
}

public class HTTPRequest
{
    private UnityWebRequest request;
    private Request_Type requestType;
    private string url;

    public HTTPRequest(string url,Request_Type requestType)
    {
        this.url = url;
        this.requestType = requestType;
    }

    public void Request()
    {
        request = new UnityWebRequest();
        switch (requestType)
        {
            case Request_Type.GET:
                break;
            case Request_Type.POST:
                break;
            default:
                break;
        }

        request.disposeDownloadHandlerOnDispose = true;
        request.disposeUploadHandlerOnDispose = true;
    }

    public void Dispose()
    {
        request.Dispose();
    }
}
