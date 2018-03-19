namespace ManicureApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ManicureApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ManicureApi.Models.ManicureApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ManicureApi.Models.ManicureApiContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Appointments.AddOrUpdate(x => x.Id,
                new Appointment() { Id = 1, Name = "Jane Chang", Phone = "19 9999-9999", Email = "janechang@example.com", DayTime = new DateTime(2018, 3, 15, 9, 50, 0), PaymentType = PaymentTypes.Money, Service = ServiceTypes.Manicure, Place = "Rua 1, 125, Jd. do Sol, Campinas - SP" },
                new Appointment() { Id = 2, Name = "Jane Chang", Phone = "19 9999-9999", Email = "janechang@example.com", DayTime = new DateTime(2018, 4, 30, 10, 50, 0), PaymentType = PaymentTypes.DebitCard, Service = ServiceTypes.Pedicure, Place = "Rua 1, 125, Jd. do Sol, Campinas - SP" },
                new Appointment() { Id = 3, Name = "Jane Chang", Phone = "19 9999-9999", Email = "janechang@example.com", DayTime = new DateTime(2018, 3, 20, 13, 50, 0), PaymentType = PaymentTypes.Money, Service = ServiceTypes.Completo, Place = "Rua 1, 125, Jd. do Sol, Campinas - SP" }
            );
            context.Services.AddOrUpdate(x => x.Id,
                new Service() { Id = 1, Name = "Manicure", Price = ServicePrices.Manicure },
                new Service() { Id = 2, Name = "Pedicure", Price = ServicePrices.Pedicure },
                new Service() { Id = 3, Name = "Completo - Manicure e Pedicure", Price = ServicePrices.Completo }
            );
        }
    }
}
