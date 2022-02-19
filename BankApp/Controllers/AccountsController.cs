#nullable disable 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApp.Models;

namespace BankApp.Controllers
{
    [Route("api/Accounts")]  
    [ApiController] 
    public class AccountsController : ControllerBase
    {
        private readonly AccountsContext _context;

        public AccountsController(AccountsContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountsDTO>>> GetAccounts()
        {
            return await _context.Accounts
                .Select(x => AccountsDTO(x))
                .ToListAsync(); 
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accounts>> GetAccounts(long id)
        {
            var accounts = await _context.Accounts.FindAsync(id);

            if (accounts == null)
            {
                return NotFound();
            }

            return accounts;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccounts(long id, Accounts accounts)
        {
            if (id != accounts.Id)
            {
                return BadRequest();
            }

            _context.Entry(accounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountsDTO>> CreateAccounts(AccountsDTO accountsDTO)
        {
            var accounts = new Accounts
            {
                IsComplete = accountsDTO.IsComplete,
                Name = accountsDTO.Name
            };

            _context.Accounts.Add(accounts);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAccounts", new { id = accounts.Id }, accounts);
            return CreatedAtAction(nameof(GetAccounts), new { id = accounts.Id }, accounts);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccounts(long id)
        {
            var accounts = await _context.Accounts.FindAsync(id);
            
            if (accounts == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accounts);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountsExists(long id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
        private static AccountsDTO AccountsDTO(Accounts accounts) =>
            new AccountsDTO
            {
                Id = accounts.Id,
                Name = accounts.Name,
                IsComplete = accounts.IsComplete
            };
    }
}