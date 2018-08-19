using System.Collections.Generic;

namespace Skybrud.Social.Microsoft.Graph.Scopes {

    /// <summary>
    /// Class representing a scope of the various Microsoft APIs.
    /// </summary>
    public class GraphScope {

        #region Private fields

        private static readonly Dictionary<string, GraphScope> Scopes = new Dictionary<string, GraphScope>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the scope.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Gets the name of the scope.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the scope.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets whether the scope requires consent from an administrator.
        /// </summary>
        public bool RequiresAdminConsent { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new scope based on the specified <paramref name="alias"/>.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        public GraphScope(string alias) {
            Alias = alias;
        }

        /// <summary>
        /// Initializes a new scope based on the specified <paramref name="alias"/>, <paramref name="name"/> and <paramref name="description"/>.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        /// <param name="name">The name of the scope.</param>
        /// <param name="description">The description of the scope.</param>
        public GraphScope(string alias, string name, string description) {
            Alias = alias;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Initializes a new scope based on the specified <paramref name="alias"/>, <paramref name="name"/> and <paramref name="description"/>.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        /// <param name="name">The name of the scope.</param>
        /// <param name="description">The description of the scope.</param>
        /// <param name="consent">Whether the scope requires consent from an administrator.</param>
        public GraphScope(string alias, string name, string description, bool consent) {
            Alias = alias;
            Name = name;
            Description = description;
            RequiresAdminConsent = consent;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns a string representation of the scope.
        /// </summary>
        /// <returns>A string representing the scope.</returns>
        public override string ToString() {
            return Alias;
        }

        #endregion
        
        #region Static methods

        /// <summary>
        /// Registers a scope in the internal dictionary.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        internal static GraphScope RegisterScope(string alias) {
            GraphScope scope = new GraphScope(alias);
            Scopes.Add(scope.Alias, scope);
            return scope;
        }

        /// <summary>
        /// Registers a scope in the internal dictionary.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        /// <param name="name">The name of the scope.</param>
        /// <param name="description">The description of the scope.</param>
        internal static GraphScope RegisterScope(string alias, string name, string description) {
            GraphScope scope = new GraphScope(alias, name, description);
            Scopes.Add(scope.Alias, scope);
            return scope;
        }

        /// <summary>
        /// Registers a scope in the internal dictionary.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        /// <param name="name">The name of the scope.</param>
        /// <param name="description">The description of the scope.</param>
        /// <param name="consent">Whether the scope requires consent from an administrator.</param>
        internal static GraphScope RegisterScope(string alias, string name, string description, bool consent) {
            GraphScope scope = new GraphScope(alias, name, description, consent);
            Scopes.Add(scope.Alias, scope);
            return scope;
        }

        /// <summary>
        /// Attempts to get a scope with the specified <paramref name="alias"/>.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        /// <returns>Gets a scope matching the specified <paramref name="alias"/>, or <c>null</c> if not found.</returns>
        public static GraphScope GetScope(string alias) {
            return Scopes.TryGetValue(alias, out GraphScope scope) ? scope : null;
        }

        /// <summary>
        /// Gets whether the scope with the specified <paramref name="alias"/> is a known scope.
        /// </summary>
        /// <param name="alias">The alias of the scope.</param>
        /// <returns><c>true</c> if <paramref name="alias"/> matches a known scope, otherwise <c>false</c>.</returns>
        public static bool ScopeExists(string alias) {
            return Scopes.ContainsKey(alias);
        }

        #endregion
        
        #region Operators

        /// <summary>
        /// Adding two instances of <see cref="GraphScope"/> will result in a <see cref="GraphScopeCollection"/> containing both scopes.
        /// </summary>
        /// <param name="left">The left scope.</param>
        /// <param name="right">The right scope.</param>
        /// <returns>A new collection based on <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static GraphScopeCollection operator +(GraphScope left, GraphScope right) {
            return new GraphScopeCollection(left, right);
        }

        #endregion

    }

}