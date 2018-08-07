namespace JPDash.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JPDash.Models.JPDashTableContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JPDash.Models.JPDashTableContext context)
        {

            var students = new List<JPStudent>
            {
                /*
                new JPStudent {JPStudentId=1,JPName="Sally Reynolds",JPEmail="sr@gmail.com",JPStudentLocation=JPStudentLocation.Local,JPStartDate=DateTime.Now},
                new JPStudent {JPStudentId=2,JPName="John Jones",JPEmail="jj@gmail.com",JPStudentLocation=JPStudentLocation.Local,JPStartDate=DateTime.Now},
                new JPStudent{JPStudentId=3,JPName="Rachel Anderson",JPEmail="ra@gmail.com",JPStudentLocation=JPStudentLocation.Remote,JPStartDate=DateTime.Now},
                new JPStudent{JPStudentId=4,JPName="Richard Smith",JPEmail="rs@gmail.com",JPStudentLocation=JPStudentLocation.Local,JPStartDate=DateTime.Now},
                new JPStudent{JPStudentId=5,JPName="Marcos Rivera",JPEmail="mr@gmail.com",JPStudentLocation=JPStudentLocation.Local,JPStartDate=DateTime.Now}
                */
                new JPStudent {JPStudentId=1,JPName="Sally Reynolds",JPEmail="sr@gmail.com",JPStudentLocation=JPStudentLocation.PortlandLocal,JPStartDate=DateTime.Now,JPLinkedIn="www.linkedin.com/in/jane-doe-21a90741",JPPortfolio="beesbeesbees.com",JPContact=true,JPGraduated = true,JPHired=false},
                new JPStudent {JPStudentId=2,JPName="John Jones",JPEmail="jj@gmail.com",JPStudentLocation=JPStudentLocation.PortlandLocal,JPStartDate=DateTime.Now,JPLinkedIn="www.linkedin.com/in/john-doe-729864110",JPPortfolio="www.buzzfeed.com/chelseamarshall/best-kitten-pictures",JPContact=false,JPGraduated = false,JPHired=false},
                new JPStudent{JPStudentId=3,JPName="Rachel Anderson",JPEmail="ra@gmail.com",JPStudentLocation=JPStudentLocation.Remote,JPStartDate=DateTime.Now,JPLinkedIn="www.linkedin.com/in/jane-doe-b2101039",JPPortfolio="www.nooooooooooooooo.com",JPContact=false,JPGraduated = false,JPHired=false},
                new JPStudent{JPStudentId=4,JPName="Richard Smith",JPEmail="rs@gmail.com",JPStudentLocation=JPStudentLocation.PortlandLocal,JPStartDate=DateTime.Now,JPLinkedIn="www.linkedin.com/in/john-doe-a47232123",JPPortfolio="tinytuba.com",JPContact=false,JPGraduated = false,JPHired=false},
                new JPStudent{JPStudentId=5,JPName="Marcos Rivera",JPEmail="mr@gmail.com",JPStudentLocation=JPStudentLocation.PortlandLocal,JPStartDate=DateTime.Now,JPLinkedIn="www.linkedin.com/in/john-doe-b8a765157",JPPortfolio="corndog.io",JPContact=true,JPGraduated = true,JPHired=false}


            };

            students.ForEach(s => context.JPStudents.AddOrUpdate(p => p.JPStudentId, s));
            context.SaveChanges();

            var applications = new List<JPApplication>
           {
               new JPApplication {JPAppId=Guid.NewGuid(),JPStudentId=1,JPCompanyName="Nike",JPJobTitle="Software Engineer",JPJobCategory=JPJobCategory.Development,JPCompanyCity="Portland",JPCompanyState=JPCompanyState.Oregon,JPApplicationDate = Convert.ToDateTime("2018-01-02")},
               new JPApplication {JPAppId=Guid.NewGuid(),JPStudentId=2,JPCompanyName="New Relic",JPJobTitle="Technical Support Engineer",JPJobCategory=JPJobCategory.Tech_Support,JPCompanyCity="Portland",JPCompanyState=JPCompanyState.Oregon,JPApplicationDate = Convert.ToDateTime("2018-02-02")},
               new JPApplication {JPAppId=Guid.NewGuid(),JPStudentId=3,JPCompanyName="Google",JPJobTitle="Jr. Database Admin",JPJobCategory=JPJobCategory.Database_Administration,JPCompanyCity="Redmond",JPCompanyState=JPCompanyState.Washington,JPApplicationDate = Convert.ToDateTime("2018-06-12")},
               new JPApplication {JPAppId=Guid.NewGuid(),JPStudentId=3,JPCompanyName="Walmart",JPJobTitle="QA Engineer",JPJobCategory=JPJobCategory.QA,JPCompanyCity="Vancouver",JPCompanyState=JPCompanyState.Washington,JPApplicationDate = Convert.ToDateTime("2018-01-15")},
               new JPApplication {JPAppId=Guid.NewGuid(),JPStudentId=4,JPCompanyName="Walmart",JPJobTitle="QA Engineer",JPJobCategory=JPJobCategory.QA,JPCompanyCity="Vancouver",JPCompanyState=JPCompanyState.Washington,JPApplicationDate = Convert.ToDateTime("2018-01-20")},
               new JPApplication {JPAppId=Guid.NewGuid(),JPStudentId=5,JPCompanyName="Plaid Electric",JPJobTitle="Software Developer",JPJobCategory=JPJobCategory.Development,JPCompanyCity="Beaverton",JPCompanyState=JPCompanyState.Oregon,JPApplicationDate = Convert.ToDateTime("2018-04-02")}
           };

            applications.ForEach(s => context.JPApplications.AddOrUpdate(p => p.JPAppId, s));
            context.SaveChanges();


            var hires = new List<JPHire>

            {
            new JPHire { JPHireId=Guid.NewGuid(),JPStudentId=1, JPCompanyName="Nike",JPJobTitle="Software Engineer",JPJobCategory=JPJobCategory.Development,JPSalary=120000,JPCompanyCity="Portland",JPCompanyState=JPCompanyState.Oregon,JPCareersPage="jobs.nike.com",JPHireDate = Convert.ToDateTime("2018-05-11") },
            new JPHire { JPHireId=Guid.NewGuid(),JPStudentId=2, JPCompanyName="New Relic",JPJobTitle="Technical Support Engineer",JPJobCategory=JPJobCategory.Dev_Ops,JPSalary=65000,JPCompanyCity="Portland",JPCompanyState=JPCompanyState.Oregon,JPCareersPage="www.newrelic.com/about/careers", JPHireDate = Convert.ToDateTime("2018-04-11")},
            new JPHire { JPHireId=Guid.NewGuid(),JPStudentId=3, JPCompanyName="Google",JPJobTitle="Jr. Database Admin",JPJobCategory=JPJobCategory.Development,JPSalary=75000,JPCompanyCity="Redmond",JPCompanyState=JPCompanyState.Washington,JPCareersPage="careers.google.com", JPHireDate = Convert.ToDateTime("2018-07-15") },
            new JPHire { JPHireId=Guid.NewGuid(),JPStudentId=4, JPCompanyName="Walmart",JPJobTitle="QA Engineer",JPJobCategory=JPJobCategory.Development,JPSalary=80000,JPCompanyCity="Vancouver",JPCompanyState=JPCompanyState.Washington,JPCareersPage="careers.walmart.com", JPHireDate = Convert.ToDateTime("2018-07-16") },
            new JPHire { JPHireId=Guid.NewGuid(),JPStudentId=5, JPCompanyName="Plaid Electric",JPJobTitle="Software Developer",JPJobCategory=JPJobCategory.Development,JPSalary=90000,JPCompanyCity="Beaverton",JPCompanyState=JPCompanyState.Oregon,JPCareersPage="www.amazon.jobs", JPHireDate = Convert.ToDateTime("2018-07-17") }
            };

            hires.ForEach(s => context.JPHires.AddOrUpdate(p => p.JPHireId, s));
            context.SaveChanges();


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
