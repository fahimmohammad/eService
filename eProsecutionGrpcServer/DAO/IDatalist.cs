using static eProsecutionGrpcServer.Model.Datalist;

namespace eProsecutionGrpcServer.DAO
{
    public interface IDatalist
    {
        public List<Datalist.ProsecutionCode> GetProsecutionCode(string userid);
        public List<Datalist.SeizedDocument> GetSeizedDocument();
        public List<Datalist.BrtaOffice> GetBrtaOffice();
        public List<Datalist.Location> GetLocation();
    }
}