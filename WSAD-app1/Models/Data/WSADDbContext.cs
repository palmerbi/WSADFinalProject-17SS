using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WSAD_app1.Models.Data
{
    public class WSADDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}