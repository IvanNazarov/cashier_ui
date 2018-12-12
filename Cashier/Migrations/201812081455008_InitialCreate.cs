namespace Cashier.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.measure",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                        full_name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "public.products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        MeasureId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        IsWeight = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.measure", t => t.MeasureId, cascadeDelete: true)
                .Index(t => t.MeasureId);
            
            CreateTable(
                "public.saldo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        DocumentId = c.Int(nullable: false),
                        Debt = c.Double(nullable: false),
                        Credt = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.DocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.saldo", "ProductId", "public.products");
            DropForeignKey("public.products", "MeasureId", "public.measure");
            DropIndex("public.saldo", new[] { "DocumentId" });
            DropIndex("public.saldo", new[] { "ProductId" });
            DropIndex("public.products", new[] { "MeasureId" });
            DropTable("public.saldo");
            DropTable("public.products");
            DropTable("public.measure");
        }
    }
}
