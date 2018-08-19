using System;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.Graph.OAuth;

namespace Skybrud.Social.Microsoft.Graph.Endpoints.Raw {

    /// <summary>
    /// Raw implementation of the <strong>Users</strong> endpoint.
    /// </summary>
    /// <see>
    ///     <cref>https://developer.microsoft.com/en-us/graph/docs/api-reference/v1.0/resources/users</cref>
    /// </see>
    public class MicrosoftGraphUsersRawEndpoint {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent OAuth client.
        /// </summary>
        public MicrosoftGraphOAuthClient Client { get; }

        #endregion

        #region Constructors

        internal MicrosoftGraphUsersRawEndpoint(MicrosoftGraphOAuthClient client) {
            Client = client;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets information about the authenticated user.
        /// </summary>
        /// <returns>An instance of <see cref="SocialHttpResponse"/> representing the raw response.</returns>
        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/api-reference/v1.0/api/user_get</cref>
        /// </see>
        public SocialHttpResponse GetSelf() {
            return GetUser("me");
        }

        /// <summary>
        /// Gets information about the user with the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An instance of <see cref="SocialHttpResponse"/> representing the raw response.</returns>
        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/api-reference/v1.0/api/user_get</cref>
        /// </see>
        public SocialHttpResponse GetUser(string userId) {
            if (String.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            return Client.DoHttpGetRequest("https://graph.microsoft.com/v1.0/users/" + userId);
        }

        #endregion

    }

}