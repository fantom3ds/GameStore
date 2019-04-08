namespace GameStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GameStore.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GameStore.Models.DBContext context)
        {

            context.Categories.AddOrUpdate(new Models.Category { Id = 1, CategoryName = "Race" });
            context.Categories.AddOrUpdate(new Models.Category { Id = 2, CategoryName = "Shooters" });
            context.Categories.AddOrUpdate(new Models.Category { Id = 3, CategoryName = "Strategy" });

            context.Roles.AddOrUpdate(new Models.Role { Id = 1, Name = "Admin" });
            context.Roles.AddOrUpdate(new Models.Role { Id = 2, Name = "User" });

            context.SaveChanges();
            
        }
    }
}
