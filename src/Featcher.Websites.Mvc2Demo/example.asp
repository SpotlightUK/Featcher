<%@  language="JScript" %>
<%

    function isFeatureEnabled(featureName) {
        try {
            // Note the use of the XPath 1.0 translate hack to make our feature name case-insensitive.
            var xpath = "/configuration/featcher/feature[translate(@name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='" + featureName.toLowerCase() + "']";
            var config = Server.CreateObject("msxml2.DOMDocument.6.0");
            config.load(Server.MapPath("/web.config"));
            var node = config.selectSingleNode(xpath);
            var enabledAttribute = (node ? node.getAttribute("enabled") : "");
            if (enabledAttribute.toLowerCase() === "true") return (true);
        } catch (ex) {
        }
        var cookie = Request.Cookies("featcher");
        return (cookie && cookie(featureName) && cookie(featureName).toLowerCase && cookie(featureName).toLowerCase() === "true");
    }
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <h3>Classic ASP Example</h3>
    <p>News Ticker:
        <%=isFeatureEnabled("NewsTicker") ? "Enabled" : "Disabled" %></p>
    <p>Twitter Feed:
        <%=isFeatureEnabled("TwitterFeed") ? "Enabled" : "Disabled" %></p>
    <p>WYSIWYG Editor:
        <%=isFeatureEnabled("WysiwygEditor") ? "Enabled" : "Disabled" %></p>
    <p>WYSIWYG Editor:
        <%=isFeatureEnabled("WYSIWYGEDITOR") ? "Enabled" : "Disabled" %></p>
    <p>WYSIWYG Editor:
        <%=isFeatureEnabled("wysiwygeditor") ? "Enabled" : "Disabled" %></p>
</body>
</html>
