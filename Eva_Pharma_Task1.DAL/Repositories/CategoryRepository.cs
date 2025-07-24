using Eva_Pharma_Task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        AppDbContext _appDbContext;
        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //Get All categories
        public List<Categories> GetAll()
        {
            return _appDbContext.Categories.ToList();
        }


        // Get Specific Category
        public Categories GetCategory(int id)
        {
            return _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
        }


        // Add Category
        public void Add(Categories category)
        {
            _appDbContext.Add(category);
        }

        
        // Update Specific Category
        public void Update(Categories category)
        {
            _appDbContext.Update(category);
        }


        // Delete Category
        public void Delete(int id)
        {
            var category=_appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
                _appDbContext.Categories.Remove(category);
        }


        // Save Execution
        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        
    }
}
