using System;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.Graph.Models.Users;

namespace Skybrud.Social.Microsoft.Graph.Responses.Users {
    
    /// <summary>
    /// Class representing a response from the Microsoft Graph API with information about a user.
    /// </summary>
    public class MicrosoftGraphGetUserResponse : MicrosoftGraphResponse<MicrosoftGraphUser> {
        
        #region Constructors

        private MicrosoftGraphGetUserResponse(SocialHttpResponse response) : base(response) {
            
            // Validate the response
            ValidateResponse(response);

            // Parse the response body
            Body = ParseJsonObject(response.Body, MicrosoftGraphUser.Parse);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="response"/> into an instance of <see cref="MicrosoftGraphGetUserResponse"/>.
        /// </summary>
        /// <param name="response">The response to be parsed.</param>
        /// <returns>An instance of <see cref="MicrosoftGraphGetUserResponse"/>.</returns>
        public static MicrosoftGraphGetUserResponse ParseResponse(SocialHttpResponse response) {
            if (response == null) throw new ArgumentNullException(nameof(response));
            return new MicrosoftGraphGetUserResponse(response);
        }

        #endregion

    }

}