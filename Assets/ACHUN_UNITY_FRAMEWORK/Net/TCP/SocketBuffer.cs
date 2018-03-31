using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketBuffer
{
    private byte[] headBuffer;
    private byte headLength = 6;
    private byte[] recvBuffer;

    //当前接收数据长度
    private int curRecvLength;

    //总接收数据长度
    private int allRecvLength;

    public SocketBuffer(byte tmpHeadLength)
    {
        headLength = tmpHeadLength;
        headBuffer = new byte[headLength];
    }

    public void RecvByte(byte[] recvByte, int realLength)
    {
        if (realLength == 0)
            return;

        if (curRecvLength < headBuffer.Length)
        {
            RecvHead(recvBuffer,realLength);
        }
    }


    private void RecvHead(byte[] recvByte, int realLength)
    {
        int tmpReal = headBuffer.Length - curRecvLength;
        int tmpLength = curRecvLength + realLength;

        if (tmpLength < headBuffer.Length)
        {
            Buffer.BlockCopy(recvBuffer, 0, headBuffer, curRecvLength, realLength);
            curRecvLength += realLength;
        }
        else
        {
            Buffer.BlockCopy(recvBuffer, 0, headBuffer, curRecvLength, tmpReal);
            curRecvLength += tmpReal;
            allRecvLength = BitConverter.ToInt32(headBuffer,0) + headLength;
            recvBuffer = new byte[allRecvLength];
        }
    }
}
