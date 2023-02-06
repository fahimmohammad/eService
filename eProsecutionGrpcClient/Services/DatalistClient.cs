using eProsecutionGrpc;
using eProsecutionGrpcClient;
using Grpc.Net.Client;

using Microsoft.Extensions.Configuration;
using System.Net;

namespace eProsecutionGrpcClient.Services
{
    public class DatalistClient:DatalistGrpc.DatalistGrpcClient
    {
       private readonly GrpcChannel channel;
       DatalistGrpc.DatalistGrpcClient client;
       private readonly string address;

        public DatalistClient(IConfiguration iconfig)
        {
            address = iconfig.GetValue<string>("GrpcServerAddress");
            channel = GrpcChannel.ForAddress(address);
            client = new DatalistGrpc.DatalistGrpcClient(channel);
        }
        public GetLocationReply GetLocation()
        {
            var reply = client.GetLocation(new Empty
            {  
            });
           
            return reply;
        }
        public GetBrtaOfficeReply GetBrtaOffice()
        {
            var reply = client.GetBrtaOffice(new Empty
            {
            });

            return reply;
        }
        public GetSeizedDocumentReply GetSeizedDocument()
        {
            var reply = client.GetSeizedDocument(new Empty
            {
            });

            return reply;
        }
        public GetProsecutionCodeReply GetProsecutionCode(string userid)
        {
            var reply = client.GetProsecutionCode(new GetProsecutionCodeReq
            {
                Userid = userid
            });

            return reply;
        }


    }
}
