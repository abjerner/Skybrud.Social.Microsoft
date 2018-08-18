﻿using System;
using Skybrud.Essentials.Common;
using Skybrud.Social.Http;
using Skybrud.Social.Interfaces.Http;
using Skybrud.Social.Microsoft.Responses.Authentication;
using Skybrud.Social.Microsoft.Scopes;
using Skybrud.Social.Microsoft.WindowsLive.Endpoints.Raw;

namespace Skybrud.Social.Microsoft.OAuth {
    
    /// <summary>
    /// 
    /// </summary>
    /// <see>
    ///     <cref>https://msdn.microsoft.com/en-us/library/hh243647.aspx</cref>
    /// </see>
    public class MicrosoftOAuthClient : SocialHttpClient {

        #region Properties

        #region OAuth

        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the redirect URI.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; }

        #endregion

        #region Endpoints

        /// <summary>
        /// Gets a reference to the raw Windows Live endpoint.
        /// </summary>
        public WindowsLiveRawEndpoint WindowsLive { get; private set; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an OAuth client with empty information.
        /// </summary>
        public MicrosoftOAuthClient() {
            WindowsLive = new WindowsLiveRawEndpoint(this);
        }

        /// <summary>
        /// Initializes an OAuth client with the specified <paramref name="accessToken"/>.
        /// </summary>
        /// <param name="accessToken">A valid access token.</param>
        public MicrosoftOAuthClient(string accessToken) : this() {
            if (String.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException("accessToken");
            AccessToken = accessToken;
        }

        /// <summary>
        /// Initializes an OAuth client with the specified <paramref name="clientId"/> and
        /// <paramref name="clientSecret"/>.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="clientSecret">The secret of the client.</param>
        public MicrosoftOAuthClient(string clientId, string clientSecret) : this() {
            if (String.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException("clientId");
            if (String.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException("clientSecret");
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        /// Initializes an OAuth client with the specified <paramref name="clientId"/>, <paramref name="clientSecret"/>
        /// and <paramref name="redirectUri"/>.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="clientSecret">The secret of the client.</param>
        /// <param name="redirectUri">The redirect URI of the client.</param>
        public MicrosoftOAuthClient(string clientId, string clientSecret, string redirectUri) : this() {
            if (String.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException("clientId");
            if (String.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException("clientSecret");
            if (String.IsNullOrWhiteSpace(redirectUri)) throw new ArgumentNullException("redirectUri");
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the authorization URL based on the specified <paramref name="state"/> and
        /// <paramref name="scope"/>.
        /// </summary>
        /// <param name="state">The state to send to the Microsoft OAuth login page.</param>
        /// <param name="scope">The scope of the application.</param>
        /// <returns>An authorization URL based on <paramref name="state"/> and <paramref name="scope"/>.</returns>
        public string GetAuthorizationUrl(string state, MicrosoftScopeCollection scope) {
            return GetAuthorizationUrl(state, scope.ToString());
        }

        /// <summary>
        /// Generates the authorization URL based on the specified <paramref name="state"/> and
        /// <paramref name="scope"/>.
        /// </summary>
        /// <param name="state">The state to send to the Microsoft OAuth login page.</param>
        /// <param name="scope">The scope of the application.</param>
        /// <returns>An authorization URL based on <paramref name="state"/> and <paramref name="scope"/>.</returns>
        public string GetAuthorizationUrl(string state, params string[] scope) {

            // Some validation
            if (String.IsNullOrWhiteSpace(ClientId)) throw new PropertyNotSetException("ClientId");
            if (String.IsNullOrWhiteSpace(RedirectUri)) throw new PropertyNotSetException("RedirectUri");

            // Do we have a valid "state" ?
            if (String.IsNullOrWhiteSpace(state)) {
                throw new ArgumentNullException("state", "A valid state should be specified as it is part of the security of OAuth 2.0.");
            }

            // Construct the query string
            IHttpQueryString query = new SocialHttpQueryString {
                {"client_id", ClientId},
                {"redirect_uri", RedirectUri},
                {"response_type", "code"},
                {"state", state}
            };

            // Append the scope (if specified)
            if (scope != null && scope.Length > 0) {
                query.Add("scope", String.Join(" ", scope));
            }

            // Construct thr authorization URL
            return "https://login.live.com/oauth20_authorize.srf?" + query;

        }

        /// <summary>
        /// Exchanges the specified <paramref name="authorizationCode"/> for a refresh token and an access token.
        /// </summary>
        /// <param name="authorizationCode">The authorization code received from the Microsoft OAuth dialog.</param>
        /// <returns>An instance of <see cref="MicrosoftTokenResponse"/> representing the response.</returns>
        public MicrosoftTokenResponse GetAccessTokenFromAuthCode(string authorizationCode) {

            // Some validation
            if (String.IsNullOrWhiteSpace(ClientId)) throw new PropertyNotSetException("ClientId");
            if (String.IsNullOrWhiteSpace(ClientSecret)) throw new PropertyNotSetException("ClientSecret");
            if (String.IsNullOrWhiteSpace(RedirectUri)) throw new PropertyNotSetException("RedirectUri");
            if (String.IsNullOrWhiteSpace(authorizationCode)) throw new ArgumentNullException("authorizationCode");

            // Initialize the POST data
            IHttpPostData data = new SocialHttpPostData {
                {"client_id", ClientId},
                {"redirect_uri", RedirectUri},
                {"client_secret", ClientSecret},
                {"code", authorizationCode },
                {"grant_type", "authorization_code"}
            };

            // Make the call to the API
            SocialHttpResponse response = SocialUtils.Http.DoHttpPostRequest("https://login.live.com/oauth20_token.srf", null, data);

            // Parse the response
            return MicrosoftTokenResponse.ParseResponse(response);

        }

        /// <summary>
        /// Gets a new access token from the specified <paramref name="refreshToken"/>.
        /// </summary>
        /// <param name="refreshToken">The refresh token of the user.</param>
        /// <returns>An instance of <see cref="MicrosoftTokenResponse"/> representing the response.</returns>
        public MicrosoftTokenResponse GetAccessTokenFromRefreshToken(string refreshToken) {

            // Some validation
            if (String.IsNullOrWhiteSpace(ClientId)) throw new PropertyNotSetException("ClientId");
            if (String.IsNullOrWhiteSpace(ClientSecret)) throw new PropertyNotSetException("ClientSecret");
            if (String.IsNullOrWhiteSpace(RedirectUri)) throw new PropertyNotSetException("RedirectUri");
            if (String.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException("refreshToken");

            // Initialize the POST data
            IHttpPostData data = new SocialHttpPostData {
                {"client_id", ClientId},
                {"redirect_uri", RedirectUri},
                {"client_secret", ClientSecret},
                {"refresh_token", refreshToken },
                {"grant_type", "refresh_token"}
            };

            // Make the call to the API
            SocialHttpResponse response = SocialUtils.Http.DoHttpPostRequest("https://login.live.com/oauth20_token.srf", null, data);

            // Parse the response
            return MicrosoftTokenResponse.ParseResponse(response);

        }

        /// <summary>
        /// Virtual method that can be used for configuring a request.
        /// </summary>
        /// <param name="request">The instance of <see cref="SocialHttpRequest"/> representing the request.</param>
        protected override void PrepareHttpRequest(SocialHttpRequest request) {

            // Append the access token to the query string if present in the client and not already
            // specified in the query string
            if (!request.QueryString.ContainsKey("access_token") && !String.IsNullOrWhiteSpace(AccessToken)) {
                request.QueryString.Add("access_token", AccessToken);
            }

        }

        #endregion

    }

}