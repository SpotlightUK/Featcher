using System;
using System.Configuration;
using System.Web;
using Featcher.Configuration;
using Featcher.Interfaces;

namespace Featcher {
    public static class FeatureSwitcher {
        public const string COOKIE_NAME = "featcher";

        public static void Toggle(IFeature feature, bool enabled) {
            Toggle(feature.Name, enabled);

        }

        public static void Toggle(string featureName, bool enabled) {
            if (String.IsNullOrEmpty(featureName)) return;
            var context = HttpContext.Current;
            if (context == null) return;
            var cookies = context.Request.Cookies;
            var cookie = cookies[COOKIE_NAME] ?? new HttpCookie(COOKIE_NAME);
            cookie.Values[featureName] = enabled.ToString();
            context.Response.AppendCookie(cookie);
        }

        public static void Enable(IFeature feature) {
            Toggle(feature, true);
        }

        public static void Disable(IFeature feature) {
            Toggle(feature, false);
        }

        public static bool IsEnabled(IFeature feature) {
            return (IsEnabled(feature.Name));
        }

        public static bool IsEnabled(string feature) {
            var section = ConfigurationManager.GetSection("featcher");
            var featureSection = section as FeatureSection;
            if (featureSection != null) {
                var configFeature = featureSection.Feature[feature];
                if (configFeature.Enabled) return (true);
            }
            if (HttpContext.Current == null) return (false);
            var cookies = HttpContext.Current.Request.Cookies;
            var cookie = cookies["featcher"];
            if (cookie == null || !cookie.HasKeys) return (false);
            if (cookie.Values[feature] == null) return (false);
            bool enabled;
            return Boolean.TryParse(cookie.Values[feature], out enabled) && enabled;
        }
        
    }
}