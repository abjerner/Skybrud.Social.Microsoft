using System;
using Skybrud.Essentials.Common;
using Skybrud.Social.Http;
using Skybrud.Social.Interfaces.Http;
using Skybrud.Social.Microsoft.Graph.Endpoints.Raw;
using Skybrud.Social.Microsoft.Graph.Responses.Authentication;
using Skybrud.Social.Microsoft.Graph.Scopes;

namespace Skybrud.Social.Microsoft.Graph.OAuth {

    /// <see>
    ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_register_app_v2</cref>
    ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user</cref>
    /// </see>
    public class MicrosoftGraphOAuthClient : SocialHttpClient {
        
        #region Properties

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

        /// <summary>
        /// Gets a reference to the raw users endpoint.
        /// </summary>
        public MicrosoftGraphUsersRawEndpoint Users { get; }

        #endregion
       
        #region Constructors

        /// <summary>
        /// Initializes an OAuth client with empty information.
        /// </summary>
        public MicrosoftGraphOAuthClient() {
            Users = new MicrosoftGraphUsersRawEndpoint(this);
        }

        /// <summary>
        /// Initializes an OAuth client with the specified <paramref name="accessToken"/>.
        /// </summary>
        /// <param name="accessToken">A valid access token.</param>
        public MicrosoftGraphOAuthClient(string accessToken) : this() {
            if (String.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            AccessToken = accessToken;
        }

        /// <summary>
        /// Initializes an OAuth client with the specified <paramref name="clientId"/> and
        /// <paramref name="clientSecret"/>.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="clientSecret">The secret of the client.</param>
        public MicrosoftGraphOAuthClient(string clientId, string clientSecret) : this() {
            if (String.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (String.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));
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
        public MicrosoftGraphOAuthClient(string clientId, string clientSecret, string redirectUri) : this() {
            if (String.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (String.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));
            if (String.IsNullOrWhiteSpace(redirectUri)) throw new ArgumentNullException(nameof(redirectUri));
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the authorization URL based on the specified <paramref name="state"/>.
        /// </summary>
        /// <param name="state">The state to send to the Microsoft OAuth login page.</param>
        /// <returns>An authorization URL based on <paramref name="state"/>.</returns>
        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user#2-get-authorization</cref>
        /// </see>
        public string GetAuthorizationUrl(string state){
            return GetAuthorizationUrl(state, default(string));
        }

        /// <summary>
        /// Generates the authorization URL based on the specified <paramref name="state"/> and <paramref name="scope"/>.
        /// </summary>
        /// <param name="state">The state to send to the Microsoft OAuth login page.</param>
        /// <param name="scope">The scope of the application.</param>
        /// <returns>An authorization URL based on <paramref name="state"/> and <paramref name="scope"/>.</returns>
        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user#2-get-authorization</cref>
        /// </see>
        public string GetAuthorizationUrl(string state, MicrosoftGraphScopeCollection scope){
            return GetAuthorizationUrl(state, scope?.ToString());
        }

        /// <summary>
        /// Generates the authorization URL based on the specified <paramref name="state"/> and <paramref name="scope"/>.
        /// </summary>
        /// <param name="state">The state to send to the Microsoft OAuth login page.</param>
        /// <param name="scope">The scope of the application.</param>
        /// <returns>An authorization URL based on <paramref name="state"/> and <paramref name="scope"/>.</returns>
        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user#2-get-authorization</cref>
        /// </see>
        public string GetAuthorizationUrl(string state, string scope) {

            // Some validation
            if (String.IsNullOrWhiteSpace(ClientId)) throw new PropertyNotSetException(nameof(ClientId));
            if (String.IsNullOrWhiteSpace(RedirectUri)) throw new PropertyNotSetException(nameof(RedirectUri));

            // Do we have a valid "state" ?
            if (String.IsNullOrWhiteSpace(state)){
                throw new ArgumentNullException(nameof(state), "A valid state should be specified as it is part of the security of OAuth 2.0.");
            }

            // Construct the query string
            IHttpQueryString query = new SocialHttpQueryString {
                {"client_id", ClientId},
                {"redirect_uri", RedirectUri},
                {"response_type", "code"},
                {"state", state}
            };

            // Append the scope (if specified)
            if (!String.IsNullOrWhiteSpace(scope)) {
                query.Add("scope", scope);
            }

            // Construct thr authorization URL
            return "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?" + query;

        }

        /// <summary>
        /// Exchanges the specified <paramref name="authorizationCode"/> for a refresh token and an access token.
        /// </summary>
        /// <param name="authorizationCode">The authorization code received from the Microsoft OAuth dialog.</param>
        /// <param name="scope">The scope.</param>
        /// <returns>An instance of <see cref="MicrosoftGraphTokenResponse"/> representing the response.</returns>
        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user#3-get-a-token</cref>
        /// </see>
        public MicrosoftGraphTokenResponse GetAccessTokenFromAuthCode(string authorizationCode, string scope) {

            // Some validation
            if (String.IsNullOrWhiteSpace(ClientId)) throw new PropertyNotSetException(nameof(ClientId));
            if (String.IsNullOrWhiteSpace(ClientSecret)) throw new PropertyNotSetException(nameof(ClientSecret));
            if (String.IsNullOrWhiteSpace(RedirectUri)) throw new PropertyNotSetException(nameof(RedirectUri));
            if (String.IsNullOrWhiteSpace(authorizationCode)) throw new ArgumentNullException(nameof(authorizationCode));

            // Initialize the POST data
            IHttpPostData data = new SocialHttpPostData {
                {"client_id", ClientId},
                {"redirect_uri", RedirectUri},
                {"client_secret", ClientSecret},
                {"code", authorizationCode },
                {"grant_type", "authorization_code"},
                {"scope", scope}
            };

            // Make the call to the API
            SocialHttpResponse response = SocialUtils.Http.DoHttpPostRequest("https://login.microsoftonline.com/common/oauth2/v2.0/token", null, data);

            // Parse the response
            return MicrosoftGraphTokenResponse.ParseResponse(response);

        }

        /// <summary>
        /// Gets a new access token from the specified <paramref name="refreshToken"/>.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>An instance of <see cref="MicrosoftGraphTokenResponse"/> representing the response.</returns>
        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user#5-use-the-refresh-token-to-get-a-new-access-token</cref>
        /// </see>
        public MicrosoftGraphTokenResponse GetAccessTokenFromRefreshToken(string refreshToken) {

            // Some validation
            if (String.IsNullOrWhiteSpace(ClientId)) throw new PropertyNotSetException(nameof(ClientId));
            if (String.IsNullOrWhiteSpace(ClientSecret)) throw new PropertyNotSetException(nameof(ClientSecret));
            if (String.IsNullOrWhiteSpace(RedirectUri)) throw new PropertyNotSetException(nameof(RedirectUri));
            if (String.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException(nameof(refreshToken));

            // Initialize the POST data
            IHttpPostData data = new SocialHttpPostData {
                {"client_id", ClientId},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken },
                {"redirect_uri", RedirectUri},
                {"client_secret", ClientSecret}
            };

            // Make the call to the API
            SocialHttpResponse response = SocialUtils.Http.DoHttpPostRequest("https://login.microsoftonline.com/common/oauth2/v2.0/token", null, data);

            // Parse the response
            return MicrosoftGraphTokenResponse.ParseResponse(response);

        }

        /// <summary>
        /// Virtual method that can be used for configuring a request.
        /// </summary>
        /// <param name="request">The instance of <see cref="SocialHttpRequest"/> representing the request.</param>
        protected override void PrepareHttpRequest(SocialHttpRequest request) {
            if (!String.IsNullOrWhiteSpace(AccessToken)) request.Authorization = "bearer " + AccessToken;
        }

        #endregion


    }

}