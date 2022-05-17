using MainApplication.BL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApplication.BL
{
    public class ApplicationContext : DbContext
    {
    
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<VM> VMs { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
    }
}
