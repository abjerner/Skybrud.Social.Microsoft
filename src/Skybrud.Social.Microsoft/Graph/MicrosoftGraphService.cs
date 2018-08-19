using System;
using Skybrud.Social.Microsoft.Graph.Endpoints;
using Skybrud.Social.Microsoft.Graph.OAuth;
using Skybrud.Social.Microsoft.Graph.Responses.Authentication;

namespace Skybrud.Social.Microsoft.Graph {

    /// <summary>
    /// Service implementation of the Microsoft Graph API.
    /// </summary>
    public class MicrosoftGraphService {

        #region Properties

        /// <summary>
        /// Gets a reference to the internal OAuth client.
        /// </summary>
        public MicrosoftGraphOAuthClient Client { get; }

        /// <summary>
        /// Gets a reference to the users endpoint.
        /// </summary>
        public MicrosoftGraphUsersEndpoint Users { get; }

        #endregion

        #region Constructors

        private MicrosoftGraphService(MicrosoftGraphOAuthClient client) {
            Client = client;
            Users = new MicrosoftGraphUsersEndpoint(this);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Initialize a new service instance from the specified OAuth client.
        /// </summary>
        /// <param name="client">The OAuth client.</param>
        public static MicrosoftGraphService CreateFromOAuthClient(MicrosoftGraphOAuthClient client) {
            if (client == null) throw new ArgumentNullException(nameof(client));
            return new MicrosoftGraphService(client);
        }

        /// <summary>
        /// Initializes a new service instance from the specifie OAuth 2 access
        /// token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public static MicrosoftGraphService CreateFromAccessToken(string accessToken) {
            if (String.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return CreateFromOAuthClient(new MicrosoftGraphOAuthClient(accessToken));
        }

        /// <summary>
        /// Initializes a new instance based on the specified refresh token.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="redirectUri">Te redirect URI.</param>
        /// <param name="refreshToken">The refresh token of the user.</param>
        public static MicrosoftGraphService CreateFromRefreshToken(string clientId, string clientSecret, string redirectUri, string refreshToken) {

            // Some validation
            if (String.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (String.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));
            if (String.IsNullOrWhiteSpace(redirectUri)) throw new ArgumentNullException(nameof(redirectUri));
            if (String.IsNullOrWhiteSpace(refreshToken)) throw new ArgumentNullException(nameof(refreshToken));

            // Initialize a new OAuth client
            MicrosoftGraphOAuthClient client = new MicrosoftGraphOAuthClient(clientId, clientSecret, redirectUri);

            // Get an access token from the refresh token.
            MicrosoftGraphTokenResponse response = client.GetAccessTokenFromRefreshToken(refreshToken);

            // Update the OAuth client with the access token
            client.AccessToken = response.Body.AccessToken;

            // Initialize a new service instance
            return new MicrosoftGraphService(client);

        }

        #endregion

    }

}