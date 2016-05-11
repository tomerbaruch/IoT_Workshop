namespace iot_pub_website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class measurement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Measurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Device_id = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                        value = c.Int(nullable: false),
                        time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Measurements");
        }
    }
}
