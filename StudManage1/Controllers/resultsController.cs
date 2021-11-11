using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudManage1.Models;

namespace StudManage1.Controllers
{
    [Authorize]
    public class resultsController : Controller
    {
        private StudentManagement1Entities db = new StudentManagement1Entities();

        // GET: results
        [Authorize(Roles = "Teacher,Student")]
        public ActionResult Index()
        {
            var results = db.results.Include(r => r.stuDetail);
            return View(results.ToList());
        }

        // GET: results/Details/5
        [Authorize(Roles = "Teacher,Student")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: results/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.stuDetails, "sid", "rollNo");
            return View();
        }

        // POST: results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "sid,id,m1,m2,m3,total,grade")] result result)
        {
            if (ModelState.IsValid)
            {
                db.results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.stuDetails, "sid", "rollNo", result.id);
            return View(result);
        }
        [Authorize(Roles = "Teacher")]
        // GET: results/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.stuDetails, "sid", "rollNo", result.id);
            return View(result);
        }

        // POST: results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sid,id,m1,m2,m3,total,grade")] result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.stuDetails, "sid", "rollNo", result.id);
            return View(result);
        }
        [Authorize(Roles = "Teacher")]
        // GET: results/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }
        [Authorize(Roles = "Teacher")]
        // POST: results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            result result = db.results.Find(id);
            db.results.Remove(result);
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
