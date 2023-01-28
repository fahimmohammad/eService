using AutoMapper;
using eProsecutionGrpcServer;
using eProsecutionGrpcServer.DAO;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;

namespace eProsecutionGrpcServer.GrpcServices
{
    public class CustomerService : CustomerGrpc.CustomerGrpcBase
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomer _customer;
        
        public CustomerService(ILogger<CustomerService> logger, ICustomer customer)
        {
            _logger = logger;
            _customer = customer;
        }
        public override Task<GetCustomerReply> GetCustomer(Empty req,ServerCallContext context)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Model.Customer, Customer>()
                );
            var mapper = new Mapper(config);
            var data = _customer.GetCustomer();
            GetCustomerReply response = new GetCustomerReply();
            foreach (var  item in data){
                response.Customer.Add(mapper.Map<Customer>(item));
            }
            //var reply = mapper.Map<GetCustomerReply>(data);
           /* if (data == null) {
                List<Model.Customer> list = new List<Model.Customer>();
                Model.Customer cs = new Model.Customer() { id =1,name="Asdf"};
                list.Add(cs);
                data = list;
            }*/
            return Task.FromResult(response);
        }
        public override Task<GetCustomerByIdReply> GetCustomerById(GetCustomerByIdReq request, ServerCallContext context)
        {
            
            var data = _customer.GetCustomerById(request.Id);
            return Task.FromResult(new GetCustomerByIdReply
            {
                Customer = new Customer{Id=data.Id,Name=data.Name}
            });
        }
    }
}
