using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;
//using ExitGames.Client.Photon.;

public class PhotonClient : MonoBehaviour, IPhotonPeerListener
{
    private PhotonPeer peer;

    private void Awake()
    {
        peer = new PhotonPeer(this,ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "LoadBalancing");
    }

    private void Update()
    {
        peer.Service();
    }


    private void SendMessage()
    {
        Dictionary<byte, object> para = new Dictionary<byte, object>();
        para.Add(218,1);
        peer.OpCustom(218,para,true);
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 100, 100, 30), "设置游戏编号"))
        {
            SendMessage();
        }
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnEvent(EventData eventData)
    {
        Debug.Log("触发了事件：" + eventData.ToStringFull());
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log("服务器返回响应" + operationResponse.ToStringFull());
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connect Success!");
                break;
            case StatusCode.Disconnect:
                Debug.Log("Disconnect!");
                break;
            case StatusCode.Exception:
                break;
            case StatusCode.ExceptionOnConnect:
                break;
            case StatusCode.SecurityExceptionOnConnect:
                break;
            case StatusCode.QueueOutgoingReliableWarning:
                break;
            case StatusCode.QueueOutgoingUnreliableWarning:
                break;
            case StatusCode.SendError:
                break;
            case StatusCode.QueueOutgoingAcksWarning:
                break;
            case StatusCode.QueueIncomingReliableWarning:
                break;
            case StatusCode.QueueIncomingUnreliableWarning:
                break;
            case StatusCode.QueueSentWarning:
                break;
            case StatusCode.ExceptionOnReceive:
                break;
            case StatusCode.TimeoutDisconnect:
                break;
            case StatusCode.DisconnectByServer:
                break;
            case StatusCode.DisconnectByServerUserLimit:
                break;
            case StatusCode.DisconnectByServerLogic:
                break;
            case StatusCode.EncryptionEstablished:
                break;
            case StatusCode.EncryptionFailedToEstablish:
                break;
            default:
                break;
        }
    }

    public void OnMessage(object messages)
    {
        throw new NotImplementedException();
    }
}
