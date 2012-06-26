namespace DayDramaing.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    //Add-Migration 201206261058194_AddLastUpdatePassword
    public partial class AddLastUpdatePassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users", "LastUpdatePassword", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Users", "LastUpdatePassword");
        }
    }
}
