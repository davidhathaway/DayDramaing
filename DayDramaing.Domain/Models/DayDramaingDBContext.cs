﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DayDramaing.Domain.Migrations;

namespace DayDramaing.Domain.Models
{
    public class DayDramaingDBContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }    
        public DbSet<WebContent> WebContents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DayDramaingInitialiser : MigrateDatabaseToLatestVersion<DayDramaingDBContext, Configuration>
    {
    }
}
