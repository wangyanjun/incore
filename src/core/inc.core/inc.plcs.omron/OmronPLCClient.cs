using inc.core.plc;
using inc.protocols.finsnet.udp;

namespace inc.plcs.omron
{
    public class OmronPLCClient : PLCClient
    {
        public OmronPLCClient(string host, int port)
        {           
            Communicator = new OmronUDPFinsNetCommunicator()
            {
                PLCHost = host,
                Port = port
            };
        }
    }
}
