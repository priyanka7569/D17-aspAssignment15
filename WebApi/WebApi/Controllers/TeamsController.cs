using System;
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
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class TeamsController : ApiController
    {
        private TeamsServiceContext db = new TeamsServiceContext();

        // GET: api/Teams
        public IQueryable<Teams> GetTeams()
        {
            return db.Teams;
        }

        // GET: api/Teams/5
        [ResponseType(typeof(Teams))]
        public async Task<IHttpActionResult> GetTeams(int id)
        {
            Teams teams = await db.Teams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }

            return Ok(teams);
        }

        // PUT: api/Teams/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeams(int id, Teams teams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teams.TId)
            {
                return BadRequest();
            }

            db.Entry(teams).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamsExists(id))
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

        // POST: api/Teams
        [ResponseType(typeof(Teams))]
        public async Task<IHttpActionResult> PostTeams(Teams teams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(teams);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = teams.TId }, teams);
        }

        // DELETE: api/Teams/5
        [ResponseType(typeof(Teams))]
        public async Task<IHttpActionResult> DeleteTeams(int id)
        {
            Teams teams = await db.Teams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }

            db.Teams.Remove(teams);
            await db.SaveChangesAsync();

            return Ok(teams);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamsExists(int id)
        {
            return db.Teams.Count(e => e.TId == id) > 0;
        }
    }
}