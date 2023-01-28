using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System.Threading.Channels;
using static eProsecutionGrpcClient.CustomerGrpc;

namespace eProsecutionGrpcClient.Services
{
    public class CustomerClient : CustomerGrpc.CustomerGrpcClient
    {
        //private readonly ILogger<CustomerClient> _logger;
        private readonly GrpcChannel channel;
        CustomerGrpc.CustomerGrpcClient client;

    public CustomerClient()
        {
            
            channel = GrpcChannel.ForAddress("http://localhost:5008");
            client = new CustomerGrpc.CustomerGrpcClient(channel);
        }
        public Customer GetCustomerById(int id)
        {
            var reply = client.GetCustomerById(new GetCustomerByIdReq
            {
                Id = id
            });
            Console.WriteLine(reply.Customer.Name);
           return reply.Customer;
        }

        public List<Customer> GetCustomer()
        {
            var reply = client.GetCustomer(new Empty { });
           // Console.WriteLine(reply.Customer.Name);
            return reply.Customer.ToList();
        }
    }
}
