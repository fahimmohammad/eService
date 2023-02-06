namespace eProsecutionGrpcServer.DAO
{
    public interface IProsecutor
    {
        public Model.Prosecutor ProsecutroLogin(string id,string password);
    }
}
