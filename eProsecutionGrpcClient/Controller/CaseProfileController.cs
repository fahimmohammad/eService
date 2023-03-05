using AutoMapper;
using eProsecutionGrpc;
using eProsecutionGrpcClient.Model;
using eProsecutionGrpcClient.Services;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Mvc;

namespace eProsecutionGrpcClient.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CaseProfileController : ControllerBase
    {
        private readonly IConfiguration _iconfig;
        public CaseProfileController(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }
        [HttpPost]
        public IActionResult CaseEntry(Requests.CaseEntryReq req)
        {
            CaseServiceClient cs = new CaseServiceClient(_iconfig);
            var data = cs.CaseEntry(req);
            var caseProfileReply = data.CaseProfile;

            var config = new MapperConfiguration(cfg =>
                         cfg.CreateMap<CaseProsecutions,Responses.CaseProsecutions>()
                     );
            var mapper = new Mapper(config);
            List<Responses.CaseProsecutions> prosecutions = new List<Responses.CaseProsecutions>();
            foreach (var prosecution in data.CaseProfile.CaseProsecutions){
                prosecutions.Add((mapper.Map<Responses.CaseProsecutions>(prosecution)));
            }
            var casePorfile = new Responses.CaseProfile(prosecutorId: caseProfileReply.ProsecutorId, caseId: caseProfileReply.CaseId, accusedPersonName: caseProfileReply.AccusedPersonName, accusedPersonFather: caseProfileReply.AccusedPersonFather, mobileNumber: caseProfileReply.MobileNumber, address: caseProfileReply.Address, vehicleNo: caseProfileReply.VehicleNo, caseProsecutions: prosecutions, location: caseProfileReply.CaseLocation, seizedDocuments: caseProfileReply.SeizedDocuments, dateOfOffense: caseProfileReply.DateOfOffense, lastDateOfPayment: caseProfileReply.LastDateofPayment, witness: caseProfileReply.Witness, amount: caseProfileReply.Amount);
            return Ok(new Responses.CaseEntryResponse(code: "200", message: "Successfull", casePorfile: casePorfile));
        }
    }
}
