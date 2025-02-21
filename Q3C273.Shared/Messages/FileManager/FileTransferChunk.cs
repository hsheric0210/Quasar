﻿using ProtoBuf;
using Q3C273.Shared.Models;

namespace Q3C273.Shared.Messages.FileManager
{
    [ProtoContract]
    public class FileTransferChunk : IMessage
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string FilePath { get; set; }

        [ProtoMember(3)]
        public long FileSize { get; set; }

        [ProtoMember(4)]
        public FileChunk Chunk { get; set; }
    }
}
