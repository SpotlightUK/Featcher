namespace Featcher.Interfaces {
    /// <summary>Defines behaviour for enabling and disabling features, and for determining whether a feature is enabled or disabled.</summary>
    public interface IFeatureSwitcher {
        /// <summary>Enable or disable the specified feature for the current user. This has no effect on features that are enabled via web.config.</summary>
        /// <param name="feature">An instance of <see cref="Feature"/> identifying the feature to be enabled or disabled.</param>
        /// <param name="enabled">True if the feature is to be enabled for this user; otherwise false.</param>
        void Toggle(Feature feature, bool enabled);

        /// <summary>Enable or disable the specified feature for the current user. This has no effect on features that are enabled via web.config.</summary>
        /// <param name="featureName">The unique name identifying the feature to be enabled or disabled.</param>
        /// <param name="enabled">True if the feature is to be enabled for this user; otherwise false.</param>        
        void Toggle(string featureName, bool enabled);

        /// <summary>Enable the specified feature for the current user. This has no effect on features that are enabled via web.config.</summary>
        /// <param name="feature">An instance of <see cref="Feature"/> identifying the feature to be enabled.</param>
        void Enable(Feature feature);

        /// <summary>Disable the specified feature for the current user. This has no effect on features that are enabled via web.config.</summary>
        /// <param name="feature">An instance of <see cref="Feature"/> identifying the feature to be disabled.</param>
        void Disable(Feature feature);

        /// <summary>Returns true if the specified feature is enabled; otherwise false.</summary>
        /// <param name="feature">An instance of <see cref="Feature"/> identifying the feature to be enabled.</param>
        /// <returns>True if the feature is enabled (either via web.config or via a session cookie); otherwise false.</returns>
        bool IsEnabled(Feature feature);

        /// <summary>Returns true if the specified feature is enabled; otherwise false.</summary>
        /// <param name="featureName">The unique name identifying the feature.</param>
        /// <returns>True if the feature is enabled (either via web.config or via a session cookie); otherwise false.</returns>
        bool IsEnabled(string featureName);

        /// <summary>Returns true if the specified feature is enabled via a session cookie; otherwise false.</summary>
        /// <param name="featureName">The unique name identifying the feature.</param>
        /// <returns>True if the feature is enabled via a session cookie; otherwise false. Returns false for features enabled via web.config</returns>
        bool IsEnabledViaCookie(string featureName);

        /// <summary>Returns true if the specified feature is enabled via the application config file; otherwise false.</summary>
        /// <param name="featureName">The unique name identifying the feature.</param>
        /// <returns>True if the feature is enabled via the config file; otherwise false. Returns false for features enabled via a session cookie.</returns>
        bool IsEnabledViaConfig(string featureName);

        /// <summary>Returns true if the specified feature is enabled via a session cookie; otherwise false.</summary>
        /// <param name="feature">An instance of <see cref="Feature"/> identifying the feature in question.</param>
        /// <returns>True if the feature is enabled via a session cookie; otherwise false. Returns false for features enabled via web.config</returns>        
        bool IsEnabledViaCookie(Feature feature);

        /// <summary>Returns true if the specified feature is enabled via the application config file; otherwise false.</summary>
        /// <param name="feature">An instance of <see cref="Feature"/> identifying the feature in question.</param>
        /// <returns>True if the feature is enabled via the config file; otherwise false. Returns false for features enabled via a session cookie.</returns>
        bool IsEnabledViaConfig(Feature feature);
    }
}