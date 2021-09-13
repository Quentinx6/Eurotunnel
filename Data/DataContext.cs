using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using API.Models;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options){}

        public DbSet<ITAccount> ITAccounts { get; set; }
    }
}