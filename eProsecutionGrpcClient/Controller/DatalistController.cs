using eProsecutionGrpc;
using eProsecutionGrpcClient.Model;
using eProsecutionGrpcClient.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static eProsecutionGrpcClient.Model.Responses;
using ProsecutionCode = eProsecutionGrpcClient.Model.Responses.ProsecutionCode;

namespace eProsecutionGrpcClient.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DatalistController : ControllerBase
    {
        private readonly IConfiguration _iconfig;
        public DatalistController(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }

        [HttpGet]
        public IActionResult GetLocation()
        {
            DatalistClient cs = new DatalistClient(_iconfig);
            var data  = cs.GetLocation();
            if (data.ExceptionReply == null && data.Location.Count > 0)
            {
                List<Responses.Location> locationList = new List<Responses.Location>();
                foreach (var row in data.Location)
                {
                    locationList.Add(new Responses.Location(id: row.Id, locationName: row.LocationName));
                }
                return Ok(new Responses.GetLocationReply(code: "200", message: "Successfull", locationList: locationList));
            }
            else {
                return Ok(new Responses.GetLocationReply(code: "204", message: "No data Found", locationList: null));
            }
        }
        [HttpGet]
        public IActionResult GetBrtaOffice()
        {
            DatalistClient cs = new DatalistClient(_iconfig);
            var data =  cs.GetBrtaOffice();
            if (data.ExceptionReply == null && data.BrtaOffice.Count>0)
            {
                List<Responses.BrtaOffice> brtaOfficeList = new List<Responses.BrtaOffice>();
                foreach (var row in data.BrtaOffice)
                {
                    brtaOfficeList.Add(new Responses.BrtaOffice(id: row.Id, code: row.Code));
                }
                return Ok(new Responses.GetBrtaOfficeReply(code: "200", message: "Successfull", brtaOfficeList: brtaOfficeList));
            }
            else {
                return Ok(new Responses.GetBrtaOfficeReply(code: "204", message: "No data found", brtaOfficeList:null));
            }
        }

        [HttpGet]
        public IActionResult GetBrtaSeries()
        {
            DatalistClient cs = new DatalistClient(_iconfig);
            var data = cs.GetBrtaSeries();
            if (data.ExceptionReply == null && data.BrtaSeries.Count > 0)
            {
                List<Responses.BrtaSeries> brtaSeriesList = new List<Responses.BrtaSeries>();
                foreach (var row in data.BrtaSeries)
                {
                    brtaSeriesList.Add(new Responses.BrtaSeries(id: row.Id, name: row.Name));
                }
                return Ok(new Responses.GetBrtaSeriesReply(code: "200", message: "Successfull", brtaSeriesList: brtaSeriesList));
            }
            else
            {
                return Ok(new Responses.GetBrtaSeriesReply(code: "204", message: "No data found", brtaSeriesList: null));
            }
        }
        [HttpGet]
        public IActionResult GetSeizedDocument()
        {
            DatalistClient cs = new DatalistClient(_iconfig);
            var data  = cs.GetSeizedDocument();
            if (data.ExceptionReply == null && data.SeizedDocument.Count > 0)
            {
                List<Responses.SeizedDocument> seizedDocumentList = new List<Responses.SeizedDocument>();
                foreach (var row in data.SeizedDocument)
                {
                    seizedDocumentList.Add(new Responses.SeizedDocument(id: row.Id, shrotName: row.Shortname));
                }
                return Ok(new Responses.GetSeizedDocumentReply(code: "200", message: "Successfull", seizedDocumentList: seizedDocumentList));
            }
            else {
                return Ok(new Responses.GetSeizedDocumentReply(code: "204", message: "No data Found", seizedDocumentList: null));
            }
            
        }
        [HttpPost]
        public IActionResult GetProsecutionCode(Requests.GetProsecutionCodeReq req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else {
                DatalistClient cs = new DatalistClient(_iconfig);
                var data = cs.GetProsecutionCode(req.userid);

                if (data.ExceptionReply == null)
                {
                    List<ProsecutionCode> prosecutionCodes = new List<ProsecutionCode>();
                    List<(string, string)> prosecutionIdList = new List<(string, string)>();
                    List<Comment> comments = new List<Comment>();
                    foreach (var row in data.ProsecutionCode)
                    {
                            if (prosecutionIdList.IndexOf((row.Id, row.Code)) == -1)
                            {
                                if (prosecutionIdList.Count > 0)
                                {
                                prosecutionCodes.Add(new ProsecutionCode(id: prosecutionIdList[prosecutionIdList.Count - 1].Item1, code: prosecutionIdList[prosecutionIdList.Count - 1].Item2, comments: comments));
                                }
                                comments = new List<Comment>();
                                prosecutionIdList.Add((row.Id, row.Code));
                                comments.Add(new Comment(cid: row.Cid, comment: row.Comment));
                            }
                            else
                            {
                                comments.Add(new Comment(cid: row.Cid, comment: row.Comment));
                            }
                    }
                    if (prosecutionIdList.Count > 0)
                    {
                        prosecutionCodes.Add(new ProsecutionCode(id: prosecutionIdList[prosecutionIdList.Count - 1].Item1, code: prosecutionIdList[prosecutionIdList.Count - 1].Item2, comments: comments));
                    }
                    return Ok(new Responses.GetProsecutionCodeReply(code: "200", message: "Successfull", prosecutionList: prosecutionCodes));
                }
                else
                {
                    return Ok(new Responses.GetProsecutionCodeReply(code: "204", message: "No data found", prosecutionList: null));
                }
            }
            
        }
    }
}
