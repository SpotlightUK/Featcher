using Featcher.Interfaces;

namespace Featcher {
    public class InjectableFeatureSwitcher : IFeatureSwitcher {

        public void Toggle(Feature feature, bool enabled) {
            FeatureSwitcher.Toggle(feature,enabled);
        }

        public void Toggle(string featureName, bool enabled) {
            FeatureSwitcher.Toggle(featureName, enabled);
        }

        public void Enable(Feature feature) {
            FeatureSwitcher.Enable(feature);
        }

        public void Disable(Feature feature) {
            FeatureSwitcher.Disable(feature);
        }

        public bool IsEnabled(Feature feature) {
            return FeatureSwitcher.IsEnabled(feature);
        }

        public bool IsEnabled(string feature) {
            return FeatureSwitcher.IsEnabled(feature);
        }
    }
}