using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MaratonaBotsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaratonaBotsApi.Controllers
{
    [Route("api/currencies")]
    public class CurrenciesController : Controller
    {
        private readonly CurrenciesContext _context;
        // inserting data into database
        public CurrenciesController(CurrenciesContext context)
        {
            _context = context;

            if (_context.CurrenciesItems.Count() == 0)
            {
                _context.CurrenciesItems.Add(new CurrencyItem {
                    Code = "USD",
                    Name = "Dólar",
                    Value = 2.35,
                });

                _context.CurrenciesItems.Add(new CurrencyItem
                {
                    Code = "EUR",
                    Name = "Euro",
                    Value = 3.90,
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<CurrencyItem> GetAll()
        {
            return _context.CurrenciesItems.ToList();
        }

        [HttpGet("{code}", Name = "GetCurrency")]
        public IActionResult GetByCode(string code)
        {
            var item = _context.CurrenciesItems.FirstOrDefault(t => t.Code == code);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}
