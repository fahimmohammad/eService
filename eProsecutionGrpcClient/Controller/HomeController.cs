using eProsecutionGrpcClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace eProsecutionGrpcClient.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public Customer Get() {
            CustomerClient cs = new CustomerClient();
            return cs.GetCustomerById(3);
        }
    }
}
