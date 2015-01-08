namespace Featcher.Interfaces {
    public interface IFeatureSwitcher {
        void Toggle(Feature feature, bool enabled);
        void Toggle(string featureName, bool enabled);
        void Enable(Feature feature);
        void Disable(Feature feature);
        bool IsEnabled(Feature feature);
        bool IsEnabled(string feature);
    }
}