using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ManicureEmCasaApi.Models;

namespace ManicureEmCasaApi.Controllers
{
    public class ServicesController : ApiController
    {
        private ManicureEmCasaContext db = new ManicureEmCasaContext();

        // GET: api/services
        public IQueryable<Service> GetServices()
        {
            return db.MyServices;
        }
    }
}