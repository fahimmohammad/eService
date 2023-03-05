using AutoMapper;
using eProsecutionGrpc;
using eProsecutionGrpcClient.Model;
using Google.Protobuf.Collections;
using Grpc.Net.Client;
using System.Threading.Tasks;
using static eProsecutionGrpcClient.Model.Requests;
using static eProsecutionGrpcClient.Model.Responses;
using CaseProsecutions = eProsecutionGrpcClient.Model.Requests.CaseProsecutions;

namespace eProsecutionGrpcClient.Services
{
    public class CaseServiceClient:CaseProfileGrpc.CaseProfileGrpcClient
    {
        private readonly GrpcChannel channel;
        CaseProfileGrpc.CaseProfileGrpcClient client;
        private readonly string address;


        public CaseServiceClient(IConfiguration iconfig)
        {
            address = iconfig.GetValue<string>("GrpcServerAddress");
            channel = GrpcChannel.ForAddress(address);
            client = new CaseProfileGrpc.CaseProfileGrpcClient(channel);
        }
        public CaseProfileResponse CaseEntry(CaseEntryReq req)
        {
            var config = new MapperConfiguration(cfg =>
                          cfg.CreateMap<CaseProsecutions,eProsecutionGrpc.CaseProsecutions>()
                      );
            var mapper = new Mapper(config);
            RepeatedField<eProsecutionGrpc.CaseProsecutions> prosecutions = new RepeatedField<eProsecutionGrpc.CaseProsecutions>();
            foreach (var k in req.caseProsecutions) {
                prosecutions.Add((mapper.Map<eProsecutionGrpc.CaseProsecutions>(k)));
            }
            CaseProfileReq request = new CaseProfileReq();
            request.VehicleNo = req.vehicleNo;
            request.BrtaZoneId = req.brtaZoneId;
            request.SeriesId = req.seriesId;
            request.AccusedPersonName = req.accusedPersonName;
            request.AccusedPersonFather = req.accusedPersonFather;
            request.AddressLine1 = req.addressLine_1;
            request.AddressLine2 = req.addressLine_2;
            request.MobileNumber = req.mobileNumber;
            request.CaseLocation = req.caseLocation;
            request.Comments1 = req.comments_1;
            request.Comments2 = req.comments_2;
            request.ProsecutorId = req.prosecutorId;
            request.DateOfOffense = req.dateOfOffense;
            request.CaseStatus = req.caseStatus;
            request.SeizedDocuments = req.seizedDocuments;
            request.CaseProsecutions.Add(prosecutions);
            request.Witness = req.witness;
           var reply = client.CaseEntry(request);

            return reply;
        }

    }
}
