using System.Net;
using Skybrud.Social.Http;
using Skybrud.Social.Microsoft.WindowsLive.Exceptions;

namespace Skybrud.Social.Microsoft.WindowsLive.Responses {

    /// <summary>
    /// Class representing a response from one of the the various Microsoft APIs.
    /// </summary>
    public abstract class WindowsLiveResponse : SocialResponse {

        #region Constructor

        protected WindowsLiveResponse(SocialHttpResponse response) : base(response) { }

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
            throw new WindowsLiveHttpException(response);

        }

        #endregion

    }

    /// <summary>
    /// Class representing a response from one of the the various Microsoft APIs.
    /// </summary>
    public class WindowsLiveResponse<T> : WindowsLiveResponse {

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
        protected WindowsLiveResponse(SocialHttpResponse response) : base(response) { }

        #endregion

    }

}