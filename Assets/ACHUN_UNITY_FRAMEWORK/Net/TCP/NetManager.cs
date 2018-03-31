using UnityEngine;
using System.Collections;

namespace Achun.Net
{
    public class NetManager
    {
        public static Connection conn = new Connection();

        public void Update()
        {
            conn.Update();
        }


        public static BaseProtocol GetHeatBeatProtocol()
        {
            BytesProtocol protocol = new BytesProtocol();
            protocol.AddString("HeatBeat");
            return protocol;
        }
    }
}