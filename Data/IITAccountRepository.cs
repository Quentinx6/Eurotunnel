using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface IITAccountRepository
    {
        Task<ITAccount>Register(ITAccount iTAccount);
        Task<List<ITAccount>>GetAllAccounts();
    }
}