using System;
using System.Net;
using Skybrud.Social.Http;

namespace Skybrud.Social.Microsoft.Exceptions {

    /// <summary>
    /// Class representing an exception based on an error from one of the Microsoft APIs.
    /// </summary>
    public class MicrosoftHttpException : Exception {

        #region Properties

        /// <summary>
        /// Gets a reference to the underlying <see cref="SocialHttpResponse"/>.
        /// </summary>
        public SocialHttpResponse Response { get; }

        /// <summary>
        /// Gets the HTTP status code returned by the Microsoft API.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new exception based on the specified <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The instance of <see cref="SocialHttpResponse"/> representing the response.</param>
        public MicrosoftHttpException(SocialHttpResponse response) : base("Invalid response received from the Microsoft API (Status: " + ((int) response.StatusCode) + ")") {
            Response = response;
            StatusCode = response.StatusCode;
        }

        #endregion

    }

}