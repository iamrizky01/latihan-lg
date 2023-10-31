using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatihanLG.Models
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext (DbContextOptions<TransactionsContext> options) : base(options)
        {

        }

        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>()
                .HasOne(t => t.customer)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.customerId);

            modelBuilder.Entity<Transactions>()
                .HasOne(t => t.food)
                .WithMany(f => f.Transactions)
                .HasForeignKey(t => t.foodId);
        }
    }
}
