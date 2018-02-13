using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaratonaBotsApi.Models
{
    public class CurrencyItem
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
