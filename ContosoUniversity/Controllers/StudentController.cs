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
using PagedList;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student

        public ViewResult Index(string sortOrder, string searchString, string currentFilter,  int? page)
        {

            var students = from s in db.Students
                           select s;







            // Search
            // If statement  to Search in  Last Name  or First Name
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
            }









            // Sorting
            // viewBag  to  paging the page
            ViewBag.CurrentSort = sortOrder;

            // viewBag  to  sort by Last Name
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // viewBag  to  sort by Enrollment Date
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }





            // Paging
            // if  for paged list
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            

            // for paged list
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));




        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // Return  Error   bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Find   it's job is   take   id   and  bring   object   if there is no  id   bring    Null
            Student student = db.Students.Find(id);
            if (student == null)
            {
                // Return Error   NotFound
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // We use Bind   to create   "White List " to protect my site from Attacking
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,EnrollmentDate,ImagePath")] Student student , HttpPostedFileBase file)
        {
            try
            {
                // Is valid   refer to   it is OK  while insert  or there is no Error
                if (ModelState.IsValid)
                {

                    

                    if (file != null)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Students/") + file.FileName);
                        student.ImagePath = file.FileName;
                    }



                    // Add the Student  to   db.Students
                    db.Students.Add(student);
                    db.SaveChanges();
                    // Go back To   Index
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // ""   To post the message up the page  and    I can  put  "LastName"   instead of  ""  to show the Erro  
                ModelState.AddModelError("", ex.Message);
            }

            // Return view  With the data that you insertsd   without lost it
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // We use Bind   to create   "White List " to protect my site from Attacking
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate,ImagePath")] Student student, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {



                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Students/") + file.FileName);
                    student.ImagePath = file.FileName;
                }



                // Save  the  Edit
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
