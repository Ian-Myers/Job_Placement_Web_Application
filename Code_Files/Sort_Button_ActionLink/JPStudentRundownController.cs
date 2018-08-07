using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPDash.ViewModels;
using JPDash.Models;
using System.Data.Entity;
using System.Text.RegularExpressions;

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


            var students = from s in db.JPStudents
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JPStudentRundown/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JPStudentRundown/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JPStudentRundown/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
