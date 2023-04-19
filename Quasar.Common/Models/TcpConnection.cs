﻿using ProtoBuf;
using Q3C273.Shared.Enums;

namespace Q3C273.Shared.Models
{
    [ProtoContract]
    public class TcpConnection
    {
        [ProtoMember(1)]
        public string ProcessName { get; set; }

        [ProtoMember(2)]
        public string LocalAddress { get; set; }

        [ProtoMember(3)]
        public ushort LocalPort { get; set; }

        [ProtoMember(4)]
        public string RemoteAddress { get; set; }

        [ProtoMember(5)]
        public ushort RemotePort { get; set; }

        [ProtoMember(6)]
        public ConnectionState State { get; set; }
    }
}
