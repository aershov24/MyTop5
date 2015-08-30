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
using MyTop5.Models;

namespace MyTop5.Controllers
{
    [Authorize]
    public class Top5ListItemController : ApiController
    {
        private mytop5apiContext db = new mytop5apiContext();

        // GET: api/Top5ListItem
        public IQueryable<Top5ListItem> GetTop5ListItem()
        {
            return db.Top5ListItem;
        }

        // GET: api/Top5ListItem/5
        [ResponseType(typeof(Top5ListItem))]
        public IHttpActionResult GetTop5ListItem(int id)
        {
            Top5ListItem top5ListItem = db.Top5ListItem.Find(id);
            if (top5ListItem == null)
            {
                return NotFound();
            }

            return Ok(top5ListItem);
        }

        // PUT: api/Top5ListItem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTop5ListItem(int id, Top5ListItem top5ListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != top5ListItem.Top5ListItemId)
            {
                return BadRequest();
            }

            db.Entry(top5ListItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Top5ListItemExists(id))
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

        // POST: api/Top5ListItem
        [ResponseType(typeof(Top5ListItem))]
        public IHttpActionResult PostTop5ListItem(Top5ListItem top5ListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Top5ListItem.Add(top5ListItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = top5ListItem.Top5ListItemId }, top5ListItem);
        }

        // DELETE: api/Top5ListItem/5
        [ResponseType(typeof(Top5ListItem))]
        public IHttpActionResult DeleteTop5ListItem(int id)
        {
            Top5ListItem top5ListItem = db.Top5ListItem.Find(id);
            if (top5ListItem == null)
            {
                return NotFound();
            }

            db.Top5ListItem.Remove(top5ListItem);
            db.SaveChanges();

            return Ok(top5ListItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Top5ListItemExists(int id)
        {
            return db.Top5ListItem.Count(e => e.Top5ListItemId == id) > 0;
        }
    }
}