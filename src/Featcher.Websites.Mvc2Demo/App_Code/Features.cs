using System.Collections;

namespace Featcher.Websites.Mvc2Demo {
    public class Features {
        public static Feature NewsTicker = new Feature("NewsTicker", "Show the news ticker on the homepage");
        public static Feature TwitterFeed = new Feature("TwitterFeed", "Show the Twitter feed on the homepage");
        public static Feature WysiwygEditor = new Feature("WysiwygEditor", "The new WYSIWYG editor for posting job information");

        public static Feature[] AllFeatures = {
            NewsTicker,
            TwitterFeed,
            WysiwygEditor
        };
    }
}