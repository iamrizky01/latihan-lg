using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LatihanLG.Models;

namespace LatihanLG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsContext _context;

        public TransactionsController(TransactionsContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactions()
        {
            var transactions = await _context.Transactions.Include(t => t.customer).Include(t => t.food).ToListAsync();
            var transactionList = transactions.Select(t => new
            {
                transactionId = t.transactionId,
                customerId = t.customerId,
                customer = new
                {
                    customerId = t.customer.customerId,
                    customerName = t.customer.customerName,
                    email = t.customer.email,
                    phoneNumber = t.customer.phoneNumber
                },
                foodId = t.foodId,
                food = new
                {
                    foodId = t.food.foodId,
                    foodName = t.food.foodName,
                    price = t.food.price,
                    stock = t.food.stock
                },
                qty = t.qty,
                totalPrice = t.totalPrice,
                transactionDate = t.transactionDate
            });
            return Ok(transactionList);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transactions>> GetTransactions(int id)
        {
            var transactions = await _context.Transactions
                .Include(t => t.customer)
                .Include(t => t.food)
                .FirstOrDefaultAsync(t => t.transactionId == id);

            if (transactions == null)
            {
                return NotFound();
            }

            var transaction = new
            {
                transactionId = transactions.transactionId,
                customerId = transactions.customerId,
                customer = new
                {
                    customerId = transactions.customer.customerId,
                    customerName = transactions.customer.customerName,
                    email = transactions.customer.email,
                    phoneNumber = transactions.customer.phoneNumber
                },
                foodId = transactions.foodId,
                food = new
                {
                    foodId = transactions.food.foodId,
                    foodName = transactions.food.foodName,
                    price = transactions.food.price,
                    stock = transactions.food.stock
                },
                qty = transactions.qty,
                totalPrice = transactions.totalPrice,
                transactionDate = transactions.transactionDate
            };

            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactions(int id, Transactions transactions)
        {
            if (id != transactions.transactionId)
            {
                return BadRequest();
            }

            _context.Entry(transactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Transactions>> PostTransactions(Transactions transactions)
        {
            _context.Transactions.Add(transactions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactions", new { id = transactions.transactionId }, transactions);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transactions>> DeleteTransactions(int id)
        {
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactions);
            await _context.SaveChangesAsync();

            return transactions;
        }

        private bool TransactionsExists(int id)
        {
            return _context.Transactions.Any(e => e.transactionId == id);
        }
    }
}
