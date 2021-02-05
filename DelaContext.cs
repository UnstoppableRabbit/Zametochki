using System.Data.Entity;

namespace Sp1sok_del
{
    class DelaContext : DbContext
    {
        public DbSet<Delas> Delas { get; set; }
        public DelaContext() : base("DefaultConnection")
        {
        }
       
    }
  
}
