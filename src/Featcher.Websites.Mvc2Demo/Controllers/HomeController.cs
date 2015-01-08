using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Featcher.Websites.Mvc2Demo.Controllers {
    [HandleError]
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Toggle(string feature, bool? enable) {
            if (enable.HasValue) FeatureSwitcher.Toggle(feature, enable.Value);
            return (RedirectToAction("Index"));

        }
    }
}
