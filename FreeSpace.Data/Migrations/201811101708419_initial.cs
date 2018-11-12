namespace FreeSpace.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileInstances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileThumbPrintId = c.Int(nullable: false),
                        FilePath = c.String(),
                        FileName = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        CreatedStamp = c.DateTime(nullable: false),
                        UpdatedStamp = c.DateTime(nullable: false),
                        IsHistory = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileThumbPrints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MD5 = c.String(),
                        FileSize = c.Long(nullable: false),
                        CreatedStamp = c.DateTime(nullable: false),
                        UpdatedStamp = c.DateTime(nullable: false),
                        IsHistory = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileThumbPrints");
            DropTable("dbo.FileInstances");
        }
    }
}
