using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/expenses
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/expenses
        // GET /api/expenses?categoryId=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseReadDto>>> GetExpenses([FromQuery] int? categoryId)
        {
            var query = _context.Expenses.Include(e => e.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryId == categoryId.Value);
            }

            var expenses = await query
                .OrderByDescending(e => e.Date)
                .Select(e => ToReadDto(e))
                .ToListAsync();

            return Ok(expenses);
        }

        // GET /api/expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseReadDto>> GetExpense(int id)
        {
            var expense = await _context.Expenses.Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound(new { message = $"Expense with id {id} was not found." });
            }

            return Ok(ToReadDto(expense));
        }

        // POST /api/expenses
        [HttpPost]
        public async Task<ActionResult<ExpenseReadDto>> CreateExpense(ExpenseCreateDto dto)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest(new { message = $"CategoryId {dto.CategoryId} does not exist." });
            }

            var expense = new Expense
            {
                Title = dto.Title,
                Amount = dto.Amount,
                Date = dto.Date ?? DateTime.UtcNow,
                Notes = dto.Notes,
                CategoryId = dto.CategoryId
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            // Reload with category for the response
            await _context.Entry(expense).Reference(e => e.Category).LoadAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, ToReadDto(expense));
        }

        // PUT /api/expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, ExpenseUpdateDto dto)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound(new { message = $"Expense with id {id} was not found." });
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest(new { message = $"CategoryId {dto.CategoryId} does not exist." });
            }

            expense.Title = dto.Title;
            expense.Amount = dto.Amount;
            expense.Date = dto.Date;
            expense.Notes = dto.Notes;
            expense.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent(); // 204 - standard REST response for a successful update
        }

        // DELETE /api/expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound(new { message = $"Expense with id {id} was not found." });
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET /api/expenses/summary
        [HttpGet("summary")]
        public async Task<ActionResult> GetSummary()
        {
            var summary = await _context.Expenses
                .Include(e => e.Category)
                .GroupBy(e => e.Category!.Name)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount),
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(summary);
        }

        private static ExpenseReadDto ToReadDto(Expense e) => new()
        {
            Id = e.Id,
            Title = e.Title,
            Amount = e.Amount,
            Date = e.Date,
            Notes = e.Notes,
            CategoryId = e.CategoryId,
            CategoryName = e.Category?.Name ?? string.Empty
        };
    }
}
