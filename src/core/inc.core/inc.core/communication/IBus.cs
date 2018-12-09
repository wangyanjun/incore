using System;
using System.Net;
using System.Net.Sockets;

namespace inc.core.communication
{
    public interface IBus : IDisposable
    {
        int Send(byte[] content, int count);

        int Read(byte[] buffer, int offset, int count);

        byte[] Read();

        bool Connect();

        void Close();
    }

    public abstract class Bus : IBus
    {
        /// <summary>
        /// Get whether is disposed
        /// </summary>
        public bool IsDisposed { get; protected set; }

        /// <summary>
        /// Disconstruct this object
        /// </summary>
        ~Bus()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                
            }
        }

        /// <summary>
        /// Dispose this object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Write data
        /// </summary>
        /// <param name="content">Data to be written</param>
        /// <param name="count">write bytes count</param>
        public abstract int Send(byte[] content,  int count);

        public bool Connect()
        {
            var result = false;
            try
            {
                result=ConnectCore();
            }
            catch
            {
            }

            return result;
        }

        protected abstract bool ConnectCore();

        public abstract int Read(byte[] buffer, int offset, int count);

        public abstract byte[] Read();

        public void Close()
        {
            Dispose();
        }
    }

    public class TcpBus : Bus
    {
        private TcpClient _client;

        public string Host { get; set; }

        public int Port { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _client.Client.Receive(buffer, 0, count, SocketFlags.None);
        }

        public override byte[] Read()
        {
            var data = new byte[_client.Available];
            int count=_client.Client.Receive(data);
            if(count!=data.Length)
            {
                Array.Resize(ref data, count);
            }

            return data;
        }

        public override int Send(byte[] content, int count)
        {
            return _client.Client.Send(content, 0, count, SocketFlags.None);
        }

        protected override bool ConnectCore()
        {
            _client?.Close();
            _client = new TcpClient();
            _client.Connect(Host, Port);
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                _client?.Dispose();
                _client = null;
                base.Dispose(disposing);
            }
        }
    }

    public class UdpBus : Bus
    {
        private UdpClient _client;

        private IPAddress _address;

        private IPEndPoint _remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

        public string Host { get; set; }

        public int Port { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _client.Client.Receive(buffer, 0, count, SocketFlags.None);
        }

        public override byte[] Read()
        {
            return _client.Receive(ref _remoteEndpoint);
        }

        public override int Send(byte[] content, int count)
        {
            return _client.Send(content, count);
        }

        protected override bool ConnectCore()
        {
            var result = false;
            if (IPAddress.TryParse(Host, out _address))
            {
                var endpoint = new IPEndPoint(_address, Port);
                _client = new UdpClient();
                _client.Client.SendTimeout = 2000;
                _client.Client.ReceiveTimeout = 2000;
                _client.Connect(endpoint);
                result = true;
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                _client?.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
