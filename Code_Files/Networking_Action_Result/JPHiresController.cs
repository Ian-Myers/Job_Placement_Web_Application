using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JPDash.Models;

namespace JPDash.Controllers
{
    public class JPHiresController : Controller
    {
        private JPDashTableContext db = new JPDashTableContext();

        // GET: JPHires
        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.CompanySortParm = String.IsNullOrEmpty(sortOrder) ? "company_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.JobTitleSortParm = sortOrder == "JobTitle" ? "jobTitle_desc" : "JobTitle";
            ViewBag.JobCategorySortParm = sortOrder == "JobCategory" ? "jobCategory_desc" : "JobCategory";
            ViewBag.SalarySortParm = sortOrder == "Salary" ? "salary_desc" : "Salary";
            ViewBag.CompanyCitySortParm = sortOrder == "CompanyCity" ? "companyCity_desc" : "CompanyCity";
            ViewBag.CompanyStateSortParm = sortOrder == "CompanyState" ? "companyState_desc" : "CompanyState";

            var students = from s in db.JPStudents
                           select s;
            var hires = from s in db.JPHires.Include(j => j.JPStudent)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                hires = hires.Where(s => s.JPStudent.JPName.Contains(searchString)||s.JPCompanyName.Contains(searchString)||s.JPJobCategory.ToString().Contains(searchString)
                                       || s.JPCompanyCity.Contains(searchString) || s.JPCompanyState.ToString().Contains(searchString) || s.JPSalary.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.JPName);
                    break;
                default: //Name ascending
                    students = students.OrderBy(s => s.JPName);
                    break;

            }
            switch (sortOrder)
            {

                case "company_desc":
                    hires = hires.OrderByDescending(s => s.JPCompanyName);
                    break;

                //case name_desc needs to be included again even though referenced in above switch (sortOrder)

                case "name_desc":
                    hires = hires.OrderByDescending(s => s.JPStudent.JPName);
                    break;
                case "JobTitle":
                    hires = hires.OrderBy(s => s.JPJobTitle);
                    break;
                case "jobTitle_desc":
                    hires = hires.OrderByDescending(s => s.JPJobTitle);
                    break;
                case "JobCategory":
                    hires = hires.OrderBy(s => s.JPJobCategory);
                    break;
                case "jobCategory_desc":
                    hires = hires.OrderByDescending(s => s.JPJobCategory);
                    break;
                case "Salary":
                    hires = hires.OrderBy(s => s.JPSalary);
                    break;
                case "salary_desc":
                    hires = hires.OrderByDescending(s => s.JPSalary);
                    break;
                case "CompanyCity":
                    hires = hires.OrderBy(s => s.JPCompanyCity);
                    break;
                case "companyCity_desc":
                    hires = hires.OrderByDescending(s => s.JPCompanyCity);
                    break;
                case "CompanyState":
                    hires = hires.OrderBy(s => s.JPCompanyState);
                    break;
                case "companyState_desc":
                    hires = hires.OrderByDescending(s => s.JPCompanyState);
                    break;
                case "companyCareersPage_desc":
                    hires = hires.OrderByDescending(s => s.JPCareersPage);
                    break;

                default: //Company ascending

                    hires = hires.OrderBy(s => s.JPCompanyName);
                    break;


            }

            return View(hires.ToList());

    }

        // GET: JPHires/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPHire jPHire = db.JPHires.Find(id);
            if (jPHire == null)
            {
                return HttpNotFound();
            }
            return View(jPHire);
        }

        // GET: JPHires/Create
        public ActionResult Create()
        {
            ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName");
            return View();
        }

        // POST: JPHires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPHireId,JPStudentId,JPCompanyName,JPJobTitle,JPJobCategory,JPSalary,JPCompanyCity,JPCompanyState,JPSecondJob,JPCareersPage,JPHireDate")] JPHire jPHire)
        {
            if (ModelState.IsValid)
            {
                jPHire.JPHireId = Guid.NewGuid();
                jPHire.JPHireDate = DateTime.Now;
                db.JPHires.Add(jPHire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPHire.JPStudentId);
            return View(jPHire);
        }

        // GET: JPHires/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPHire jPHire = db.JPHires.Find(id);
            if (jPHire == null)
            {
                return HttpNotFound();
            }
            ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPHire.JPStudentId);
            return View(jPHire);
        }

        // POST: JPHires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPHireId,JPStudentId,JPCompanyName,JPJobTitle,JPJobCategory,JPSalary,JPCompanyCity,JPCompanyState,JPSecondJob,JPCareersPage,JPHireDate")] JPHire jPHire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPHire).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPHire.JPStudentId);
            return View(jPHire);
        }

        // GET: JPHires/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPHire jPHire = db.JPHires.Find(id);
            if (jPHire == null)
            {
                return HttpNotFound();
            }
            return View(jPHire);
        }

        // POST: JPHires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            JPHire jPHire = db.JPHires.Find(id);
            db.JPHires.Remove(jPHire);
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

        public ActionResult Networking()
        {
            return View();
        }
    }
}
