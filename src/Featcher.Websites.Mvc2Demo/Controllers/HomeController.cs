using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Featcher.Websites.Mvc2Demo.Controllers {
    [HandleError]
    public class HomeController : Controller {

        private readonly FeatureSwitcher featureSwitcher = new FeatureSwitcher();

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Toggle(string feature, bool? enable) {
            if (enable.HasValue) featureSwitcher.Toggle(feature, enable.Value);
            return (RedirectToAction("Index"));

        }
    }
}
