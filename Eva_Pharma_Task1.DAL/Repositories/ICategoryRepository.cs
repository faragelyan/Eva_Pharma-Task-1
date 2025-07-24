using Eva_Pharma_Task1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.DAL.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Categories>> GetAllAsync();
        Task<Categories?> GetCategoryAsync(int id);
        Task AddAsync(Categories category);
        Task UpdateAsync(Categories category);
        Task DeleteAsync(int id);
        Task SaveAsync();


    }
}
