﻿using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Social.Microsoft.Graph.Scopes;

namespace Skybrud.Social.Microsoft.Graph.Models.Authentication {
    
    /// <summary>
    /// Class representing the response body of a call to get a refresh token or an access token.
    /// </summary>
    public class GraphToken : GraphObject {

        #region Properties

        /// <summary>
        /// Gets the type of the topen.
        /// </summary>
        public string TokenType { get; }

        /// <summary>
        /// Gets a collection of the scopes the user has granted.
        /// </summary>
        public GraphScopeCollection Scope { get; }

        /// <summary>
        /// Gets an instance of <see cref="TimeSpan"/> for when the access token will expire.
        /// </summary>
        public TimeSpan ExpiresIn { get; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Gets the refresh token. Only included in if the user has granted offline access.
        /// </summary>
        public string RefreshToken { get; }

        /// <summary>
        /// Gets whether the <see cref="RefreshToken"/> has a value.
        /// </summary>
        public bool HasRefreshToken => !String.IsNullOrWhiteSpace(RefreshToken);

        #endregion

        #region Constructors

        private GraphToken(JObject obj) : base(obj) {

            // Convert the "scope" string to a collection of scopes
            Scope = new GraphScopeCollection();
            foreach (string alias in obj.GetString("scope").Split(' ')) {
                GraphScope scope = GraphScope.GetScope(alias) ?? GraphScope.RegisterScope(alias);
                Scope.Add(scope);
            }

            // Parse the rest of the response body
            TokenType = obj.GetString("token_type");
            ExpiresIn = obj.GetDouble("expires_in", TimeSpan.FromSeconds);
            AccessToken = obj.GetString("access_token");
            RefreshToken = obj.GetString("refresh_token");

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GraphToken"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GraphToken"/>.</returns>
        public static GraphToken Parse(JObject obj) {
            return obj == null ? null : new GraphToken(obj);
        }

        #endregion

    }

}