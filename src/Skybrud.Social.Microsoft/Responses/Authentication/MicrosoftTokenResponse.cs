using System;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.Models.Authentication;

namespace Skybrud.Social.Microsoft.Responses.Authentication {
    
    public class MicrosoftTokenResponse : MicrosoftResponse<MicrosoftTokenResponseBody> {
        
        #region Constructors

        private MicrosoftTokenResponse(SocialHttpResponse response) : base(response) {
            
            // Validate the response
            ValidateResponse(response);

            // Parse the response body
            Body = ParseJsonObject(response.Body, MicrosoftTokenResponseBody.Parse);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="response"/> into an instance of <see cref="MicrosoftTokenResponse"/>.
        /// </summary>
        /// <param name="response">The response to be parsed.</param>
        /// <returns>An instance of <see cref="MicrosoftTokenResponse"/>.</returns>
        public static MicrosoftTokenResponse ParseResponse(SocialHttpResponse response) {
            if (response == null) throw new ArgumentNullException(nameof(response));
            return new MicrosoftTokenResponse(response);
        }

        #endregion

    }

}