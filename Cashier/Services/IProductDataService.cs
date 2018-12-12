using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cashier.Model;
using Cashier.ViewModel;

namespace Cashier.Services
{
    public interface IProductDataService
    {
        Task<IEnumerable<ProductTableViewRow>> GetAllAsync();
        Task<IEnumerable<ProductTableViewRow>> GetAsync(Expression<Func<Product, bool>> filter);
        Task UpdateSaldoByRange(IEnumerable<ProductTableViewRow> list);
    }
}