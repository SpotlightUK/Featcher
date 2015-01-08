namespace Featcher.Interfaces {
    public interface IFeatureSwitcher {
        void Toggle(IFeature feature, bool enabled)
        void Toggle(string featureName, bool enabled);
        
        void Enable(IFeature feature);
        void Disable(IFeature feature);

        bool IsEnabled(IFeature feature);
        bool IsEnabled(string feature);

    }
}
