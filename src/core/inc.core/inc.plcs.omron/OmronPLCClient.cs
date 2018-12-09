using inc.core.plc;
using inc.protocols.finsnet.udp;

namespace inc.plcs.omron
{
    /// <summary>
    /// Omron plc client
    /// </summary>
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
