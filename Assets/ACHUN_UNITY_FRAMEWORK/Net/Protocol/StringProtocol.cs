using UnityEngine;
using System.Collections;

public class StringProtocol : BaseProtocol
{
    public string str;

    public override BaseProtocol Decode(byte[] readBuffer, int start, int length)
    {
        StringProtocol protocol = new StringProtocol();
        protocol.str = System.Text.Encoding.UTF8.GetString(readBuffer, start,length);
        return protocol;
    }


    public override byte[] Encode()
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
        return bytes;
    }


    public override string GetProtocolID()
    {
        if (str.Length == 0)
            return string.Empty;
        return str.Split(',')[0];
    }


    public override string GetDesc()
    {
        return str;
    }
}
