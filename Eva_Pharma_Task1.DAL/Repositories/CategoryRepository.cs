using Eva_Pharma_Task1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.DAL.Repositories
{
    public class CategoryRepository(AppDbContext _appDbContext) : ICategoryRepository
    {
        /*private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }*/

        public async Task<List<Categories>> GetAllAsync()
        {
            return await _appDbContext.Categories.OrderBy(c=>c.catOrder).ThenBy(c=>c.catName)
                .ToListAsync();
        }

        public async Task<Categories?> GetCategoryAsync(int id)
        {
            return await _appDbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Categories category)
        {
            await _appDbContext.Categories.AddAsync(category);
        }

        public Task UpdateAsync(Categories category)
        {
            _appDbContext.Categories.Update(category);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.markedAsDeleted = true;
                _appDbContext.Categories.Update(category);
            }
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }



    }
}
