namespace DayDramaing.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DayDramaing.Domain.Models;

    public sealed class Configuration : DbMigrationsConfiguration<DayDramaing.Domain.Models.DayDramaingDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DayDramaing.Domain.Models.DayDramaingDBContext context)
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

            if (context.WebContents.Count() == 0)
            {
                context.WebContents.Add(new WebContent() { Name = "Intro", RawHTML = "Intro" });
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
