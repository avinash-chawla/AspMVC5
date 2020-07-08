namespace Vidly3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) Values (1, 'Pay as You Go', 0, 0, 0)");
            Sql("Insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) Values (2, 'Monthly',30, 1, 10)");
            Sql("Insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) Values (3,'Quarterly', 90, 3, 10)");
            Sql("Insert into MembershipTypes (Id, Name, SignUpFee, DurationInMonths, DiscountRate) Values (4,'Annual', 300, 12, 20)");
        }
        
        public override void Down()
        {
        }
    }
}
