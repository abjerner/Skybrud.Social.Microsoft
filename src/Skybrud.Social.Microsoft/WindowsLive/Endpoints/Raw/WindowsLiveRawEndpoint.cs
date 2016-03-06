using System;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.OAuth;

namespace Skybrud.Social.Microsoft.WindowsLive.Endpoints.Raw {
    
    /// <summary>
    /// Raw implementation of the Windows Live endpoint.
    /// </summary>
    public class WindowsLiveRawEndpoint {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent OAuth client.
        /// </summary>
        public MicrosoftOAuthClient Client { get; private set; }

        #endregion

        #region Constructors

        internal WindowsLiveRawEndpoint(MicrosoftOAuthClient client) {
            Client = client;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets information about the authenticated user.
        /// </summary>
        /// <returns>Returns an instance of <see cref="SocialHttpResponse"/> representing the raw response.</returns>
        public SocialHttpResponse GetSelf() {
            return Client.DoHttpGetRequest("https://apis.live.net/v5.0/me");
        }

        /// <summary>
        /// Gets information about the user with the specified <code>userId</code>.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Returns an instance of <see cref="SocialHttpResponse"/> representing the raw response.</returns>
        public SocialHttpResponse GetUser(string userId) {
            if (String.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");
            return Client.DoHttpGetRequest("https://apis.live.net/v5.0/" + userId);
        }

        #endregion

    }

}