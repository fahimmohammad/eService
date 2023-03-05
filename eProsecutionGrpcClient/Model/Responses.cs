using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

namespace eProsecutionGrpcClient.Model
{
    public class Responses
    {
        public record LoginResponse(string code,string message,string loginName,string name,string id,string divisionName, string token);
        public record ProsecutionCode(string id,string code,List<Comment> comments);
        public record Comment(string cid,string comment);
        public record BrtaOffice(string id, string code);
        public record BrtaSeries(string id, string name);
        public record Location(string id,string locationName);
        public record SeizedDocument(string id,string shrotName);
        public record ExceptionReply(string code, string message);
        public record GetProsecutionCodeReply(string code, string message,List<ProsecutionCode> prosecutionList);
        public record GetBrtaOfficeReply(string code, string message,List<BrtaOffice> brtaOfficeList);
        public record GetBrtaSeriesReply(string code, string message, List<BrtaSeries> brtaSeriesList);
        public record GetLocationReply(string code, string message, List<Location> locationList);
        public record GetSeizedDocumentReply(string code, string message, List<SeizedDocument> seizedDocumentList);
        public record CaseProfile(string prosecutorId, string caseId,string accusedPersonName, string accusedPersonFather, string mobileNumber, string address, string vehicleNo, List<CaseProsecutions> caseProsecutions, string location, string seizedDocuments,string dateOfOffense,string lastDateOfPayment,string witness, string amount);
        public record CaseProsecutions(string prosecution, RepeatedField<string> comment);
        public record CaseEntryResponse(string code, string message, CaseProfile casePorfile);

    }
}
