using AutoMapper;
using eProsecutionGrpc;
using eProsecutionGrpcServer.DAO;
using Grpc.Core;

namespace eProsecutionGrpcServer.GrpcServices
{
    public class ProsecutorService: ProsecutorGrpc.ProsecutorGrpcBase
    {
        private readonly ILogger<ProsecutorService> _logger;
        private readonly IProsecutor _prosecutor;
       // private readonly IDatalist _datalist;

        public ProsecutorService(ILogger<ProsecutorService> logger, IProsecutor prosecutor)
        {
            _logger = logger;
            _prosecutor = prosecutor;
          
        }
        

        public override Task<ProsecutorLoginReply> ProsecutorLogin(ProsecutorLoginReq req, ServerCallContext context)
        {
          
            var data = _prosecutor.ProsecutroLogin(req.UserId,req.Password);
            ProsecutorLoginReply response = new ProsecutorLoginReply();
            if (data != null)
            {
                response.Prosecutor = data;
                return Task.FromResult(response);
            }
            else {
                Prosecutor prosecutor = new Prosecutor();
                prosecutor.Id = req.UserId;
                prosecutor.LoginName = "";
                prosecutor.DivisionName = "";
                prosecutor.Name = "";
                prosecutor.Id = req.UserId;
                response.Prosecutor = prosecutor;
                ExceptionReply exceptionReply = new ExceptionReply();
                exceptionReply.Code = "204";
                exceptionReply.Message = "User Name Password didn't matched";
                response.ExceptionReply = exceptionReply;

               /* response.ExceptionReply.Code = "200";
                response.ExceptionReply.Message = "User Name Password didn't matched";*/
                return Task.FromResult(response);
            }
           
            /*ProsecutorLoginReply response = new ProsecutorLoginReply();
            response.Id = data.ID;
            response.LoginName = data.LOGINNAME;
            response.Name = data.NAME;
            response.DivisionName = data.DIVISIONNAME;*/
            //var reply = mapper.Map<GetCustomerReply>(data);
            /* if (data == null) {
                 List<Model.Customer> list = new List<Model.Customer>();
                 Model.Customer cs = new Model.Customer() { id =1,name="Asdf"};
                 list.Add(cs);
                 data = list;
             }*/
            
        }
    }
}
