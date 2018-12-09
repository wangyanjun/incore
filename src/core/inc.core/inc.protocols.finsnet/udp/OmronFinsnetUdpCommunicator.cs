using inc.core;
using inc.core.communication;
using inc.core.plc;
using System;
using System.Net;
using System.Net.Sockets;

namespace inc.protocols.finsnet.udp
{
    /// <summary>
    /// Udp communicator for omron finsnet plc
    /// </summary>
    public class OmronUDPFinsNetCommunicator : PLCCommunicator
    {
        private UdpClient _client;

        private IPAddress _address;

        private IPEndPoint _remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

        public FinsOptions Options { get; private set; } = new FinsOptions();

        /// <summary>
        /// 获取通讯副本
        /// </summary>
        public override IPLCCommunicator Copy
        {
            get
            {
                var result = new OmronUDPFinsNetCommunicator()
                {
                    PLCHost = PLCHost,
                    Port = Port
                };

                Options.CopyTo(result.Options);
                return result;
            }
        }

        /// <summary>
        /// Get transport media
        /// </summary>
        public override TransportMedia TransportMedia => TransportMedia.Udp;

        /// <summary>
        /// Get protocol family
        /// </summary>
        public override core.plc.ProtocolFamily ProtocolFamily => core.plc.ProtocolFamily.FinsNet;

        /// <summary>
        /// Get address mapping
        /// </summary>
        public override IAddress AddressMapping { get; } = new FinsnetAddress();

        /// <summary>
        /// Construct OmronUDPFinsNetCommunicator
        /// </summary>
        public OmronUDPFinsNetCommunicator()
        {
        }

        public override OpResult<byte[]> Read(string address, ushort length)
        {
            var result = new OpResult<byte[]>();
            return result;
        }

