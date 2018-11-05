using FreeSpace.Data.Model;

namespace FreeSpace.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FreeSpaceContext : DbContext
    {
        // Your context has been configured to use a 'FreeSpaceContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FreeSpace.Data.FreeSpaceContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FreeSpaceContext' 
        // connection string in the application configuration file.
        public FreeSpaceContext() : base()
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<FileThumbPrint> FileThumbPrints { get; set; }
        public virtual DbSet<FileInstance> FileInstances { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}