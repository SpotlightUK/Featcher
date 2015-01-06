using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Featcher {
    public class Feature {
        public string Name { get; set; }
        public string Description { get; set; }

        public Feature(string name, string description) {
            Name = name;
            Description = description;
        }

        public override string ToString() {
            return (Name);
        }
    }
}
