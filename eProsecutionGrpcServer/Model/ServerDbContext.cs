using eProsecutionGrpc;
using Microsoft.EntityFrameworkCore;

namespace eProsecutionGrpcServer.Model
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options):base(options) { }
        //public DbSet<Customer> Customer { get; set; }
       public DbSet<Prosecutor> Prosecutor { get; set; }
    }


  
}
