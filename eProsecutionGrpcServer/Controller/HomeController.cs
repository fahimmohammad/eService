using eProsecutionGrpcServer.DAO;
using Microsoft.AspNetCore.Mvc;

namespace eProsecutionGrpcServer.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
       /* private readonly IProsecutor _prosecutor;
        public HomeController(IProsecutor prosecutor) {
            _prosecutor = prosecutor;
        }*/
        [HttpGet]
        public string Index()
        {
            /*Prosecutor re = new Prosecutor();
           re =  _prosecutor.ProsecutroLogin("8917203417", "1780");
            return re;*/
            return "Hello";
        }
    }
}
