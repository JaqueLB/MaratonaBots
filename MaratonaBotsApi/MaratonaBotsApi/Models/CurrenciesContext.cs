using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaratonaBotsApi.Models
{
    public class CurrenciesContext : DbContext
    {
        public CurrenciesContext(DbContextOptions<CurrenciesContext> options)
            : base(options)
        {
        }

        public DbSet<CurrencyItem> CurrenciesItems { get; set; }
    }
}
