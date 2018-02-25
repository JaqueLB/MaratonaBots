using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaratonaBotsApi.Models
{
    public class ShirtsContext : DbContext
    {
        public ShirtsContext(DbContextOptions<ShirtsContext> options)
            : base(options)
        {
        }

        public DbSet<Shirt> Shirts { get; set; }
    }
}
