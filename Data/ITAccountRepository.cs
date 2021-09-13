using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models;


namespace API.Data
{
    public class ITAccountRepository : IITAccountRepository
    {
        private readonly DataContext _context;
        public ITAccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ITAccount> Register(ITAccount iTAccount)
        {
            await _context.ITAccounts.AddAsync(iTAccount);
            await _context.SaveChangesAsync();

            return iTAccount;
        }

        public async Task<List<ITAccount>> GetAllAccounts()
        {
            var accountsFromDb = await _context.ITAccounts.ToListAsync();

            return accountsFromDb;
        }
    }
}