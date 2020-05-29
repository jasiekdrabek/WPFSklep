namespace Sklep
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Sklep.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public MyContext()
            : base("name=MyContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Dostawy> Dostawys { get; set; }
        public virtual DbSet<Transakcje> Transakcjes { get; set; }
        public virtual DbSet<Produkty> Produktys { get; set; }
        public virtual DbSet<Pracownicy> Pracownicys { get; set; }
        public virtual DbSet <Klienci> Kliencis { get; set; }
    }
}