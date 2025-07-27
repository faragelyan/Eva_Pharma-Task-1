using Eva_Pharma_Task1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.DAL.Repositories
{
    public class ProductRepository(AppDbContext _appDbContext) : IProductRepository
    {
        public async Task<List<Product>> GetAllAsync()
        {
            return await _appDbContext.Products
                .Include(p => p.category)
                .ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _appDbContext.Products
                .Include(p => p.category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task AddAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            _appDbContext.Products.Update(product);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            _appDbContext.Products .Remove(product);
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
