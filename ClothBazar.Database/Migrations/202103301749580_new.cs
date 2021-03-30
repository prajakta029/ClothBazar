namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "ProductID", "dbo.Products");
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropIndex("dbo.OrderItems", new[] { "ProductID" });
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        OrderedAt = c.DateTime(nullable: false),
                        Status = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.OrderItems", "ProductID");
            CreateIndex("dbo.OrderItems", "OrderID");
            AddForeignKey("dbo.OrderItems", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
        }
    }
}
