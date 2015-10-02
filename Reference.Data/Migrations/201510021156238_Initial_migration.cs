namespace Reference.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kjøp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Opprettet = c.DateTimeOffset(precision: 7),
                        Endret = c.DateTimeOffset(precision: 7),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fornavn = c.String(),
                        Etternavn = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VareLinje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Antall = c.Int(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Vare_Id = c.Int(),
                        Kjøp_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vare", t => t.Vare_Id)
                .ForeignKey("dbo.Kjøp", t => t.Kjøp_Id)
                .Index(t => t.Vare_Id)
                .Index(t => t.Kjøp_Id);
            
            CreateTable(
                "dbo.Vare",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Isbn = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VareLinje", "Kjøp_Id", "dbo.Kjøp");
            DropForeignKey("dbo.VareLinje", "Vare_Id", "dbo.Vare");
            DropForeignKey("dbo.Kjøp", "Person_Id", "dbo.Person");
            DropIndex("dbo.VareLinje", new[] { "Kjøp_Id" });
            DropIndex("dbo.VareLinje", new[] { "Vare_Id" });
            DropIndex("dbo.Kjøp", new[] { "Person_Id" });
            DropTable("dbo.Vare");
            DropTable("dbo.VareLinje");
            DropTable("dbo.Person");
            DropTable("dbo.Kjøp");
        }
    }
}
