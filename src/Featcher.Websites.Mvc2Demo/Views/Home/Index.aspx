<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Featcher" %>
<%@ Import Namespace="Featcher.Websites.Mvc2Demo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Featcher: Feature Switching for .NET</h1>
    <% if (FeatureSwitcher.IsEnabled(Features.NewsTicker)) { %>
    <div style="border: 1px solid #f00; color: #f00; padding: 8px; margin: 8px;">THIS IS THE NEWS TICKER!</div>
    <% } %>
    <% if (FeatureSwitcher.IsEnabled(Features.TwitterFeed)) { %>
    <div style="background-color: #55acee; color: #fff; padding: 8px; margin: 8px;">This is a Twitter feed. Check it out.</div>
    <% } %>
    <% if (FeatureSwitcher.IsEnabled(Features.WysiwygEditor)) { %>
    <div style="border: 1px solid #0c0; padding: 8px; margin: 8px;">This is the WYSIWIG editor. In this example it's enabled via web.config so you can't disable it.</div>
    <% } %>
    <% foreach (var feature in Features.AllFeatures) { %>
    <p>
        <%= feature.Name %>: <strong>
            <%=FeatureSwitcher.IsEnabled(feature) ? "Enabled" : "Hidden" %></strong>
        <% if (FeatureSwitcher.IsEnabled(feature)) { %>
        <%=Html.ActionLink("disable", "Toggle", new { feature = feature.Name, enable = false}) %>
        <% } else { %>
        <%=Html.ActionLink("enable", "Toggle", new { feature = feature.Name, enable = true }) %>
        <% } %>
    </p>
    <% } %>
    <h2>Classic ASP example (IFRAME):</h2>
    <iframe src="example.asp" style="border: 1px solid #000; width: 100%; height: 300px;">
    </iframe>
</asp:Content>
