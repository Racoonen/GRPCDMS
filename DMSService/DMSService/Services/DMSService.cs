using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using proto.dms;

namespace DMSService
{
    public class DMSService : proto.dms.DocumentService.DocumentServiceBase
    {
        private readonly ILogger<DMSService> _logger;
        public DMSService(ILogger<DMSService> logger)
        {
            _logger = logger;
        }

        public override async Task<UploadStatus> UploadDoc(IAsyncStreamReader<Chunk> aRequestStream, ServerCallContext context)
        {
            using (var newfile = File.Create(@"C:\ServerOrdner\Test.txt"))
            {
                while (await aRequestStream.MoveNext())
                {
                    var chunk = aRequestStream.Current;
                    newfile.Write(chunk.Content.ToByteArray());
                }
            }
            return new UploadStatus() { Code = UploadStatusCode.Ok, Message = "JUHU" };
        }
    }
}
