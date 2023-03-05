using eProsecutionGrpc;
using Google.Protobuf.Collections;

namespace eProsecutionGrpcServer.DAO
{
    public interface IDatalist
    {
        public RepeatedField<ProsecutionCode> GetProsecutionCode(long userid);
        public RepeatedField<SeizedDocument> GetSeizedDocument();
        public RepeatedField<BrtaOffice> GetBrtaOffice();
        public RepeatedField<BrtaSeries> GetBrtaSeries();
        public RepeatedField<Location> GetLocation();
    }
}