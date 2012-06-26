namespace DayDramaing.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DayDramaing.Domain.Models;
    using Innovations.Core.Security;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<DayDramaing.Domain.Models.DayDramaingDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
         
        }

        protected override void Seed(DayDramaing.Domain.Models.DayDramaingDBContext context)
        {
            var adminName = "Administrator";
            
            var adminPermission = new Permission() { PermissionName = adminName };
            context.Permissions.AddOrUpdate(x => x.PermissionName, adminPermission);

            var adminRole = new Role() { RoleName = adminName };
            adminRole.Permissions.Add(adminPermission);

            context.Roles.AddOrUpdate(x => x.RoleName, adminRole);

            var user = context.Users.Where(x => x.Username == adminName).FirstOrDefault();
            if (user == null)
            {
                user = new User()
                {
                    Username = adminName,
                    Role = adminRole,
                    Email = "info@daydrama-ing.co.uk",
                    Password = PasswordHash.HashPassword("daydrama-ing_FFGGHH"),
                    LastUpdatePassword = DateTime.Now
                };
                context.Users.Add(user);
            }
            var content = context.WebContents.ToList();
            var homeIntro = content.FirstOrDefault(x => x.Name == "Home Intro");
            if (homeIntro == null)
            {
                context.WebContents.Add(new WebContent() { Name = "Home Intro", RawHTML = "<p>Intro</p>" });
            }
            else
            {
                homeIntro.Name = "Home Intro";
            }

            var homeTitle = content.FirstOrDefault(x => x.Name == "Home Title");
            if (homeTitle == null)
            {
                context.WebContents.Add(new WebContent() { Name = "Home Title", RawHTML = "Home Title" });
            }
            else
            {
                homeTitle.Name = "Home Title";
            }

            var deleteIntro = content.Where(x => x.Name == "Intro").FirstOrDefault();
            if (deleteIntro != null)
            {
                context.WebContents.Remove(deleteIntro);
            }


            context.SaveChanges();

            base.Seed(context);
        }
    }
}
