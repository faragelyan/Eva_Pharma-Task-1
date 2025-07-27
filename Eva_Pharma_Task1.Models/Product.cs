using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva_Pharma_Task1.Models
{
    public class Product
    {
        //[Key]
        public int Id { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string Title { get; set; }
        //[MaxLength(250)]
        public string Description { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string Author { get; set; }

        //[Required]
        [Range(1, 1000)]
        //[Column("BookPrice")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Categories category { get; set; }

    }
}
