using Skybrud.Social.Microsoft.Graph.Endpoints.Raw;
using Skybrud.Social.Microsoft.Graph.Responses.Users;

namespace Skybrud.Social.Microsoft.Graph.Endpoints {

    public class GraphUsersEndpoint {

        #region Properties

        /// <summary>
        /// Gets a reference to the Microsoft service.
        /// </summary>
        public GraphService Service { get; }

        /// <summary>
        /// A reference to the raw endpoint.
        /// </summary>
        public GraphUsersRawEndpoint Raw => Service.Client.Users;

        #endregion

        #region Constructors

        internal GraphUsersEndpoint(GraphService service) {
            Service = service;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets information about the authenticated user.
        /// </summary>
        /// <returns>An instance of <see cref="GraphGetUserResponse"/> representing the response.</returns>
        public GraphGetUserResponse GetSelf() {
            return GraphGetUserResponse.ParseResponse(Raw.GetSelf());
        }

        /// <summary>
        /// Gets information about the user with the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An instance of <see cref="GraphGetUserResponse"/> representing the response.</returns>
        public GraphGetUserResponse GetUser(string userId) {
            return GraphGetUserResponse.ParseResponse(Raw.GetUser(userId));
        }

        #endregion

    }

}