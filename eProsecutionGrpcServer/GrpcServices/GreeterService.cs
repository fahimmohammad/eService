using eProsecutionGrpcServer;
using eProsecutionGrpcServer.DAO;
using Grpc.Core;

namespace eProsecutionGrpcServer.GrpcServices
{
    public class GreeterService : Greeter.GreeterBase
    {
       private readonly ILogger<GreeterService> _logger;
        private readonly ICustomer _customer;
        public GreeterService(ILogger<GreeterService> logger, ICustomer customer)
        {
            _logger = logger;
            _customer = customer;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var data = _customer.GetCustomerById(Int32.Parse(request.Name.ToString()));
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + " customer Name= " + data.Name
            });
        }
    }
}