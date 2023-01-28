using eProsecutionGrpcServer.DAO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eProsecutionGrpcServer.DbService
{
    public class CustomerService : ICustomer
    {
        private readonly ServerDbContext _dbContext=null;
        public CustomerService(ServerDbContext dbContext) {
            _dbContext = dbContext;
        }
        public List<Model.Customer> GetCustomer()
        {
           var data =  _dbContext.Customer.FromSqlRaw("get_customers").ToList();
            return data;
        }

        public Model.Customer GetCustomerById(long id)
        {
            var param = new SqlParameter("@id",id);
    
            var data = _dbContext.Customer.FromSql($"EXECUTE get_customers {id}").ToList().FirstOrDefault();
            return data;
        }
    }
}
