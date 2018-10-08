using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using comment_system_01.DAL;
using comment_system_01.Models;
using Newtonsoft.Json;
namespace comment_system_01.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Movies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        
        public string GetComments(int? id)
        {
            Movie movie = db.Movies.Find(id);
            //Type tp = movie.GetType();
            //var someVar = Convert.ChangeType(movie, tp);
            //var someVar = Activator.CreateInstance(tp);
            //
            //someVar = db.Set(tp).Find(id);sd
            string str = JsonConvert.SerializeObject(movie.Comments.Select(p => new { p.CommentID, p.Context,p.Parent,p.Created,p.Modified,p.Upvote_count,p.User.Name }));         
            return str;

        }

        
        public string PostComment([Bind(Include = "MovieID,UserID,Context,Parent,Created,Modified")] Comment comment)
        {

            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
            }
            return JsonConvert.SerializeObject(new { comment.CommentID,comment.Context,comment.Parent,comment.Created,comment.Modified});
            
        }

        public void UpvoteComment(int? id)
        {
            Comment comment = db.Comment.Find(id);
            User user = db.User.Find(comment.UserID);

            Upvote Upvote = user.Upvotes.Where(item => item.CommentID.Equals(comment.CommentID)).FirstOrDefault();

            if (Upvote==null)
            {
                comment.Upvote_count = comment.Upvote_count + 1;
                db.Entry(comment).State = EntityState.Modified;
                db.Upvote.Add(new Upvote { CommentID = comment.CommentID, UserID = comment.UserID, Created = DateTime.Now });
                db.SaveChanges();               
            }
            else
            {
                comment.Upvote_count = comment.Upvote_count - 1;
                db.Entry(comment).State = EntityState.Modified;
                db.Upvote.Remove(Upvote);
                db.SaveChanges();
                //db.Upvote.

            }
                   
        }


        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
