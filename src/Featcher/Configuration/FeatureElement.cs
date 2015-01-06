using System.Configuration;

namespace Featcher.Configuration {
    public class FeatureElement : ConfigurationElement {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("enabled", IsRequired = true)]
        public bool Enabled {
            get { return ((bool)this["enabled"]); }
            set { this["enabled"] = value; }
        }
    }
}