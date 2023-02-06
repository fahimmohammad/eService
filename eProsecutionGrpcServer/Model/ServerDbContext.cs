using Microsoft.EntityFrameworkCore;
using static eProsecutionGrpcServer.Model.Datalist;

namespace eProsecutionGrpcServer.Model
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options):base(options) { }
        //public DbSet<Customer> Customer { get; set; }
       public DbSet<Prosecutor> Prosecutor { get; set; }
       /* public DbSet<Datalist.ProsecutionCode> ProsecutionCode { get; set; }
        public DbSet<Datalist.SeizedDocument> SeizedDocument { get; set; }
        public DbSet<Datalist.BrtaOffice> BrtaOffice { get; set; }*/
        public DbSet<Datalist.Location> Location { get; set; }
    }


  
}
