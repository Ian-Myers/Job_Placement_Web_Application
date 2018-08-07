using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPDash.ViewModels;
using JPDash.Models;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Net;

namespace JPDash.Controllers
{
    public class JPStudentRundownController : Controller
    {

        private JPDashTableContext db = new JPDashTableContext();



        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "location_desc" : "Location";
            ViewBag.GraduateSortParm = sortOrder == "Graduate" ? "graduate_desc" : "Graduate";


            List<JPStudentRundown> jPStudentRundownList = new List<JPStudentRundown>();


            var students = from s in db.JPStudents where s.JPHired == false //setting the logic here so we can skip the whole passing of the object into the list, and then not have to look at it in the view at all. Before it would still have to hit all the information below
                                                                            //where as now it will skip the following and not even be there in the view
                           select s;
                           


            if (!String.IsNullOrEmpty(searchString))
            {
                string searchStringNoSpaces = Regex.Replace(searchString, @"\s+", "");
                students = students.Where(s => s.JPStudentLocation.ToString().Contains(searchString) || s.JPStudentLocation.ToString().Contains(searchStringNoSpaces) || s.JPName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Graduate":
                    students = students.Where(s => (s.JPApplications.Count() >= 30));
                    break;
                case "graduate_desc":
                    students = students.Where(s => (s.JPApplications.Count() >= 30));
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.JPName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.JPStartDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.JPStartDate);
                    break;
                case "Location":
                    students = students.OrderBy(s => s.JPStudentLocation);
                    break;
                case "location_desc":
                    students = students.OrderByDescending(s => s.JPStudentLocation);
                    break;
                default: //Name ascending
                    students = students.OrderBy(s => s.JPName);
                    break;
            }


            
            foreach (var student in students)
            {
                var applicationCount = db.JPApplications.Where(a => a.JPStudentId == student.JPStudentId).Count();
                var dateCriteria = DateTime.Now.AddDays(-7);
                var thisWeek = db.JPApplications.Where(a => a.JPStudentId == student.JPStudentId && a.JPApplicationDate >= dateCriteria).Count();
                var jPStudent = new JPStudentRundown(student, applicationCount, thisWeek);
                jPStudentRundownList.Add(jPStudent);
            }


          





            return View(jPStudentRundownList);
        }


        // GET: JPStudentRundown/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JPStudentRundown/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JPStudentRundown/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JPStudentRundown/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPStudent jpstudent = db.JPStudents.Find(id);
            JPStudentRundown jPStudentRundown = new JPStudentRundown(jpstudent);
            if (jPStudentRundown == null)
            {
                return HttpNotFound();
            }
            return View(jPStudentRundown);
        }

        // POST: JPStudentRundown/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPStudentId,JPName,JPEmail,JPStudentLocation,JPStartDate,JPLinkedIn,JPPortfolio,JPContact")] JPStudent jPStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jPStudent);
        }

        // GET: JPStudentRundown/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPStudent jpstudent = db.JPStudents.Find(id);
            JPStudentRundown jPStudentRundown = new JPStudentRundown(jpstudent);
            if (jPStudentRundown == null)
            {
                return HttpNotFound();
            }
            return View(jPStudentRundown);
        }

        // POST: JPStudentRundown/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPStudent jPStudent = db.JPStudents.Find(id);
            db.JPStudents.Remove(jPStudent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JPStudentRundown Find(int id)//this is to create a JPStudentRundown object out of a primary key for a JPStudent, I used this logic to hook up the Delete and the Edit method calls and don't like repeating it a bunch so I put it into
            //a method down here.
        {
            var student = db.JPStudents.Find(id);
            var applicationCount = db.JPApplications.Where(a => a.JPStudentId == student.JPStudentId).Count();
            var dateCriteria = DateTime.Now.AddDays(-7);
            var thisWeek = db.JPApplications.Where(a => a.JPStudentId == student.JPStudentId && a.JPApplicationDate >= dateCriteria).Count();
            var jPStudent = new JPStudentRundown(student, applicationCount, thisWeek);
            return jPStudent;
        }
    }
}
