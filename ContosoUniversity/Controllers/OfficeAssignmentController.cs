using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class OfficeAssignmentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: OfficeAssignment
        public ActionResult Index()
        {
            var officeAssignments = db.OfficeAssignments.Include(o => o.Instructor);
            return View(officeAssignments.ToList());
        }

        // GET: OfficeAssignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssignment);
        }

        // GET: OfficeAssignment/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName");
            return View();
        }

        // POST: OfficeAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorID,Location")] OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                db.OfficeAssignments.Add(officeAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // GET: OfficeAssignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // POST: OfficeAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorID,Location")] OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officeAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // GET: OfficeAssignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssignment);
        }

        // POST: OfficeAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfficeAssignment officeAssignment = db.OfficeAssignments.Find(id);
            db.OfficeAssignments.Remove(officeAssignment);
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
