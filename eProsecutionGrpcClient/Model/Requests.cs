using Google.Protobuf.Collections;

namespace eProsecutionGrpcClient.Model
{
    public class Requests
    {
        public record LoginRequest(string userid,string password);
        public record GetProsecutionCodeReq(long userid);
        public record CaseProsecutions(string prosecution, RepeatedField<string> comment);
        public record CaseEntryReq(string vehicleNo, string brtaZoneId, int seriesId, string accusedPersonName, string accusedPersonFather, string addressLine_1, string addressLine_2, string mobileNumber, string caseLocation, string comments_1, string comments_2, long prosecutorId, string dateOfOffense, int caseStatus, RepeatedField<CaseProsecutions> caseProsecutions, string seizedDocuments,string witness);
    }
}
