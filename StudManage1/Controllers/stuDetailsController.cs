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
    public class stuDetailsController : Controller
    {
        private StudentManagement1Entities db = new StudentManagement1Entities();

        // GET: stuDetails
        public ActionResult Index()
        {
            var stuDetails = db.stuDetails.Include(s => s.branch).Include(s => s.company);
            return View(stuDetails.ToList());
        }

        // GET: stuDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stuDetail stuDetail = db.stuDetails.Find(id);
            if (stuDetail == null)
            {
                return HttpNotFound();
            }
            return View(stuDetail);
        }


        // GET: stuDetails/Create
        [Authorize(Roles = "Admin,Student")]
        public ActionResult Create()
        {
            ViewBag.branchId = new SelectList(db.branches, "bid", "b_name");
            ViewBag.cid = new SelectList(db.companies, "cid", "cname");
            return View();
        }

        // POST: stuDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Student")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sid,branchId,rollNo,name,age,address,phoneNo,DOB,gender,city,cid")] stuDetail stuDetail)
        {
            if (ModelState.IsValid)
            {
                db.stuDetails.Add(stuDetail);
                result r1 = new result();
                r1.id = stuDetail.sid;
                db.results.Add(r1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.branchId = new SelectList(db.branches, "bid", "b_name", stuDetail.branchId);
            ViewBag.cid = new SelectList(db.companies, "cid", "cname", stuDetail.cid);
            return View(stuDetail);
        }
        [Authorize(Roles = "Admin,PO")]
        // GET: stuDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stuDetail stuDetail = db.stuDetails.Find(id);
            if (stuDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.branchId = new SelectList(db.branches, "bid", "b_name", stuDetail.branchId);
            ViewBag.cid = new SelectList(db.companies, "cid", "cname", stuDetail.cid);
            return View(stuDetail);
        }

        // POST: stuDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,PO")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sid,branchId,rollNo,name,age,address,phoneNo,DOB,gender,city,cid")] stuDetail stuDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stuDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branchId = new SelectList(db.branches, "bid", "b_name", stuDetail.branchId);
            ViewBag.cid = new SelectList(db.companies, "cid", "cname", stuDetail.cid);
            return View(stuDetail);
        }
        [Authorize(Roles = "Admin")]
        // GET: stuDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stuDetail stuDetail = db.stuDetails.Find(id);
            if (stuDetail == null)
            {
                return HttpNotFound();
            }
            return View(stuDetail);
        }
        [Authorize(Roles = "Admin")]
        // POST: stuDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stuDetail stuDetail = db.stuDetails.Find(id);
            db.stuDetails.Remove(stuDetail);
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
