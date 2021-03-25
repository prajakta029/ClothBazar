namespace ClothBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donors",
                c => new
                    {
                        donarId = c.Int(nullable: false, identity: true),
                        donorUsername = c.String(),
                        donorAddress = c.String(),
                        itemsNumber = c.Int(nullable: false),
                        dDescription = c.String(),
                    })
                .PrimaryKey(t => t.donarId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Donors");
        }
    }
}
