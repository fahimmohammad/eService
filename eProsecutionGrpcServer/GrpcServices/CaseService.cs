using AutoMapper;
using eProsecutionGrpc;
using eProsecutionGrpcServer.DAO;
using Google.Protobuf.Collections;
using Grpc.Core;


namespace eProsecutionGrpcServer.GrpcServices
{
    public class CaseService:CaseProfileGrpc.CaseProfileGrpcBase
    {
        private readonly ILogger<CaseService> _logger;
        private readonly ICaseProfile _casePorifle;

        public CaseService(ILogger<CaseService> logger, ICaseProfile casePorifle)
        {
            _logger = logger;
            _casePorifle = casePorifle;
        }


        public override Task<CaseProfileResponse> CaseEntry(CaseProfileReq req, ServerCallContext context) {
            CaseProfileResponse response = new CaseProfileResponse();
            var data = _casePorifle.CaseEntry(req);
            if (data!=null)
            {
               /* CaseProfile caseProfile = new CaseProfile();
                caseProfile.ProsecutorId = data.ProsecutorId;
                caseProfile.CaseId = Int64.Parse(data.CaseId);
                caseProfile.Amount = data.amount.ToString();
                caseProfile.VehicleNo = data.vehicleNo;
                caseProfile.BrtaZoneId = data.brtaZoneId;
                caseProfile.SeriesId = data.seriesId;
                caseProfile.AccusedPersonName = data.accusedPersonName;
                caseProfile.AccusedPersonFather = data.accusedPersonFather;
                caseProfile.AddressLine1 = data.addressLine_1;
                caseProfile.SeizedDocuments = data.seizedDocuments;
                var config = new MapperConfiguration(cfg =>
                         cfg.CreateMap<CaseProsecutions, eProsecutionGrpc.CaseProsecutions>()
                     );
                var mapper = new Mapper(config);
                foreach (var casePros in data.caseProsecutions) {
                    caseProfile.CaseProsecutions.Add(mapper.Map<eProsecutionGrpc.CaseProsecutions>(casePros));
                }*/
                response.CaseProfile = data;
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
    }
}
