﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ManicureApi.Models;

namespace ManicureApi.Controllers
{
    public class ServicesController : ApiController
    {
        private ManicureApiContext db = new ManicureApiContext();

        // GET: api/Services
        public IQueryable<Service> GetServices()
        {
            return db.Services;
        }

        // GET: api/Services/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> GetService(long id)
        {
            Service service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutService(long id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Services
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Services.Add(service);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> DeleteService(long id)
        {
            Service service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            db.Services.Remove(service);
            await db.SaveChangesAsync();

            return Ok(service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceExists(long id)
        {
            return db.Services.Count(e => e.Id == id) > 0;
        }
    }
}