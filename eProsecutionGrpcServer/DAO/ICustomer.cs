namespace eProsecutionGrpcServer.DAO
{
    public interface ICustomer
    {
        public List<Model.Customer> GetCustomer();
        public Model.Customer GetCustomerById(long id);
    }
}
