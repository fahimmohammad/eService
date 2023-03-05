using eProsecutionGrpc;
using eProsecutionGrpcClient.Model;
using eProsecutionGrpcClient.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static eProsecutionGrpcClient.Model.Requests;

namespace eProsecutionGrpcClient.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProsecutorController : ControllerBase
    {
        private readonly IConfiguration _iconfig;
        public ProsecutorController(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Test() {
            return Ok(new { message = "Hello" });
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginRequest req)
        {
            ProsecutorClient pc = new ProsecutorClient(_iconfig);
            try {
                var reply = pc.ProsecutorLogin(req.userid, req.password);
                
                if (reply.ExceptionReply == null)
                {
                    Responses.LoginResponse resp = new Responses.LoginResponse(code:"200",message:"Login successfull",loginName: reply.Prosecutor.Name,name: reply.Prosecutor.Name, id: reply.Prosecutor.Id, divisionName: reply.Prosecutor.DivisionName,token: GenerateJSONWebToken(req));
                    return Ok(resp);
                }
                else {
                    Responses.LoginResponse resp = new Responses.LoginResponse(code: reply.ExceptionReply.Code.ToString(), message: reply.ExceptionReply.Message.ToString(), loginName: "", name:"", id: "", divisionName: "", token:"");
                    return Ok(resp);
                }
            }
            catch (Exception e) {
                return Ok(new { exception=e.Message});
            }
        }
        private string GenerateJSONWebToken(LoginRequest userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.userid),
                new Claim("IssueDate",DateTime.Now.ToString("dddd, MMMM dd yyyy")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_iconfig["Jwt:Issuer"],
              _iconfig["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
