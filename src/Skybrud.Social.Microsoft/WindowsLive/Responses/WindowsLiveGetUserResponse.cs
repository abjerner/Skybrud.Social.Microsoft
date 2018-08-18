using System;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.Responses;
using Skybrud.Social.Microsoft.WindowsLive.Objects.Users;

namespace Skybrud.Social.Microsoft.WindowsLive.Responses {

    /// <summary>
    /// Class representing the response of a call to get information about a single Windows Live user.
    /// </summary>
    public class WindowsLiveGetUserResponse : MicrosoftResponse<WindowsLiveUser> {

        #region Constructors

        private WindowsLiveGetUserResponse(SocialHttpResponse response) : base(response) {

            // Validate the response
            ValidateResponse(response);

            // Parse the response body
            Body = ParseJsonObject(response.Body, WindowsLiveUser.Parse);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="response"/> into an instance of <see cref="WindowsLiveGetUserResponse"/>.
        /// </summary>
        /// <param name="response">The response to be parsed.</param>
        /// <returns>An instance of <see cref="WindowsLiveGetUserResponse"/>.</returns>
        public static WindowsLiveGetUserResponse ParseResponse(SocialHttpResponse response) {
            if (response == null) throw new ArgumentNullException(nameof(response));
            return new WindowsLiveGetUserResponse(response);
        }

        #endregion

    }

}