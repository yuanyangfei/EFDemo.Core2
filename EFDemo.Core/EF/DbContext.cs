using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using EFDemo.Core.Entity;

namespace EFDemo.Core.EF
{
    /// <summary>
    /// EF访问数据库的接口   
    /// </summary>
    public class MyDbContext : System.Data.Entity.DbContext
    {        
        public MyDbContext()
            : base("EFDemo")
        {
            //解决团队开发中，多人迁移数据库造成的修改覆盖问题。
            Database.SetInitializer<MyDbContext>(null);
            //base.Configuration.AutoDetectChangesEnabled = false;
            ////关闭EF6.x 默认自动生成null判断语句
            //base.Configuration.UseDatabaseNullSemantics = true;           
        }    
        public MyDbContext(System.Data.Common.DbConnection oConnection)
            : base(oConnection, true)
        {
            this.Configuration.LazyLoadingEnabled = true;         
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //表名不用复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //限制长度
            modelBuilder.Entity<T_Classes>().Property(c => c.Money).HasPrecision(18, 4);


            base.OnModelCreating(modelBuilder);
        }
        //将实体对象写在这里，就可以生成对应的数据。 如下：
        //public DbSet<Demo> Demo { get; set; }

        public DbSet<Public_Area> Public_Area { get; set; }
        public DbSet<T_Classes> T_Classes { get; set; }
        public DbSet<T_Student> T_Student { get; set; }

    }


}
