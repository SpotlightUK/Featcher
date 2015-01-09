using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Configuration;
using System.Web;
using Featcher.Configuration;
using Featcher.Interfaces;

namespace Featcher {
    public class FeatureSwitcher : IFeatureSwitcher {
        public const string COOKIE_NAME = "featcher";

        public void Toggle(Feature feature, bool enabled) {
            Toggle(feature.Name, enabled);
        }

        public void Toggle(string featureName, bool enabled) {
            if (String.IsNullOrEmpty(featureName)) {
                return;
            }
            var context = HttpContext.Current;
            if (context == null) {
                return;
            }
            var cookies = context.Request.Cookies;
            var cookie = cookies[COOKIE_NAME] ?? new HttpCookie(COOKIE_NAME);
            cookie.Values[featureName] = enabled.ToString();
            context.Response.AppendCookie(cookie);
        }

        public void Enable(Feature feature) {
            Toggle(feature, true);
        }

        public void Disable(Feature feature) {
            Toggle(feature, false);
        }

        public bool IsEnabled(Feature feature) {
            return (IsEnabled(feature.Name));
        }

        public bool IsEnabledViaConfig(string feature) {
            var section = ConfigurationManager.GetSection("featcher");
            var featureSection = section as FeatureSection;
            if (featureSection != null) {
                var configFeature = featureSection.Feature[feature];
                if (configFeature.Enabled) return (true);
            }
            return (false);
        }

        public bool IsEnabledViaCookie(Feature feature) {
            return (IsEnabledViaCookie(feature.Name));
        }

        public bool IsEnabledViaConfig(Feature feature) {
            return (IsEnabledViaConfig(feature.Name));
        }

        public bool IsEnabledViaCookie(string feature) {
            if (HttpContext.Current == null) return (false);
            var cookies = HttpContext.Current.Request.Cookies;
            var cookie = cookies["featcher"];
            if (cookie == null || !cookie.HasKeys) return (false);
            if (cookie.Values[feature] == null) return (false);
            bool enabled;
            return Boolean.TryParse(cookie.Values[feature], out enabled) && enabled;
        }

        public bool IsEnabled(string feature) {
            return (IsEnabledViaConfig(feature) || IsEnabledViaCookie(feature));
        }
    }
}