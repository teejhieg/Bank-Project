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
                .Select(x => CreateAccountsDTO(x))
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
            if (string.IsNullOrWhiteSpace(accountsDTO.FirstName) || string.IsNullOrWhiteSpace(accountsDTO.LastName))
            {
                return new BadRequestObjectResult("First name AND last name are required.");
            }

            var accounts = new Accounts
            {
                FirstName = accountsDTO.FirstName,
                LastName = accountsDTO.LastName,
                PhoneNumber = accountsDTO.PhoneNumber,
                DateOfBirth = accountsDTO.DateOfBirth
            };

            _context.Accounts.Add(accounts);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAccounts", new { id = accounts.Id }, accounts);
            return CreatedAtAction(nameof(GetAccounts), new { id = accounts.Id }, CreateAccountsDTO(accounts) );
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
        private static AccountsDTO CreateAccountsDTO(Accounts accounts) =>
            new AccountsDTO
            {
                Id = accounts.Id,
                FirstName = accounts.FirstName,
                LastName = accounts.LastName,
                PhoneNumber = accounts.PhoneNumber,
                DateOfBirth = accounts.DateOfBirth    
            };
    }
}