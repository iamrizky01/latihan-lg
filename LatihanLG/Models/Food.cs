using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LatihanLG.Models
{
    public class Food
    {
        [Key]
        public int foodId { get; set; }
        public string foodName { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public List<Transactions> Transactions { get; set; }

    }
}
