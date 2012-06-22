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

            var content = context.WebContents.ToList();
            var homeIntro = content.FirstOrDefault(x => x.Name == "HomeIntro");
            if (homeIntro == null)
            {
                context.WebContents.Add(new WebContent() { Name = "HomeIntro", RawHTML = "<p>Intro</p>" });
            }

            var HomeTitle = content.FirstOrDefault(x => x.Name == "HomeTitle");
            if (HomeTitle == null)
            {
                context.WebContents.Add(new WebContent() { Name = "HomeTitle", RawHTML = "Home Title" });
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
