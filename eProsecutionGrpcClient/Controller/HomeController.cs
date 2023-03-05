using eProsecutionGrpcClient.Model;
using eProsecutionGrpcClient.Services;
using eProsecutionGrpcClient;
using Microsoft.AspNetCore.Mvc;
using Grpc.Core;
using eProsecutionGrpc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
     
       /* [HttpGet]
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
        }*/
    }
}
