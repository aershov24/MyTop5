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
using Microsoft.AspNet.Identity;

namespace MyTop5.Controllers
{
    [Authorize]
    public class Top5ListController : ApiController
    {
        private mytop5apiContext db = new mytop5apiContext();

        // GET: api/Top5List
        public IEnumerable<Top5List> GetTop5List()
        {
            var userId = User.Identity.GetUserId();

            return db.Top5List.Where(l => l.UserId == userId)
                .Include(p => p.Items).Include(p => p.Tags).AsEnumerable();
        }

        /*[Route("api/Users/{userId}/Top5Lists")]
        [HttpGet]
        public IEnumerable<Top5List> FindListsByUsers(int userId)
        {
            return db.Top5List.
                Where(p => p.UserId == userId);
        }*/

        [Route("api/Top5List/Search/{Tag1}")]
        [HttpGet]
        public IEnumerable<Top5List> GetListsByTags1(string Tag1)
        {
            TagComparer comparer = new TagComparer();

            return db.Top5List.
                Include(p => p.Items).
                Include(p => p.Tags).
                Where(p => p.Tags.Any(t => t.TagText.Contains(Tag1)));
        }

        [Route("api/Top5List/Search/{Tag1}/{Tag2}")]
        [HttpGet]
        public IEnumerable<Top5List> GetListsByTags2(string Tag1, string Tag2 = "")
        {
            TagComparer comparer = new TagComparer();

            return db.Top5List.
                Include(p => p.Items).
                Include(p => p.Tags).
                Where(p => p.Tags.Any(t => t.TagText.Contains(Tag1))).
                Where(p => p.Tags.Any(t => t.TagText.Contains(Tag2)));
        }

        [Route("api/Top5List/Search/{Tag1}/{Tag2}/{Tag3}")]
        [HttpGet]
        public IEnumerable<Top5List> GetListsByTags3(string Tag1, string Tag2 = "", string Tag3 = "")
        {
            TagComparer comparer = new TagComparer();

            return db.Top5List.
                Include(p => p.Items).
                Include(p => p.Tags).
                Where(p => p.Tags.Any(t => t.TagText.Contains(Tag1))).
                Where(p => p.Tags.Any(t => t.TagText.Contains(Tag2))).
                Where(p => p.Tags.Any(t => t.TagText.Contains(Tag3)));
        }

        [Route("api/Top5List/{top5listId}/Items")]
        [HttpGet]
        public IEnumerable<Top5ListItem> GetListItems(int top5listId)
        {
            return db.Top5ListItem.
                Where(p => p.Top5ListId == top5listId);
        }

        // GET: api/Top5List/5
        [ResponseType(typeof(Top5List))]
        public IHttpActionResult GetTop5List(int id)
        {
            Top5List top5List = db.Top5List.
                Include(p => p.Items).
                Include(p => p.Tags).
                First(p => p.Top5ListId == id);

            if (top5List == null)
            {
                return NotFound();
            }

            return Ok(top5List);
        }

        // PUT: api/Top5List/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTop5List(int id, Top5List top5List)
        {
            var strCurrentUserId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != top5List.Top5ListId)
            {
                return BadRequest();
            }

            db.Entry(top5List).State = EntityState.Modified;

            try
            {
                /*List<Tag> tags = top5List.Tags.ToList();
                var top5ItemInDb = db.Top5List.Include(c => c.Tags)
                    .Single(c => c.Top5ListId == id);

                // Remove all tags from top5list in DB
                foreach (var deltag in top5List.Tags.Where(t => t.TagId != 0))
                {
                    db.Tags.Remove(deltag);
                }*/

                db.SaveChanges();

                // Add all tags from top5list to DB
                /*foreach (var newtag in tags)
                    top5ItemInDb.Tags.Add(new Tag { TagText = newtag.TagText});

                db.SaveChanges();*/
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Top5ListExists(id))
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

        // POST: api/Top5List
        [ResponseType(typeof(Top5List))]
        public IHttpActionResult PostTop5List(Top5List top5List)
        {
            var strCurrentUserId = User.Identity.GetUserId();

            top5List.UserId = strCurrentUserId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Top5List.Add(top5List);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = top5List.Top5ListId }, top5List);
        }

        // DELETE: api/Top5List/5
        [ResponseType(typeof(Top5List))]
        public IHttpActionResult DeleteTop5List(int id)
        {
            Top5List top5List = db.Top5List.Find(id);
            if (top5List == null)
            {
                return NotFound();
            }

            db.Top5List.Remove(top5List);
            db.SaveChanges();

            return Ok(top5List);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Top5ListExists(int id)
        {
            return db.Top5List.Count(e => e.Top5ListId == id) > 0;
        }
    }
}