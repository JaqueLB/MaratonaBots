using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaratonaBotsApi.Models
{
    public class Shirt
    {
        public long Id { get; set; }
        public string Size { get; set; }
        public double DrawingPrice { get; set; }
        public double DrawingTextPrice { get; set; }
    }
}
