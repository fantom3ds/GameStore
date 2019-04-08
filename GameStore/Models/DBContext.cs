using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Models
{
    public class DBContext:DbContext
    {
        public DBContext():base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}