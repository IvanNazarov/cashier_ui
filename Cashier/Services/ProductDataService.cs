using Cashier.Model;
using Cashier.Persistence;
using Cashier.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cashier.Services
{
    public class ProductDataService : IProductDataService
    {
        public ProductDataService()
        {
        }
        public async Task<IEnumerable<ProductTableViewRow>> GetAllAsync()
        {

            return await Task.Run(() => {
                using (var ctx = new CashierDataContext())
                {
                    return (from prod in ctx.Products.AsNoTracking()
                            join sq in (from sl in ctx.Saldo
                                        group sl by sl.ProductId into g
                                        select new
                                        {
                                            ProductId = g.Key,
                                            SumDt = g.Sum(x => x.Debt),
                                            SumKt = g.Sum(x => x.Credt)
                                        }) on prod.Id equals sq.ProductId into withSaldo
                            from c in withSaldo
                            select new ProductTableViewRow
                            {
                                Id = prod.Id,
                                IsWeight = prod.IsWeight,
                                MeasureName = prod.Measure.Name,
                                MeasureId = prod.Measure.Id,
                                Name = prod.Name,
                                Price = prod.Price,
                                Quantity = c.SumDt - c.SumKt
                            }).ToList();
                }
            });
        }
        public async Task<IEnumerable<ProductTableViewRow>>
            GetAsync(Expression<Func<Product, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");

            return await Task.Run(() => {
                using (var ctx = new CashierDataContext())
                {
                    var products = ctx.Products
                        .AsNoTracking()
                        .Where(filter);

                    return (from prod in products
                            join sq in (from sl in ctx.Saldo
                                        group sl by sl.ProductId into g
                                        select new
                                        {
                                            ProductId = g.Key,
                                            SumDt = g.Sum(x => x.Debt),
                                            SumKt = g.Sum(x => x.Credt)
                                        }) on prod.Id equals sq.ProductId into jt
                            from c in jt
                            select new ProductTableViewRow
                            {
                                Id = prod.Id,
                                IsWeight = prod.IsWeight,
                                MeasureName = prod.Measure.Name,
                                MeasureId = prod.Measure.Id,
                                Name = prod.Name,
                                Price = prod.Price,
                                Quantity = 1,
                                RemainderOfDebt = c.SumDt - c.SumKt
                            }).ToList();
                }
            });

            //return await Task.Run(() =>
            //{
            //    using (var ctx = new CashierDataContext())
            //    {
            //        return ctx.Products
            //        .AsNoTracking()
            //        .Where(filter)
            //        .Select(
            //        p => new ProductTableViewRow
            //        {
            //            Id = p.Id,
            //            IsWeight = p.IsWeight,
            //            MeasureName = p.Measure.Name,
            //            MeasureId = p.Measure.Id,
            //            Name = p.Name,
            //            Price = p.Price,
            //            Quantity = 1
            //        })
            //        .ToList();
            //    }
            //}
            //);
        }

        public async Task UpdateSaldoByRange(IEnumerable<ProductTableViewRow> list)
        {
            using (var ctx = new CashierDataContext())
            {
                var opDate = DateTime.Now;
                var saldo = from p in list
                            select new Saldo
                            {
                                ProductId = p.Id,
                                DocumentId = 1,
                                Credt = p.Quantity,
                                OperationDate = opDate,
                                Debt = 0
                            };
                ctx.Saldo.AddRange(saldo);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