        public override OpResult<bool> ReadBoolean(string address)
        {
            var result = new OpResult<bool>();
            var encoder = new FinsReadCommand() { Length = 1 };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsBoolean;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        public override OpResult<short> ReadInt16(string address)
        {
            var result = new OpResult<short>();
            var encoder = new FinsReadCommand();
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsInt16;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        public override OpResult<int> ReadInt32(string address)
        {
            var result = new OpResult<int>();
            var encoder = new FinsReadCommand() { Length = 2 };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsInt32;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        protected FinsFrame NewFrame()
        {
            var frame = new FinsFrame();
            Options.CopyTo(frame);
            return frame;
        }

        public override OpResult<float> ReadSingle(string address)
        {
            var result = new OpResult<float>();
            var encoder = new FinsReadCommand() { Length = 2 };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsSingle;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        public override OpResult<ushort> ReadUInt16(string address)
        {
            var result = new OpResult<ushort>();
            var encoder = new FinsReadCommand() { Length = 1 };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsUInt16;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        public override OpResult<uint> ReadUInt32(string address)
        {
            var result = new OpResult<uint>();
            var encoder = new FinsReadCommand() { Length = 2 };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsUInt32;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        public override OpResult<ulong> ReadUInt64(string address)
        {
            var result = new OpResult<ulong>();
            var encoder = new FinsReadCommand() { Length = 4 };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsUInt64;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        protected override void CloseConnection()
        {
            if (_client != null)
            {
                _client.Close();
            }
        }

        protected override OpResult ConnectServerCore(string plcHost, int port)
        {
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }

            var result = new OpResult();
            if (result.CheckTrue(IPAddress.TryParse(plcHost, out _address), SR.InvalidHostNameOrAddress))
            {
                Options.DA1 = _address.GetAddressBytes()[3];
                var endpoint = new IPEndPoint(_address, port);
                _client = new UdpClient();
                _client.Client.SendTimeout = 2000;
                _client.Client.ReceiveTimeout = 2000;
                _client.Connect(endpoint);
                if (_client.Client.LocalEndPoint is IPEndPoint local)
                {
                    Options.SA1 = local.Address.GetAddressBytes()[3];
                }
            }

            return result;
        }

        public override OpResult<double> ReadDouble(string address)
        {
            var result = new OpResult<double>();
            var encoder = new FinsReadCommand() { Length = 4 };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsDouble;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val.Value;
                }
            });

            return result;
        }

        /// <summary>
        /// 写入布尔值
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        public override OpResult Write(string address, bool value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 1 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        private void Read(string address, OpResult result, IAddressableEncoder encoder, IDecoder decoder, Action<OpResult, IEncoder, IDecoder> dataGotAction)
        {
            if (result.CheckNotNullOrWhiteSpace(address, SR.ArgumentIsNullOrEmpty))
            {
                FinsFrame frame = NewFrame();
                frame.MRC = 01;
                frame.SRC = 01;
                if (result.CheckTrue(encoder.FillAddress(address), SR.WrongAddressFormat))
                {
                    frame.Content = encoder.Encode();
                    var buffer = frame.Encode();
                    try
                    {
                        int count = _client.Send(buffer, buffer.Length);
                        buffer = _client.Receive(ref _remoteEndpoint);
                        var decodeFrame = new FinsFrame();
                        if (result.CheckTrue(decodeFrame.Decode(buffer), SR.DecodeError))
                        {
                            if (result.CheckTrue(decoder.Decode(decodeFrame.Content), SR.DecodeError))
                            {
                                dataGotAction?.Invoke(result, encoder, decoder);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.SetException(ex, SR.CallServiceException);
                    }
                }
            }
        }

        private void Write(string address, OpResult result, IAddressableEncoder encoder, IDecoder decoder, Action<OpResult, IEncoder, IDecoder> dataGotAction)
        {
            if (result.CheckNotNullOrWhiteSpace(address, SR.ArgumentIsNullOrEmpty))
            {
                FinsFrame frame = NewFrame();
                frame.MRC = 01;
                frame.SRC = 02;
                if (result.CheckTrue(encoder.FillAddress(address), SR.WrongAddressFormat))
                {
                    frame.Content = encoder.Encode();
                    var buffer = frame.Encode();
                    int count = _client.Send(buffer, buffer.Length);
                    buffer = _client.Receive(ref _remoteEndpoint);
                    var decodeFrame = new FinsFrame();
                    if (result.CheckTrue(decodeFrame.Decode(buffer), SR.DecodeError))
                    {
                        if (result.CheckTrue(decoder.Decode(decodeFrame.Content), SR.DecodeError))
                        {
                            dataGotAction?.Invoke(result, encoder, decoder);
                        }
                    }
                }
            }
        }

        public override OpResult Write(string address, short value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 1 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, ushort value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 1 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, uint value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 2 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, int value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 2 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, long value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 4 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, ulong value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 4 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, float value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 2 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, double value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 4 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, byte value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = 1 };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult<short[]> ReadInt16(string address, int count)
        {
            var result = new OpResult<short[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(1 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsInt16Array;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult<ushort[]> ReadUInt16(string address, int count)
        {
            var result = new OpResult<ushort[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(1 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsUInt16Array;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult<int[]> ReadInt32(string address, int count)
        {
            var result = new OpResult<int[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(2 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsInt32Array;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult<float[]> ReadSingle(string address, int count)
        {
            var result = new OpResult<float[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(2 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsSingleArray;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult<double[]> ReadDouble(string address, int count)
        {
            var result = new OpResult<double[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(4 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsDoubleArray;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult<bool[]> ReadBoolean(string address, int count)
        {
            var result = new OpResult<bool[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsBooleanArray;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult<ulong[]> ReadUInt64(string address, int count)
        {
            var result = new OpResult<ulong[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(4 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsUInt64Array;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult<uint[]> ReadUInt32(string address, int count)
        {
            var result = new OpResult<uint[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(2 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                var val = decoder.DataAsUInt32Array;
                if (result.CheckNotNull(val, SR.DecodeError))
                {
                    result.Content = val;
                }
            });

            return result;
        }

        public override OpResult Write(string address, bool[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(1 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, short[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(1 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, byte[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(1 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, ushort[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(1 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, uint[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(2 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, int[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(2 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, long[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(4 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, float[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(2 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, double[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(4 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult Write(string address, ulong[] value)
        {
            var result = new OpResult();
            var encoder = new FinsWriteCommand() { Length = (ushort)(4 * value.Length) };
            var decoder = new FinsWriteResponseCommand();
            encoder.SetData(value);
            Write(address, result, encoder, decoder, (r, en, de) =>
            {
            });

            return result;
        }

        public override OpResult<byte[]> ReadUInt16Bytes(string address, int count)
        {
            var result = new OpResult<byte[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(1 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                if (result.CheckNotNull(decoder.Data, SR.DecodeError))
                {
                    result.Content = decoder.Data;
                }
            });

            return result;
        }

        public override OpResult<byte[]> ReadBooleanBytes(string address, int count)
        {
            var result = new OpResult<byte[]>();
            var encoder = new FinsReadCommand() { Length = (ushort)(1 * count) };
            var decoder = new FinsReadResponseCommand();
            Read(address, result, encoder, decoder, (r, en, de) =>
            {
                if (result.CheckNotNull(decoder.Data, SR.DecodeError))
                {
                    decoder.Data.SwapEvenOdd();
                    result.Content = decoder.Data;
                }
            });

            return result;
        }
    }
}
