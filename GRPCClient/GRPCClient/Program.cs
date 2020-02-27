using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DMSService;
using Grpc.Net.Client;
using GRPCClient.DMS;

namespace GRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using(var file = File.OpenRead(@"C:\ClientOrdner\Test.txt"))
            {
                var client = new DMSClient();
                var ans = await client.UploadDokument(file);
                Console.WriteLine(ans.Message + ans.Code);
            }
        }
    }
}
