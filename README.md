Featcher
========

**Feat**ure + swit**cher** == **Featcher**


Lightweight feature switching for .NET

* Uses session cookies to switch features on temporarily for testing, demo and review
* Uses custom config sections to turn things on for real when they're properly released.

```
if(FeatureSwitcher.IsEnabled(Features.NewsTicker)) {
  // show the news ticker!
}
```


