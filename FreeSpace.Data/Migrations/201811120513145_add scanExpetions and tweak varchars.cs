namespace FreeSpace.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addscanExpetionsandtweakvarchars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScanExceptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionType = c.Int(nullable: false),
                        Path = c.String(maxLength: 1024),
                        ShortDesc = c.String(maxLength: 250),
                        ExceptionName = c.String(),
                        Message = c.String(),
                        Source = c.String(),
                        HResult = c.Int(nullable: false),
                        InnerExceptionMessage = c.String(),
                        StackTrace = c.String(),
                        CreatedStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScanExceptions");
        }
    }
}
