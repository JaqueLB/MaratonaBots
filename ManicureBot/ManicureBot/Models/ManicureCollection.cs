using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManicureBot.Models
{
    public class ManicureCollection
    {
        public IEnumerable<Manicure> manicures { get; set; }
    }
}