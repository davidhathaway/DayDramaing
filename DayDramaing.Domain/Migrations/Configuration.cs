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

            var user =  new User()
            {
                Username = adminName, 
                Role=adminRole, 
                Email = "info@daydrama-ing.co.uk", 
                Password = PasswordHash.HashPassword("daydrama-ing_FFGGHH"), 
                LastUpdatePassword = DateTime.Now 
            };
            context.Users.AddOrUpdate(x=>x.Username, user );

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
