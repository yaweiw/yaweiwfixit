using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETMVC.Models;

namespace ASPNETMVC.Controllers
{
    public class MyFirstMVCController : Controller
    {
        // GET: HelloView.cshtml
        public ActionResult SayHello()
        {
            ViewData["currenttime"] = DateTime.Now.ToString();
            return View("HelloView");
        }

        //Get: DisplayCustomer.cshtml
        public ActionResult DisplayCustomer()
        {
            Customer objCustomer = new Customer();
            objCustomer.Id = 12;
            objCustomer.CustomerCode = "1001";
            objCustomer.Amount = 90.34;

            return View("DisplayCustomer", objCustomer);
        }

        public ActionResult Justatest()
        {
            return View("JTTView");
        }
    }
}