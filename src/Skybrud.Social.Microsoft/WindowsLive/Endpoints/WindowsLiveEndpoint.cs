using Skybrud.Social.Microsoft.WindowsLive.Endpoints.Raw;
using Skybrud.Social.Microsoft.WindowsLive.Responses;
using Skybrud.Social.Microsoft.WindowsLive.Responses.Users;

namespace Skybrud.Social.Microsoft.WindowsLive.Endpoints {
    
    /// <summary>
    /// Implementation of the Windows Live endpoint.
    /// </summary>
    public class WindowsLiveEndpoint {

        #region Properties

        /// <summary>
        /// Gets a reference to the Microsoft service.
        /// </summary>
        public WindowsLiveService Service { get; }

        /// <summary>
        /// A reference to the raw endpoint.
        /// </summary>
        public WindowsLiveUsersRawEndpoint Raw => Service.Client.Users;

        #endregion

        #region Constructors

        internal WindowsLiveEndpoint(WindowsLiveService service) {
            Service = service;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets information about the authenticated user.
        /// </summary>
        /// <returns>An instance of <see cref="WindowsLiveGetUserResponse"/> representing the response.</returns>
        public WindowsLiveGetUserResponse GetSelf() {
            return WindowsLiveGetUserResponse.ParseResponse(Raw.GetSelf());
        }

        /// <summary>
        /// Gets information about the user with the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An instance of <see cref="WindowsLiveGetUserResponse"/> representing the response.</returns>
        public WindowsLiveGetUserResponse GetUser(string userId) {
            return WindowsLiveGetUserResponse.ParseResponse(Raw.GetUser(userId));
        }

        #endregion

    }

}