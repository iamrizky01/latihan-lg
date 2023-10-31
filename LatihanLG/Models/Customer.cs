using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LatihanLG.Models
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public List<Transactions> Transactions { get; set; }
    }
}
