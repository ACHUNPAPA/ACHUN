//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LZW
//{
//    //LZW编解码中使用的常量数据
//    const int BITS = 12;//largest code size
//    const int SHIFT = 4;//shift for hashing
//    const int THELIMIT = 4095;//NEVER generate this
//    const int HSIZE = 5003;//hash table size

//    const int CODE_CLEAR = (1 << 8);//clean the dictionary table
//    const int CODE_EOF = CODE_CLEAR + 1;//end of once package

//    uint[] raw_buffer;
//    uint[] end_addr;
//    uint[] cur_addr;
//    uint cur_bits;

//    byte FinChar;

//    int curMaxCode;
//    int emptySlot;
//    uint curCodeBits;
//    int[] Prefix = new int[4096];
//    int[] Suffix = new int[4096];
//    int[] OutCode = new int[4096];

//    int cur_addr_index = 0;

//    public void Clear()
//    {
//        cur_addr_index = 0;
//    }

//    public LZW()
//    {
//        reset();
//    }

//    public bool empty()
//    {
//        return emptySlot == CODE_CLEAR + 2;
//    }

//    public void reset()
//    {
//        curCodeBits = 9;
//        emptySlot = CODE_CLEAR + 2;
//        curMaxCode = (1 << (int)curCodeBits);
//        FinChar = 0;
//    }


//    public void SetEncoderData(byte[] entry, uint size)
//    {
//        if (size % 4 != 0)
//        {
//            Debug.LogError("");
//            raw_buffer = new uint[size / 4];
//        }
//        else
//        {
//            //size应是4的整数倍
//            raw_buffer = new uint[size / 4 + 1];
//        }

//        for (int i = 0; i < raw_buffer.Length; ++i)
//        {
//            if (4 * i + 4 > entry.Length)
//            {
//                byte[] tem = new byte[4];
//                for (int j = 0; j < 4; ++j)
//                {
//                    if (j + 4 * i < entry.Length)
//                        tem[j] = entry[j + 4 * i];
//                    else
//                        tem[j] = 0;
//                }
//                raw_buffer[i] = BitConverter.ToUInt32(tem, 0);
//            }
//            else
//            {
//                raw_buffer[i] = BitConverter.ToUInt32(entry,4 * i);
//            }
//        }

//        if (raw_buffer.Length - (size >> 2) > 0)
//        { }
//        cur_addr = raw_buffer;
//        cur_bits = 0;
//    }


//    private uint Get(uint bits)
//    {
//        //data &= ((1 << bits) - 1);将无效位的数据清除，不过为了效率，直接默认外部传入的数据已经只存在有效位
//        uint temp_ur_addr = 0;
//        if (cur_addr_index < cur_addr.Length)
//            temp_ur_addr = cur_addr[cur_addr_index];
//        else
//            temp_ur_addr = 0;

//        uint data = (uint)((temp_ur_addr >> (int)cur_bits) & ((1 << (int)bits) - 1));
//        cur_bits += bits;

//        //如果bit位数已经超过4个字节（DWORD），则cur_addr需要增加
//        if ((cur_bits & 0xffffffe0) != 0)
//        {
//            cur_bits &= 0x1f;
//            cur_addr_index++;
//            if (cur_addr_index < cur_addr.Length)
//                temp_ur_addr = cur_addr[cur_addr_index];
//            else
//                temp_ur_addr = 0;
//            if (cur_bits != 0)
//                return (uint)(data | ((temp_ur_addr & ((1 << (int)cur_bits) - 1)) << (int)(bits - cur_bits)));
//            return data;
//        }
//    }


//    public byte[] lzw_decode(byte[] outbuf, uint size, out int oCount)
//    {
//        byte[] buff = outbuf;
//        const int CODEMASK = 0xff;
//        int buffer_index = 0;
//        int CurCode = 0;
//        int OldCode = 0;
//        int InCode = 0;
//        int OutCount = 0;
//        int count = 0;
//        oCount = count;

//        if (empty())
//        {
//            OldCode = (int)Get(curCodeBits);
//            FinChar = (byte)OldCode;
//            if (count >= size)
//            {
//                oCount = -1;
//                return buff;
//            }
//            else
//            {
//                buff[buffer_index++] = FinChar;
//                count++;
//            }
//            CurCode = (int)Get(curCodeBits);
//        }
//        else
//        {
//            OldCode = CODE_EOF;
//            CurCode = (int)Get(curCodeBits);
//        }
//        for (; count < size;)
//        {
//            if (CurCode < 0)
//            {
//                oCount = -1;
//                return buff;
//            }

//            if (CurCode == CODE_EOF)
//            {
//                oCount = count;
//                return buff;
//            }

//            //if()
//        }
//    }
//}
