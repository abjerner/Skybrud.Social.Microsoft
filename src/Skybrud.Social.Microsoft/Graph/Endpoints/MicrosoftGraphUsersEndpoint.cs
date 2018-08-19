using Skybrud.Social.Microsoft.Graph.Endpoints.Raw;
using Skybrud.Social.Microsoft.Graph.Responses.Users;

namespace Skybrud.Social.Microsoft.Graph.Endpoints {

    public class MicrosoftGraphUsersEndpoint {

        #region Properties

        /// <summary>
        /// Gets a reference to the Microsoft service.
        /// </summary>
        public MicrosoftGraphService Service { get; }

        /// <summary>
        /// A reference to the raw endpoint.
        /// </summary>
        public MicrosoftGraphUsersRawEndpoint Raw => Service.Client.Users;

        #endregion

        #region Constructors

        internal MicrosoftGraphUsersEndpoint(MicrosoftGraphService service) {
            Service = service;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets information about the authenticated user.
        /// </summary>
        /// <returns>An instance of <see cref="MicrosoftGraphGetUserResponse"/> representing the response.</returns>
        public MicrosoftGraphGetUserResponse GetSelf() {
            return MicrosoftGraphGetUserResponse.ParseResponse(Raw.GetSelf());
        }

        /// <summary>
        /// Gets information about the user with the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An instance of <see cref="MicrosoftGraphGetUserResponse"/> representing the response.</returns>
        public MicrosoftGraphGetUserResponse GetUser(string userId) {
            return MicrosoftGraphGetUserResponse.ParseResponse(Raw.GetUser(userId));
        }

        #endregion

    }

}