using eProsecutionGrpcClient.Model;
using eProsecutionGrpcClient.Services;
using eProsecutionGrpcClient;
using Microsoft.AspNetCore.Mvc;
using Grpc.Core;
using eProsecutionGrpc;

namespace eProsecutionGrpcClient.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _iconfig;
        public HomeController(IConfiguration iconfig) {
            _iconfig = iconfig;
        }
       [HttpPost]
        public ProsecutorLoginReply Login(LogReq req) {
            ProsecutorClient pc = new ProsecutorClient(_iconfig);
            return pc.ProsecutorLogin(req.userid, req.password);
        }

       [HttpGet]
        public GetLocationReply GetLocation() {
            DatalistClient cs = new DatalistClient(_iconfig);
            return cs.GetLocation();
        }
        [HttpGet]
        public GetBrtaOfficeReply GetBrtaOffice()
        {
            DatalistClient cs = new DatalistClient(_iconfig);
            return cs.GetBrtaOffice();
        }
        [HttpGet]
        public GetSeizedDocumentReply GetSeizedDocument()
        {
            DatalistClient cs = new DatalistClient(_iconfig);
            return cs.GetSeizedDocument();
        }
        [HttpPost]
        public GetProsecutionCodeReply GetProsecutionCode(string userid)
        {
            DatalistClient cs = new DatalistClient(_iconfig);
            return cs.GetProsecutionCode(userid);
        }
    }
}
