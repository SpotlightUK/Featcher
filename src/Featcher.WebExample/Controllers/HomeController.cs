using System.Linq;
using System.Web.Mvc;

namespace Featcher.WebExample.Controllers {
    public class HomeController : Controller {
        public ActionResult Index(string name, bool? enable) {
            var feature = Features.Find(name);
            if (feature != null && enable.HasValue) {
                FeatureSwitcher.Toggle(feature, enable.Value);
            }
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public static class Features {
        public static Feature BigRedBox = new Feature("BigRedBox", "Shows a big red box on the landing page");
        public static Feature BigGreenBox = new Feature("BigGreenBox", "Shows a big green box on the landing page");
        public static Feature NuclearLaunch = new Feature("NuclearLaunch", "Allow users to launch nuclear missiles");

        public static Feature[] AllFeatures = {
            BigRedBox,
            BigGreenBox,
            NuclearLaunch
        };

        public static Feature Find(string name) {
            return (AllFeatures.FirstOrDefault(feature => feature.Name == name));
        }
    }
}
