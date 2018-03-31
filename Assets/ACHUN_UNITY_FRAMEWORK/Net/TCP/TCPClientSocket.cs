using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TCPClientSocket
{
    /// <summary>
    /// socket信息
    /// </summary>
    public enum Socket_Error
    {
        Success = 0,        
        Timeout,
        SocketNull,
        SocketUnconnect,
        ConnectUnsuccessUnknow,
        ConnectUnknow,
        ConnectError,
        SendUnsuccessUnknow,
        RecvUnsuccessUnknow,
        DisconnectUnknow,
    }

    private Socket_Error socket_error;

    public delegate void ConnectCallback(bool success,Socket_Error error,string exception);
    public delegate void SendCallback(bool success, Socket_Error error, string exception);
    public delegate void RecvCallback(bool success, Socket_Error error, string exception,byte[] byteMsg,string strMsg);
    public delegate void DisconnectCallback(bool success, Socket_Error error, string exception);

    private ConnectCallback connectCallback;
    private SendCallback sendCallback;
    private RecvCallback recvCallback;
    private DisconnectCallback disconnectCallback;

    private Socket socket;
    private string IP;
    private ushort port;

    private SocketBuffer recvBuffer;
    private byte[] buffer;

    public TCPClientSocket()
    {
        recvBuffer = new SocketBuffer(6);
    }


    public void RecvMsgOver(byte[] allByte)
    {

    }


    public void Connect(string IP,ushort port,ConnectCallback connectCallback,RecvCallback recvCallback)
    {
        socket_error = Socket_Error.Success;
        this.connectCallback = connectCallback;
        this.recvCallback = recvCallback;
        if (socket != null && socket.Connected)
        {
            this.connectCallback(false, Socket_Error.Success, "connect report");
        }
        else if (socket == null || !socket.Connected)
        {
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress,port);
            IAsyncResult connect = socket.BeginConnect(ipEndPoint, ConnectedCallback, socket);
            if (!WriteDot(connect))
            {
                this.connectCallback(false,Socket_Error.Timeout,"连接超时");
            }
        }
    }

    private void ConnectedCallback(IAsyncResult ar)
    {
        try
        {
            socket.EndConnect(ar);
            if (!socket.Connected)
            {
                socket_error = Socket_Error.ConnectError;
                return;
            }
            else
            {
                this.connectCallback(true,Socket_Error.Success,"连接成功");

            }
        }
        catch(Exception e)
        {
            this.connectCallback(false,Socket_Error.ConnectError,e.ToString());
        }
    }

    /// <summary>
    /// 判断超时
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    private bool WriteDot(IAsyncResult ar)
    {
        int i = 0;
        while (!ar.IsCompleted)
        {
            i++;
            if (i > 20)
            {
                socket_error = Socket_Error.Timeout;
                return false;
            }
            Thread.Sleep(100);
        }
        return true;
    }


    public void Receive()
    {
        if (socket != null && socket.Connected)
        {
            socket.BeginReceive(buffer,0,buffer.Length,SocketFlags.None, RecvedCallback,socket);
        }
    }


    private void RecvedCallback(IAsyncResult ar)
    {
        try
        {
            if (!socket.Connected)
            {
                this.recvCallback(false,Socket_Error.RecvUnsuccessUnknow,"连接出错",null,"");
                return;
            }

            int length = socket.EndReceive(ar);
            if (length == 0)
                return;

            recvBuffer.RecvByte(buffer,length);
        }
        catch (Exception e)
        {
            this.recvCallback(false,Socket_Error.RecvUnsuccessUnknow,"",null,"");
        }
        Receive();
    }


    public void Send(byte[] sendBuffer,SendCallback sendCallback)
    {
        this.sendCallback = sendCallback;
        if (socket != null && socket.Connected)
        {
            IAsyncResult send = socket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, sendedCallback, socket);
            if (!WriteDot(send))
                this.sendCallback(false,Socket_Error.SendUnsuccessUnknow,"send failed");
        }
        else
            this.sendCallback(false, Socket_Error.SendUnsuccessUnknow, "");
    }


    private void sendedCallback(IAsyncResult ar)
    {
        try
        {
            int bytesend = socket.EndSend(ar);
            //if()
        }
        catch (Exception e)
        {

        }
    }
}


//public class SocketTime
