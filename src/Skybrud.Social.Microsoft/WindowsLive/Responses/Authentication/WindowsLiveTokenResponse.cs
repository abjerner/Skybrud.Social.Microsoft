using System;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.WindowsLive.Models.Authentication;
using Skybrud.Social.Microsoft.WindowsLive.Responses;

namespace Skybrud.Social.Microsoft.WindowsLive.Responses.Authentication {
    
    public class WindowsLiveTokenResponse : WindowsLiveResponse<WindowsLiveTokenResponseBody> {
        
        #region Constructors

        private WindowsLiveTokenResponse(SocialHttpResponse response) : base(response) {
            
            // Validate the response
            ValidateResponse(response);

            // Parse the response body
            Body = ParseJsonObject(response.Body, WindowsLiveTokenResponseBody.Parse);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="response"/> into an instance of <see cref="WindowsLiveTokenResponse"/>.
        /// </summary>
        /// <param name="response">The response to be parsed.</param>
        /// <returns>An instance of <see cref="WindowsLiveTokenResponse"/>.</returns>
        public static WindowsLiveTokenResponse ParseResponse(SocialHttpResponse response) {
            if (response == null) throw new ArgumentNullException(nameof(response));
            return new WindowsLiveTokenResponse(response);
        }

        #endregion

    }

}