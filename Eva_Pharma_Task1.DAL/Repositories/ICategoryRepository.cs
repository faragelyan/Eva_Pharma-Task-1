using Eva_Pharma_Task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.DAL.Repositories
{
    public interface ICategoryRepository
    {
        List<Categories> GetAll();
        Categories GetCategory(int id);
        void Add(Categories category);
        void Update(Categories category);
        void Delete(int id);
        void Save();
    }
}
