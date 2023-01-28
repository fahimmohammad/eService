using System.ComponentModel.DataAnnotations;

namespace eProsecutionGrpcServer.Model
{
    public class Customer
    {
        [Key]
        public long Id {get;set;}
        [Required]
        public string Name {get;set;}
    }
}
