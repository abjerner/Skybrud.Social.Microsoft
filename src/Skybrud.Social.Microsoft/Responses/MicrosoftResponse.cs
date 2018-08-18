using System.Net;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.Exceptions;

namespace Skybrud.Social.Microsoft.Responses {

    /// <summary>
    /// Class representing a response from one of the the various Microsoft APIs.
    /// </summary>
    public abstract class MicrosoftResponse : SocialResponse {

        #region Constructor

        protected MicrosoftResponse(SocialHttpResponse response) : base(response) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Validates the specified <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The response to be validated.</param>
        public static void ValidateResponse(SocialHttpResponse response) {

            // Skip error checking if the server responds with an OK status code
            if (response.StatusCode == HttpStatusCode.OK) return;

            // Now throw some exceptions
            throw new MicrosoftHttpException(response);

        }

        #endregion

    }

    /// <summary>
    /// Class representing a response from one of the the various Microsoft APIs.
    /// </summary>
    public class MicrosoftResponse<T> : MicrosoftResponse {

        #region Properties

        /// <summary>
        /// Gets the body of the response.
        /// </summary>
        public T Body { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The instance of <see cref="SocialHttpResponse"/> representing the raw response.</param>
        protected MicrosoftResponse(SocialHttpResponse response) : base(response) { }

        #endregion

    }

}