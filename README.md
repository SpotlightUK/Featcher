Featcher
========

**Feat**ure + swit**cher** == **Featcher**


Lightweight feature switching for .NET

* Uses session cookies to switch features on temporarily for testing, demo and review
* Uses custom config sections to turn things on for real when they're properly released.

```
if(FeatureSwitcher.IsEnabled(Features.NewsTicker)) {
  // show the news ticker!
}
```

How To Define your Features
---------------------------

Create a static class in **your project** that exposes each of your switchable project features as a static property. Featcher doesn't restrict how you do this, but you do need an instance of `Feature` for each feature you want to switch.

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

Temporarily Enable a Feature
----------------------------

You can temporarily enable features by setting cookies - this means developers and testers can view and test code that isn't visible to the rest of the world yet.

    public ActionResult Toggle(string name, bool? enable) {
        var feature = Features.Find(name);
        if (feature != null && enable.HasValue) FeatureSwitcher.Toggle(feature, enable.Value);
		return(View());
    }

This sets a session cookie.

Cookies use subkeys for individual features:

    Request.Cookies["featcher"]["BigRedBox"] = true;

Permanently Enable a Feature
----------------------------

To launch a feature, use a custom web.config setting to switch it on permanently. **Features that have been enabled via web.config cannot be disabled via cookies**

    <configuration>
      <configSections>
        <section name="featcher" type="Featcher.Configuration.FeatureSection, Featcher" />
      </configSections>
      <featcher>
        <feature name="BigRedBox" enabled="true" />
      </featcher>
    </configuration>

Classic ASP support
-------------------

Because some of our code still uses classic ASP, there's a simple function to determine whether a feature is enabled from classic ASP code.

    <%@ language="JScript" %>
    <%
    function isFeatureEnabled(featureName) {
        try {
            // Note the use of the XPath 1.0 translate hack to make our feature name case-insensitive.
            var xpath = "/configuration/featcher/feature[translate(@name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='" + featureName.toLowerCase() + "']";
            var config = Server.CreateObject("msxml2.DOMDocument.6.0");
            config.load(Server.MapPath("/web.config"));
            var node = config.selectSingleNode(xpath);
            var enabledAttribute = (node ? node.getAttribute("enabled") : "");
            if (enabledAttribute.toLowerCase() === "true") return (true);
        } catch (ex) {
        }
        var cookie = Request.Cookies("featcher");
        return (cookie && cookie(featureName) && cookie(featureName).toLowerCase && cookie(featureName).toLowerCase() === "true");
    }
    %>







