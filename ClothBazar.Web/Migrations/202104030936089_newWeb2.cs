﻿namespace ClothBazar.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newWeb2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Mobile");
        }
    }
}
