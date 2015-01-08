using System;
using System.Configuration;

namespace Featcher.Configuration {
    public class FeatureCollection : ConfigurationElementCollection {
        public FeatureCollection() {
            var details = (FeatureElement)CreateNewElement();
            if (details.Name != "") Add(details);
        }

        public override ConfigurationElementCollectionType CollectionType {
            get {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new FeatureElement();
        }

        protected override Object GetElementKey(ConfigurationElement element) {
            return ((FeatureElement)element).Name.ToLowerInvariant();
        }

        public FeatureElement this[int index] {
            get {
                return (FeatureElement)BaseGet(index);
            }
            set {
                if (BaseGet(index) != null) {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public FeatureElement this[string name] {
            get {
                return (FeatureElement)BaseGet(name.ToLowerInvariant()) ?? new FeatureElement { Name = name, Enabled = false };
            }
        }

        public int IndexOf(FeatureElement details) {
            return BaseIndexOf(details);
        }

        public void Add(FeatureElement details) {
            details.Name = details.Name.ToLowerInvariant();
            BaseAdd(details);
        }
        protected override void BaseAdd(ConfigurationElement element) {
            BaseAdd(element, false);
        }

        public void Remove(FeatureElement details) {
            if (BaseIndexOf(details) >= 0) BaseRemove(details.Name.ToLowerInvariant());
        }

        public void RemoveAt(int index) {
            BaseRemoveAt(index);
        }

        public void Remove(string name) {
            BaseRemove(name.ToLowerInvariant());
        }

        public void Clear() {
            BaseClear();
        }

        protected override string ElementName {
            get { return "feature"; }
        }
    }
}