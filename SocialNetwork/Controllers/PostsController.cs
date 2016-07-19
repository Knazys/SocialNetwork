using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
    }

    public class PostsController : BaseController
    {

        // Вывод списка постов
        public ActionResult Index()
        {
            return View(db.Posts.Include("PostType").Include("Comments").ToList());
        }

        // Подробности поста
        public ActionResult Details(int? p)
        {
            if (p == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(p);
            if (post == null)
            {
                return HttpNotFound();
            }

            post.Comments = db.Comments.Where(x => x.PostId == post.Id).ToList();
            return View(post);
        }

        // Получение формы на создание поста
        public ActionResult Create()
        {
            var PostTypes = db.PostTypes;
            var PostTypesList = new SelectList(PostTypes, "Id", "Name");
            ViewBag.PostTypesList = PostTypesList;

            var post = new Post();
            return View(post);
        }

        // POST: /Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.DateCreated = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: /Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Content,DateCreated")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: /Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult Like(int id)
        {
            bool status = true;
            string errorMessage = "";
            int count = 0;

            try
            {
                var post = db.Posts.Find(id);
                if(post == null)
                {
                    throw new Exception("Пост в базе данных не найден");
                }

                post.Likes++;
                count = post.Likes;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }

            return Json(new { status, errorMessage, count });
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
