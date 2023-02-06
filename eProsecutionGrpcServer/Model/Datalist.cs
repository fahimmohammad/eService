namespace eProsecutionGrpcServer.Model
{
    public class Datalist
    {
     public record ProsecutionCode(string id, string code);
     public record SeizedDocument(string id, string shortName);
     public record BrtaOffice(string id, string code);
     public record Location(string id, string locationName);

    }
}
