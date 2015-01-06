using System;
using System.Configuration;
using System.Web;
using Featcher.Configuration;

namespace Featcher {
    public static class FeatureSwitcher {
        public const string COOKIE_NAME = "featcher";
        public static void Toggle(Feature feature, bool enabled) {
            var context = HttpContext.Current;
            if (context == null) return;
            var cookies = context.Request.Cookies;
            var cookie = cookies[COOKIE_NAME] ?? new HttpCookie(COOKIE_NAME);
            cookie.Values[feature.ToString()] = enabled.ToString();
            context.Response.Cookies.Add(cookie);
        }

        public static void Enable(Feature feature) {
            Toggle(feature, true);
        }

        public static void Disable(Feature feature) {
            Toggle(feature, false);
        }

        public static bool IsEnabled(Feature feature) {
            var section = ConfigurationManager.GetSection("featcher");
            var featureSection = section as FeatureSection;
            if (featureSection != null) {
                var configFeature = featureSection.Feature[feature.ToString()];
                if (configFeature.Enabled) return (true);
            }
            if (HttpContext.Current == null) return (false);
            var cookies = HttpContext.Current.Request.Cookies;
            var cookie = cookies["featcher"];
            if (cookie == null || !cookie.HasKeys) return (false);
            if (cookie.Values[feature.ToString()] == null) return (false);
            bool enabled;
            return Boolean.TryParse(cookie.Values[feature.ToString()], out enabled) && enabled;
        }
    }
}