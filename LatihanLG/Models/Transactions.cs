using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LatihanLG.Models
{
    public class Transactions
    {
        [Key]
        public int transactionId { get; set; }
        public int customerId { get; set; }

        [JsonIgnore]
        public Customer customer { get; set; }
        public int foodId { get; set; }

        [JsonIgnore]
        public Food food { get; set; }

        public int qty { get; set; }
        public int totalPrice { get; set; }
        public DateTime transactionDate { get; set; }
    }
}
