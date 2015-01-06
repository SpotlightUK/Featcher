using System.Configuration;

namespace Featcher.Configuration {
    public class FeatureSection : ConfigurationSection {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public FeatureCollection Feature {
            get {
                var features = (FeatureCollection)base[""];
                return (features);
            }
        }
    }
}