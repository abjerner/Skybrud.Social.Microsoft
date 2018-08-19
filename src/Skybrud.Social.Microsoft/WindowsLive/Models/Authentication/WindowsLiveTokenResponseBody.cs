using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Social.Microsoft.WindowsLive.Models;
using Skybrud.Social.Microsoft.WindowsLive.Scopes;

namespace Skybrud.Social.Microsoft.WindowsLive.Models.Authentication {
    
    /// <summary>
    /// Class representing the response body of a call to get a refresh token or an access token.
    /// </summary>
    public class WindowsLiveTokenResponseBody : WindowsLiveObject {

        #region Properties

        /// <summary>
        /// Gets the type of the topen.
        /// </summary>
        public string TokenType { get; }

        /// <summary>
        /// Gets an instance of <see cref="TimeSpan"/> for when the access token will expire.
        /// </summary>
        public TimeSpan ExpiresIn { get; }

        /// <summary>
        /// Gets a collection of the scopes the user has granted.
        /// </summary>
        public WindowsLiveScopeCollection Scope { get; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        public string AuthenticationToken { get; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        public string RefreshToken { get; }

        /// <summary>
        /// Gets whether a authentication token was specified in the response.
        /// </summary>
        public bool HasAuthenticationToken => !String.IsNullOrWhiteSpace(AuthenticationToken);

        /// <summary>
        /// Gets whether a refresh token was specified in the response.
        /// </summary>
        public bool HasRefreshToken => !String.IsNullOrWhiteSpace(RefreshToken);

        #endregion

        #region Constructors

        private WindowsLiveTokenResponseBody(JObject obj) : base(obj) {

            // Convert the "scope" string to a collection of scopes
            Scope = new WindowsLiveScopeCollection();
            foreach (string name in obj.GetString("scope").Split(' ')) {
                WindowsLiveScope scope = WindowsLiveScope.GetScope(name) ?? WindowsLiveScope.RegisterScope(name);
                Scope.Add(scope);
            }

            // Parse the rest of the response body
            TokenType = obj.GetString("token_type");
            ExpiresIn = obj.GetDouble("expires_in", TimeSpan.FromSeconds);
            AccessToken = obj.GetString("access_token");
            AuthenticationToken = obj.GetString("authentication_token");
            RefreshToken = obj.GetString("refresh_token");

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="WindowsLiveTokenResponseBody"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="WindowsLiveTokenResponseBody"/>.</returns>
        public static WindowsLiveTokenResponseBody Parse(JObject obj) {
            return obj == null ? null : new WindowsLiveTokenResponseBody(obj);
        }

        #endregion

    }

}