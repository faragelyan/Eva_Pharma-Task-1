using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eva_Pharma_Task1.Models
{
    internal class ProductFormViewModel
    {
        public Product Product { get; set; }

        public List<CategoryItem> Categories { get; set; }

        public class CategoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
