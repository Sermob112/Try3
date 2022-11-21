namespace Try3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(),
                        mark = c.String(),
                        users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.users_Id)
                .Index(t => t.users_Id);
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(),
                        placeId = c.Int(),
                        carId = c.Int(),
                        created = c.DateTime(),
                        quantity = c.Int(),
                        cars_id = c.Int(),
                        users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.cars", t => t.cars_id)
                .ForeignKey("dbo.places", t => t.placeId)
                .ForeignKey("dbo.AspNetUsers", t => t.users_Id)
                .Index(t => t.placeId)
                .Index(t => t.cars_id)
                .Index(t => t.users_Id);
            
            CreateTable(
                "dbo.places",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        price = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cars", "users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.orders", "users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.orders", "placeId", "dbo.places");
            DropForeignKey("dbo.orders", "cars_id", "dbo.cars");
            DropIndex("dbo.orders", new[] { "users_Id" });
            DropIndex("dbo.orders", new[] { "cars_id" });
            DropIndex("dbo.orders", new[] { "placeId" });
            DropIndex("dbo.cars", new[] { "users_Id" });
            DropTable("dbo.places");
            DropTable("dbo.orders");
            DropTable("dbo.cars");
        }
    }
}
