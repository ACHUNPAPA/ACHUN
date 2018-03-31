using UnityEngine;
using System.Collections;
using System;

public class BytesProtocol : BaseProtocol
{
    public byte[] bytes;

    public override BaseProtocol Decode(byte[] readBuffer, int start, int length)
    {
        BytesProtocol protocol = new BytesProtocol();
        protocol.bytes = new byte[length];
        Array.Copy(readBuffer,start,protocol.bytes,0,length);
        return protocol;
    }


    public override byte[] Encode()
    {
        return bytes;
    }

    public override string GetProtocolID()
    {
        return GetStr(0);
    }


    public override string GetDesc()
    {
        string str = string.Empty; ;
        if (bytes == null)
            return string.Empty;

        for (int i = 0; i < bytes.Length; i++)
        {
            str += bytes[i].ToString() + " ";
        }
        return str;
    }


    public void AddString(string str)
    {
        int len = str.Length;
        byte[] lenBytes = BitConverter.GetBytes(len);
        byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(str);
        //if (bytes == null)
        //    bytes = lenBytes.Concat().ToArray();
        //else
        //    bytes = bytes.Concat().Concat(strBytes).ToArray();
    }


    private string GetStr(ushort start,ref ushort end)
    {
        if (bytes == null)
            return null;

        if (bytes.Length > start + sizeof(int))
            return null;

        int strLen = BitConverter.ToInt32(bytes,start);
        if (bytes.Length < start + sizeof(int) + strLen)
            return null;

        string str = System.Text.Encoding.UTF8.GetString(bytes,start + sizeof(int),strLen);
        end = (ushort)(start + sizeof(int) + strLen);
        return str;
    }


    private string GetStr(ushort start)
    {
        ushort end = 0;
        return GetStr(start,ref end);
    }
}
