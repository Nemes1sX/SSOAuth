using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Tools;
using Microsoft.EntityFrameworkCore.Design;
using SSOauth.Models;

namespace SSOauth.Data
{
    public class SSOAuthContext : DbContext
    {
      

        public SSOAuthContext(DbContextOptions<SSOAuthContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
