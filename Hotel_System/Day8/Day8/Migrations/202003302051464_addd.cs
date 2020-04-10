namespace Day8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RoomTypes", "Instock");
            DropColumn("dbo.RoomTypes", "Actual_Number");
            DropColumn("dbo.Rooms", "RoomIsReserved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "RoomIsReserved", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomTypes", "Actual_Number", c => c.Int(nullable: false));
            AddColumn("dbo.RoomTypes", "Instock", c => c.Int(nullable: false));
        }
    }
}
