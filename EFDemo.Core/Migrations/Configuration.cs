namespace EFDemo.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFDemo.Core.EF.MyDbContext>
    {
        public Configuration()
        {
            //允许自动迁移
            //不然会报错Unable to update database to match the current model because there are pending changes and automatic migration is disabled. Either write the pending model changes to a code-based migration or enable automatic migration. Set DbMigrationsConfiguration.AutomaticMigrationsEnabled to true to enable automatic migration.You can use the Add-Migration command to write the pending model changes to a code-based migration.

            //允许自动迁移
            AutomaticMigrationsEnabled = true;
            //自动迁移默认情况下不扔掉列在我们的数据库中的表。如果我们不希望这样的行为，我们可以告诉迁移明确允许数据丢失的配置类的AutomaticMigrationDataLossAllowed属性设置为true。
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EF.MyDbContext context)
        {
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
