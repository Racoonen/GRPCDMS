using DMSService;
using Google.Protobuf;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GRPCClient.DMS
{
    public class DMSClient
    {
        public async Task<UploadStatus> UploadDokument(Stream aFile)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new DocumentService.DocumentServiceClient(channel);
            using (var call = client.UploadDoc())
            {
                byte[] buf = new byte[64];
                while(aFile.Read(buf) >0)
                    await call.RequestStream.WriteAsync(new Chunk() { Content = ByteString.CopyFrom(buf) });
                await call.RequestStream.CompleteAsync();
                return await call.ResponseAsync;
            }
        }
    }
}
