using JPDash.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JPDash.ViewModels
{
    public class JPStudentRundown
    {
        public JPStudent jpstudent { get; set; }
        public int totalApplications { get; set; }
        public int totalApplicationsThisWeek { get; set; }
        [DisplayName("Location")]
        public JPStudentLocation JPStudentLocation { get; set; }

        public JPStudentRundown(JPStudent jpstudent, int totalApplications, int totalApplicationsThisWeek)
        {
            this.jpstudent = jpstudent;
            this.totalApplications = totalApplications;
            this.totalApplicationsThisWeek = totalApplicationsThisWeek;
        }
    }
}