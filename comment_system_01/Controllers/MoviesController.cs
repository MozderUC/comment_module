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
using Microsoft.AspNet.Identity;
using comment_system_01.Models.Account;
using System.IO;

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
        
            string st1r = this.User.Identity.GetUserId();
            //Type tp = movie.GetType();
            //var someVar = Convert.ChangeType(movie, tp);
            //var someVar = Activator.CreateInstance(tp);
            //
            //someVar = db.Set(tp).Find(id);sd
        
            var str = movie.Comments.
                            Select(p => new
                            {
                                p.CommentID,
                                p.Context,
                                p.Parent,
                                p.Created,
                                p.Modified,
                                p.Upvote_count,
                                p.User.UserName,
                                created_by_current_user = (p.User.Id == this.User.Identity.GetUserId() ? true : false),
                                user_has_upvoted = p.Upvotes.Where(u => u.UserID.Equals(this.User.Identity.GetUserId())).Any()                               
                            });
            return JsonConvert.SerializeObject(str);
        
        }

       

        public string PostComment([Bind(Include = "MovieID,Context,Parent,Created,Modified")] Comment comment)
        {
            if(this.User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    comment.UserID = db.Users.Find(this.User.Identity.GetUserId()).Id;
                    db.Comment.Add(comment);
                    db.SaveChanges();
                }
                return JsonConvert.SerializeObject(new { comment.CommentID, comment.UserID, comment.User.UserName, comment.Context, comment.Parent, comment.Created, comment.Modified, success = true });
            }
            return JsonConvert.SerializeObject(new { success = false});
            
            
        }
        
        [Authorize]
        public void UpvoteComment(int? id)
        {
            Comment comment = db.Comment.Find(id);                   
            Upvote Upvote = comment.Upvotes.Where(item => item.UserID.Equals(this.User.Identity.GetUserId())).FirstOrDefault();
        
            if (Upvote==null)
            {
                comment.Upvote_count = comment.Upvote_count + 1;
                db.Entry(comment).State = EntityState.Modified;
                db.Upvote.Add(new Upvote { CommentID = comment.CommentID, UserID = this.User.Identity.GetUserId(), Created = DateTime.Now });
                db.SaveChanges();               
            }
            else
            {
                comment.Upvote_count = comment.Upvote_count - 1;
                db.Entry(comment).State = EntityState.Modified;
                db.Upvote.Remove(Upvote);
                db.SaveChanges();                      
            }
                   
        }

        [HttpPost]
        public string UploadAttachments(Image image)
        {

            HttpPostedFileBase file = this.Request.Files[0];
         

            if (file != null && file.ContentLength > 0)
            {
                string fname = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/images", fname)));

                image.FileName = file.FileName;
                image.ImageSize = file.ContentLength;

                byte[] data = new byte[file.ContentLength];
                file.InputStream.Read(data, 0, file.ContentLength);
                //string imageBase64Data = Convert.ToBase64String(data);
                //string imageDataURL = string.Format("data:image/jpeg;base64,{0}", imageBase64Data);

                image.ImageData = data;

                ImageUrl imageUrl = new ImageUrl();
                imageUrl.imageUrl = Path.Combine(Request.ApplicationPath, "/images/", fname);

                image.ImageUrl = imageUrl;

                db.Image.Add(image);
                db.SaveChanges();
                //Path.Combine(Request.ApplicationPath, "/images/Kolkata-in-pictures.jpg")
                //return JsonConvert.SerializeObject(new { imageUrl = (Path.Combine("~/App_Data/images", Path.GetFileName(file.FileName))) });
                return JsonConvert.SerializeObject(new { fileUrl = Path.Combine(Request.ApplicationPath, "/images/Kolkata-in-pictures.jpg") });

            }
            return "Hellow";
          
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
