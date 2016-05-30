using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SangueHeroiWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["ID_HEMOCENTRO"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }            
        }

        public ActionResult Ajax()
        {
            return Json(CriaJson(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Company> CriaJson()
        {
            List<Company> companies = new List<Company>();

            Company company = new Company() { Expense = 200, Salary = 1000, Year = new DateTime(2012, 1, 1).ToString("yyyy/MM") };
            Company company1 = new Company() { Expense = 300, Salary = 1200, Year = new DateTime(2012, 2, 1).ToString("yyyy/MM") };
            Company company2 = new Company() { Expense = 240, Salary = 1400, Year = new DateTime(2012, 3, 1).ToString("yyyy/MM") };
            Company company3 = new Company() { Expense = 100, Salary = 1100, Year = new DateTime(2012, 4, 1).ToString("yyyy/MM") };
            Company company4 = new Company() { Expense = 40, Salary = 1200, Year = new DateTime(2012, 5, 1).ToString("yyyy/MM") };
            Company company5 = new Company() { Expense = 343, Salary = 1300, Year = new DateTime(2012, 6, 1).ToString("yyyy/MM") };
            Company company6 = new Company() { Expense = 401, Salary = 1700, Year = new DateTime(2012, 7, 1).ToString("yyyy/MM") };

            companies.Add(company);
            companies.Add(company1);
            companies.Add(company2);
            companies.Add(company3);
            companies.Add(company4);
            companies.Add(company5);
            companies.Add(company6);

            return companies;
        }
    }
}