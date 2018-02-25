namespace ManicureEmCasaApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ManicureEmCasaApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ManicureEmCasaApi.Models.AppointmentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ManicureEmCasaApi.Models.AppointmentContext context)
        {
            context.Appointments.AddOrUpdate(x => x.Id,
            new Appointment() { Id = 1, Name = "Jane Chang", Phone = "19 9999-9999", Email = "janechang@example.com", DayTime = new DateTime(2018, 3, 15, 9, 50, 0), PaymentType = PaymentTypes.Money, Service = Services.Manicure, Place = "Rua 1, 125, Jd. do Sol, Campinas - SP" },
            new Appointment() { Id = 1, Name = "Jane Chang", Phone = "19 9999-9999", Email = "janechang@example.com", DayTime = new DateTime(2018, 4, 30, 10, 50, 0), PaymentType = PaymentTypes.DebitCard, Service = Services.Pedicure, Place = "Rua 1, 125, Jd. do Sol, Campinas - SP" },
            new Appointment() { Id = 1, Name = "Jane Chang", Phone = "19 9999-9999", Email = "janechang@example.com", DayTime = new DateTime(2018, 3, 20, 13, 50, 0), PaymentType = PaymentTypes.Money, Service = Services.Completo, Place = "Rua 1, 125, Jd. do Sol, Campinas - SP" }
        );
        }
    }
}