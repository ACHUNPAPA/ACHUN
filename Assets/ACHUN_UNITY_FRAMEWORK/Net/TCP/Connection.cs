using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
namespace Achun.Net
{
    public class Connection
    {
        private const int BUFFER_SIZE = 1024;
        private Socket socket;
        private byte[] readBuffer = new byte[BUFFER_SIZE];
        private int bufferCount = 0;
        private int msgLength = 0;
        private byte[] lenBytes = new byte[sizeof(int)];
        public BaseProtocol protocol;
        //心跳包
        public float lastTickTime = 0;
        public float heartBeatTime = 30;

        public MsgDistribution msgDist = new MsgDistribution();

        public enum Status
        {
            None,
            Connect,
        }
        public Status status = Status.None;


        public bool Connect(string host, int port)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(host, port);
                socket.BeginReceive(readBuffer, bufferCount, BUFFER_SIZE - bufferCount, SocketFlags.None, ReceiveCb, readBuffer);
                status = Status.Connect;
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Close()
        {
            try
            {
                socket.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


        private void ReceiveCb(IAsyncResult ar)
        {
            try
            {
                int count = socket.EndReceive(ar);
                bufferCount = bufferCount + count;
                ProcessData();
                socket.BeginReceive(readBuffer, bufferCount, BUFFER_SIZE - bufferCount, SocketFlags.None, ReceiveCb, readBuffer);
            }
            catch
            {
                status = Status.None;
            }
        }


        private void ProcessData()
        {
            //粘包处理
            if (bufferCount < sizeof(int))
                return;

            Array.Copy(readBuffer, lenBytes, sizeof(int));
            msgLength = BitConverter.ToInt32(lenBytes, 0);
            if (bufferCount < msgLength + sizeof(int))
                return;

            BaseProtocol protocol = this.protocol.Decode(readBuffer, sizeof(int), msgLength);
            lock (msgDist.msgList)
                msgDist.msgList.Add(protocol);

            int count = bufferCount - msgLength - sizeof(int);
            Array.Copy(readBuffer, sizeof(int) + msgLength, readBuffer, 0, count);
            bufferCount = count;
            if (bufferCount > 0)
                ProcessData();
        }


        public bool Send(BaseProtocol protocol)
        {
            if (status != Status.Connect)
                return false;

            byte[] bytes = protocol.Encode();
            byte[] length = BitConverter.GetBytes(bytes.Length);
            //byte[] sendBuff = length.Concat(bytes).ToArray();
            //socket.Send(sendBuff);
            return true;
        }


        public bool Send(BaseProtocol protocol, string cbName, MsgDistribution.Delegate cb)
        {
            if (status == Status.None)
                return false;
            msgDist.AddListener(cbName, cb);
            return Send(protocol);
        }


        public bool Send(BaseProtocol protocol, MsgDistribution.Delegate cb)
        {
            string cbName = protocol.GetProtocolID();
            return Send(protocol, cbName, cb);
        }


        public void Update()
        {
            msgDist.Update();
            if (status == Status.Connect)
                if (Time.time - lastTickTime > heartBeatTime)
                {
                    //BaseProtocol protocol = NetManager.GetHeatBeatOrotocol();
                    Send(protocol);
                    lastTickTime = Time.time;
                }
        }
    }
}
