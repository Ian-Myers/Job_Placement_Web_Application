using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JPDash.Models
{
    public class JPHire
    {
        [DisplayName("Hired ID")]
        public Guid JPHireId { get; set; }
        [DisplayName("Student ID")]
        public int JPStudentId { get; set; }
        [DisplayName("Company Name")]
        public string JPCompanyName { get; set; }
        [DisplayName("Job Title")]
        public string JPJobTitle { get; set; }
        [DisplayName("Job Category")]
        public JPJobCategory JPJobCategory { get; set; }
        [DisplayName(" Salary")]
        public decimal JPSalary { get; set; }
        [DisplayName("Work Location City")]
        public string JPCompanyCity { get; set; }
        [DisplayName("Work Location State")]
        public JPCompanyState JPCompanyState { get; set; }
        [DisplayName("Second Job")]
        public bool JPSecondJob { get; set; }
        [DisplayName("Company Careers Page")]
        public string JPCareersPage { get; set; }
        [DisplayName("Hire Date")]
        public DateTime? JPHireDate { get; set; }
        public virtual JPStudent JPStudent { get; set; }


        [NotMapped]
        public bool? IsHiredWithinOneWeekOfCurrentDate
        {
            get
            {
                return DateCalculateHelper.CalculateIsObjectCreatedWithinOneWeekOfCurrentDate(this.JPHireDate);
            }
        }

        public string JPHireDateNoTimeStamp
        {
            get
            {
                string noStamp = Convert.ToDateTime(JPHireDate).ToShortDateString();
                return (noStamp);
            }
        }

    }
}