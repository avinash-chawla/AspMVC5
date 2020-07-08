namespace Vidly3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "MembershipTypeId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Customers", "MembershipTypeId", c => c.Byte(nullable: false));
        }
    }
}
