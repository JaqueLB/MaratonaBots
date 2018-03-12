using System;
using System.ComponentModel.DataAnnotations;

namespace ManicureEmCasaApi.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime DayTime { get; set; }
        [Required]
        public string Place { get; set; }
        public PaymentTypes PaymentType { get; set; }
        [Required]
        public ServiceTypes Service { get; set; }
    }

    public enum PaymentTypes
    {
        Money = 1,
        DebitCard,
    }

    public enum ServiceTypes
    {
        Manicure = 1,
        Pedicure,
        Completo,
    }
}