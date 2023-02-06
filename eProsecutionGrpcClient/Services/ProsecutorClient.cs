using eProsecutionGrpc;
using eProsecutionGrpcClient;
using Grpc.Core;
using Grpc.Net.Client;

namespace eProsecutionGrpcClient.Services
{
    public class ProsecutorClient:ProsecutorGrpc.ProsecutorGrpcClient
    {
        private readonly GrpcChannel channel;
        ProsecutorGrpc.ProsecutorGrpcClient client;
        private readonly string address;

        public ProsecutorClient(IConfiguration iconfig)
        {
            address = iconfig.GetValue<string>("GrpcServerAddress");
            channel = GrpcChannel.ForAddress(address);
            client = new ProsecutorGrpc.ProsecutorGrpcClient(channel);
        }
        public ProsecutorLoginReply ProsecutorLogin(string id,string password)
        {
            ProsecutorLoginReply rep = new ProsecutorLoginReply();
                var reply = client.ProsecutorLogin(new ProsecutorLoginReq
                {
                    UserId = id,
                    Password = password
                });
                
                rep.MergeFrom(reply);
                return rep;
        }

       
    
    }
}
