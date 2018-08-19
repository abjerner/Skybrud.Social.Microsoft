using System;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.Graph.Models.Authentication;

namespace Skybrud.Social.Microsoft.Graph.Responses.Authentication {

    /// <see>
    ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/auth_v2_user#3-get-a-token</cref>
    /// </see>
    public class MicrosoftGraphTokenResponse : MicrosoftGraphResponse<MicrosoftGraphToken> {
        
        #region Constructors

        private MicrosoftGraphTokenResponse(SocialHttpResponse response) : base(response) {
            
            // Validate the response
            ValidateResponse(response);

            // Parse the response body
            Body = ParseJsonObject(response.Body, MicrosoftGraphToken.Parse);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="response"/> into an instance of <see cref="MicrosoftGraphTokenResponse"/>.
        /// </summary>
        /// <param name="response">The response to be parsed.</param>
        /// <returns>An instance of <see cref="MicrosoftGraphTokenResponse"/>.</returns>
        public static MicrosoftGraphTokenResponse ParseResponse(SocialHttpResponse response) {
            if (response == null) throw new ArgumentNullException(nameof(response));
            return new MicrosoftGraphTokenResponse(response);
        }

        #endregion

    }

}