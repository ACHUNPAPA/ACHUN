using UnityEngine;
using System.Collections;

public class BaseProtocol
{
    /// <summary>
    /// 解码器
    /// </summary>
    /// <param name="readBuffer"></param>
    /// <param name="start"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public virtual BaseProtocol Decode(byte[] readBuffer,int start,int length)
    {
        return new BaseProtocol();
    }

    /// <summary>
    /// 编码器
    /// </summary>
    /// <returns></returns>
    public virtual byte[] Encode()
    {
        return new byte[] { };
    }


    public virtual string GetProtocolID()
    {
        return string.Empty;
    }


    public virtual string GetDesc()
    {
        return string.Empty;
    }
}
