using Google.Protobuf.Collections;

namespace eProsecutionGrpcServer.Model {
    public record CaseEntryReq(string vehicleNo,string brtaZoneId,int seriesId,string accusedPersonName,string accusedPersonFather,string addressLine_1,string addressLine_2,string mobileNumber,string caseLocation,string comments_1,string comments_2,long prosecutorId,string dateOfOffense, int caseStatus,RepeatedField<CaseProsecutions> caseProsecutions, string seizedDocuments);

    public record CaseProsecutions(string prosecution,RepeatedField<string> comment);
    
    public record SeizedDocuments(string seizedDocumentId);
    public record CaseEntryResp (long prosecutorId,
        string caseId,
        int amount, 
        string vehicleNo,
        string brtaZoneId, 
        int seriesId, 
        string accusedPersonName, 
        string accusedPersonFather, 
        string addressLine_1,
        RepeatedField<CaseProsecutions> caseProsecutions,
        string seizedDocuments);
}