using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManicureApi.Models
{
    public class Service
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ServicePrices Price { get; set; }
    }

    public enum ServicePrices
    {
        Manicure = 25,
        Pedicure = 25,
        Completo = 40
    }
}