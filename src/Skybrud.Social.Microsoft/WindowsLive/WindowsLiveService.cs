using System;
using Skybrud.Social.Microsoft.WindowsLive.Responses.Authentication;
using Skybrud.Social.Microsoft.WindowsLive.Endpoints;
using Skybrud.Social.Microsoft.WindowsLive.OAuth;

namespace Skybrud.Social.Microsoft.WindowsLive {

    /// <summary>
    /// Service implementation of the Windows Live API.
    /// </summary>
    public class WindowsLiveService {

        #region Properties

        /// <summary>
        /// Gets a reference to the internal OAuth client.
        /// </summary>
        public WindowsLiveOAuthClient Client { get; }

        /// <summary>
        /// Gets a reference to the Windows Live endpoint.
        /// </summary>
        public WindowsLiveEndpoint WindowsLive { get; }

        #endregion

        #region Constructors

        private WindowsLiveService(WindowsLiveOAuthClient client) {
            Client = client;
            WindowsLive = new WindowsLiveEndpoint(this);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Initialize a new service instance from the specified OAuth client.
        /// </summary>
        /// <param name="client">The OAuth client.</param>
        public static WindowsLiveService CreateFromOAuthClient(WindowsLiveOAuthClient client) {
            if (client == null) throw new ArgumentNullException(nameof(client));
            return new WindowsLiveService(client);
        }

        /// <summary>
        /// Initializes a new service instance from the specifie OAuth 2 access
        /// token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public static WindowsLiveService CreateFromAccessToken(string accessToken) {
            if (String.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return CreateFromOAuthClient(new WindowsLiveOAuthClient {
                AccessToken = accessToken
            });
        }

        /// <summary>
        /// Initializes a new instance based on the specified refresh token.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="refreshToken">The refresh token of the user.</param>
        public static WindowsLiveService CreateFromRefreshToken(string clientId, string clientSecret, string refreshToken) {

            // Some validation
            if (String.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (String.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));
            if (String.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException(nameof(refreshToken));

            // Initialize a new OAuth client
            WindowsLiveOAuthClient client = new WindowsLiveOAuthClient(clientId, clientSecret);

            // Get an access token from the refresh token.
            WindowsLiveTokenResponse response = client.GetAccessTokenFromRefreshToken(refreshToken);

            // Update the OAuth client with the access token
            client.AccessToken = response.Body.AccessToken;

            // Initialize a new service instance
            return new WindowsLiveService(client);

        }

        #endregion

    }

}