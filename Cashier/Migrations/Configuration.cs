namespace Cashier.Migrations
{
    using Cashier.Model;

    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Cashier.Persistence.CashierDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Cashier.Persistence.CashierDataContext context)
        {
            context.Measures.AddOrUpdate(x => x.Id,
                new Measure { Id = 1, Name="шт.", FullName = "штука"},
                new Measure { Id = 2, Name="упак.", FullName = "упаковка"},
                new Measure { Id = 3, Name="кг."},
                new Measure { Id = 4, Name="бут.", FullName = "бутылка"}
            );

            context.Products.AddOrUpdate(x=> x.Id,
                new Product { Id = 1, MeasureId = 1, Name = "Кремлевская таблетка", Price = 1000000, IsWeight=false},
                new Product { Id = 2, MeasureId = 3, Name = "Аспирин", Price = 10, IsWeight=true},
                new Product { Id = 3, MeasureId = 2, Name = "Анальгин", Price = 189.9, IsWeight=false},
                new Product { Id = 4, MeasureId = 4, Name = "Настойка боярышника", Price = 16.7, IsWeight=false},
                new Product { Id = 5, MeasureId = 4, Name = "Асептолин", Price = 22.30, IsWeight=false},
                new Product { Id = 6, MeasureId = 1, Name = "Панадол", Price = 189.7, IsWeight=false},
                new Product { Id = 7, MeasureId = 1, Name = "Парацетомол", Price = 6235000.28, IsWeight=false},
                new Product { Id = 8, MeasureId = 1, Name = "Парацельсий", Price = 122.34, IsWeight=false}
            );
            var operationDate = DateTime.Now;
            context.Saldo.AddOrUpdate(x => x.Id,
                new Saldo { Id = 1, ProductId = 1, Debt = 1, DocumentId = 1, OperationDate = operationDate},
                new Saldo { Id = 2, ProductId = 2, Debt = 3, DocumentId = 1, OperationDate = operationDate},
                new Saldo { Id = 3, ProductId = 3, Debt = 2, DocumentId = 1, OperationDate = operationDate},
                new Saldo { Id = 4, ProductId = 4, Debt = 50, DocumentId = 1, OperationDate = operationDate},
                new Saldo { Id = 5, ProductId = 5, Debt = 25, DocumentId = 1, OperationDate = operationDate},
                new Saldo { Id = 6, ProductId = 6, Debt = 98, DocumentId = 1, OperationDate = operationDate},
                new Saldo { Id = 7, ProductId = 7, Debt = 1, DocumentId = 1, OperationDate = operationDate},
                new Saldo { Id = 8, ProductId = 8, Credt = 2, DocumentId = 2, OperationDate = operationDate}
            );
        }
    }
}
