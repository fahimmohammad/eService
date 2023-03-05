using eProsecutionGrpc;

namespace eProsecutionGrpcServer.DAO
{
    public interface IProsecutor
    {
        public Prosecutor ProsecutroLogin(string id,string password);
    }
}
