﻿using System.Collections.Generic;

namespace Skybrud.Social.Microsoft.WindowsLive.Scopes {

    /// <summary>
    /// Class representing a scope of the various Microsoft APIs.
    /// </summary>
    public class WindowsLiveScope {

        #region Private fields

        private static readonly Dictionary<string, WindowsLiveScope> Scopes = new Dictionary<string, WindowsLiveScope>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the scope.
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new scope based on the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the scope.</param>
        public WindowsLiveScope(string name) {
            Name = name;
        }

        #endregion

        #region Member methods

        public override string ToString() {
            return Name;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Registers a scope in the internal dictionary.
        /// </summary>
        /// <param name="name">The name of the scope.</param>
        internal static WindowsLiveScope RegisterScope(string name) {
            WindowsLiveScope scope = new WindowsLiveScope(name);
            Scopes.Add(scope.Name, scope);
            return scope;
        }

        /// <summary>
        /// Attempts to get a scope with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the scope.</param>
        /// <returns>Gets a scope matching the specified <paramref name="name"/>, or <c>null</c> if not found.</returns>
        public static WindowsLiveScope GetScope(string name) {
            WindowsLiveScope scope;
            return Scopes.TryGetValue(name, out scope) ? scope : null;
        }

        /// <summary>
        /// Gets whether the scope with the specified <paramref name="name"/> is a known scope.
        /// </summary>
        /// <param name="name">The name of the scope.</param>
        /// <returns><c>true</c> if <paramref name="name"/> matches a known scope, otherwise <c>false</c>.</returns>
        public static bool ScopeExists(string name) {
            return Scopes.ContainsKey(name);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adding two instances of <see cref="WindowsLiveScope"/> will result in a
        /// <see cref="WindowsLiveScopeCollection"/> containing both scopes.
        /// </summary>
        /// <param name="left">The left scope.</param>
        /// <param name="right">The right scope.</param>
        /// <returns>A new collection based on <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static WindowsLiveScopeCollection operator +(WindowsLiveScope left, WindowsLiveScope right) {
            return new WindowsLiveScopeCollection(left, right);
        }

        #endregion

    }

}