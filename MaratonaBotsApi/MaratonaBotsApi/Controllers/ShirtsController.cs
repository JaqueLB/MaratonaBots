using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MaratonaBotsApi.Models;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaratonaBotsApi.Controllers
{
    [Route("api/shirts")]
    public class ShirtsController : Controller
    {
        private readonly ShirtsContext _context;
        // inserting data into database
        public ShirtsController(ShirtsContext context)
        {
            _context = context;

            if (_context.Shirts.Count() == 0)
            {
                _context.Shirts.Add(new Shirt
                {
                    Size = "pp",
                    DrawingPrice = 20.00,
                    DrawingTextPrice = 22.50,
                });
                _context.Shirts.Add(new Shirt
                {
                    Size = "p",
                    DrawingPrice = 25.00,
                    DrawingTextPrice = 27.50,
                });
                _context.Shirts.Add(new Shirt
                {
                    Size = "m",
                    DrawingPrice = 30.00,
                    DrawingTextPrice = 32.95,
                });
                _context.Shirts.Add(new Shirt
                {
                    Size = "g",
                    DrawingPrice = 35.00,
                    DrawingTextPrice = 37.95,
                });
                _context.Shirts.Add(new Shirt
                {
                    Size = "gg",
                    DrawingPrice = 40.00,
                    DrawingTextPrice = 43.00,
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Shirt> GetAll()
        {
            return _context.Shirts.ToList();
        }

        [HttpGet("{size}", Name = "GetBySize")]
        public IActionResult GetBySize(string size)
        {
            var item = _context.Shirts.FirstOrDefault(t => t.Size == size);
            if (item == null)
            {
                return NotFound();
            }
            return new Microsoft.AspNetCore.Mvc.ObjectResult(item);
        }
    }
}
