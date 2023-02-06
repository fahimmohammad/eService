using AutoMapper;
using eProsecutionGrpcServer.DAO;
using eProsecutionGrpcServer;
using Google.Protobuf.Collections;
using Grpc.Core;
using eProsecutionGrpc;
using static eProsecutionGrpcServer.Model.Datalist;
using BrtaOffice = eProsecutionGrpc.BrtaOffice;
using Location = eProsecutionGrpc.Location;
using SeizedDocument = eProsecutionGrpc.SeizedDocument;
using ProsecutionCode = eProsecutionGrpc.ProsecutionCode;

namespace eProsecutionGrpcServer.GrpcServices
{
    public class DatalistService: DatalistGrpc.DatalistGrpcBase
    {
            private readonly ILogger<DatalistService> _logger;
            private readonly IDatalist _datalist;

            public DatalistService(ILogger<DatalistService> logger, IDatalist datalist)
            {
                _logger = logger;
                _datalist = datalist;
            }

        public override Task<GetProsecutionCodeReply> GetProsecutionCode(GetProsecutionCodeReq req, ServerCallContext context) {
            GetProsecutionCodeReply response = new GetProsecutionCodeReply();
            var data = _datalist.GetProsecutionCode(req.Userid);
            if (data.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                         cfg.CreateMap<Model.Datalist.ProsecutionCode, ProsecutionCode>()
                     );
                var mapper = new Mapper(config);
                foreach (var prosecutionCode in data)
                {
                    response.ProsecutionCode.Add((mapper.Map<ProsecutionCode>(prosecutionCode)));
                }
            }
            else
            {
                ExceptionReply excep = new ExceptionReply();
                excep.Code = "204";
                excep.Message = "No data found";
                response.ExceptionReply = excep;
            }
            return Task.FromResult(response);
        }
       public override Task<GetSeizedDocumentReply> GetSeizedDocument(Empty req,ServerCallContext context) {
            GetSeizedDocumentReply response = new GetSeizedDocumentReply();
            var data = _datalist.GetSeizedDocument();
            if (data.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                         cfg.CreateMap<Model.Datalist.SeizedDocument, SeizedDocument>()
                     );
                var mapper = new Mapper(config);
                foreach (var seizedDocument in data)
                {
                    response.SeizedDocument.Add((mapper.Map<SeizedDocument>(seizedDocument)));
                }
            }
            else
            {
                ExceptionReply excep = new ExceptionReply();
                excep.Code = "204";
                excep.Message = "No data found";
                response.ExceptionReply = excep;
            }
            return Task.FromResult(response);
        }
        public override Task<GetBrtaOfficeReply> GetBrtaOffice(Empty req, ServerCallContext context) {
            GetBrtaOfficeReply response = new GetBrtaOfficeReply();
            var data = _datalist.GetBrtaOffice();
            if (data.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                         cfg.CreateMap<Model.Datalist.BrtaOffice, BrtaOffice>()
                     );
                var mapper = new Mapper(config);
                foreach (var brtaOffice in data)
                {
                    response.BrtaOffice.Add((mapper.Map<BrtaOffice>(brtaOffice)));
                }
            }
            else
            {
                ExceptionReply excep = new ExceptionReply();
                excep.Code = "204";
                excep.Message = "No data found";
                response.ExceptionReply = excep;
            }
            return Task.FromResult(response);
        }
        public override Task<GetLocationReply> GetLocation(Empty req,ServerCallContext context) {
            GetLocationReply response = new GetLocationReply();
            
            var data = _datalist.GetLocation();
            if (data.Count>0)
            {
                var config = new MapperConfiguration(cfg =>
                         cfg.CreateMap<Model.Datalist.Location, Location>()
                     );
                var mapper = new Mapper(config);
                foreach (var location in data)
                {
                    response.Location.Add((mapper.Map<Location>(location)));
                }
            }
            else {
                ExceptionReply excep = new ExceptionReply();
                excep.Code = "204";
                excep.Message = "No data found";
                response.ExceptionReply = excep;
            }
           
           return Task.FromResult(response);
        }
      
    }
}
