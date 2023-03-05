using eProsecutionGrpc;

namespace eProsecutionGrpcServer.DAO
{
    public interface ICaseProfile
    {
        public CaseProfile CaseEntry(CaseProfileReq req);
    }
}
