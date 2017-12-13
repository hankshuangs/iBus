using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Models.iAAA
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext() : base("name=MvcDbContext")
        {
        }

        //重要對應資料表
        private DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //資料表名不要自動變複數
        }
    }
}