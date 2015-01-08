using System;

namespace Featcher.Websites.Mvc2Demo.Models {
    public class IndexViewData {
        public string Message { get; set; }
        public Func<Feature, bool> IsEnabled { get; set; }
        public bool ShowTwitterFeed { get; set; }
        public bool ShowNewsTicker { get; set; }
        public bool ShowWysiwygEditor { get; set; }
    }
}