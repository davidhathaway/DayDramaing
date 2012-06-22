using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace DayDramaing.Domain.Models
{
    public class DayDramaingDBContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
      
        public DbSet<WebContent> WebContents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DayDramaingInitialiser 
        :
#if DEBUG
        DropCreateDatabaseAlways<DayDramaingDBContext> 
#else
        CreateDatabaseIfNotExists<DayDramaingDBContext> 
#endif
    {
        public DayDramaingInitialiser()
        {
        }

        protected override void Seed(DayDramaingDBContext context)
        {

            context.WebContents.Add(new WebContent() { Name = "Intro", RawHTML = "Intro" });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
