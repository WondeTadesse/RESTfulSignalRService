//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTfulSignalRService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "RESTful SignalR service";

            return View();
        }
    }
}
