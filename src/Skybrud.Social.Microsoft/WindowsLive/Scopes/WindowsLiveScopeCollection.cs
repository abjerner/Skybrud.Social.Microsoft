using System;
using System.Collections.Generic;
using System.Linq;

namespace Skybrud.Social.Microsoft.WindowsLive.Scopes {
    
    /// <summary>
    /// Class representing a collection of scopes for the various Microsoft APIs.
    /// </summary>
    public class WindowsLiveScopeCollection {

        #region Private fields

        private readonly List<WindowsLiveScope> _list = new List<WindowsLiveScope>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets an array of all the scopes added to the collection.
        /// </summary>
        public WindowsLiveScope[] Items => _list.ToArray();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new collection based on the specified <paramref name="array"/> of scopes.
        /// </summary>
        /// <param name="array">Array of scopes.</param>
        public WindowsLiveScopeCollection(params WindowsLiveScope[] array) {
            _list.AddRange(array);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds the specified <paramref name="scope"/> to the collection.
        /// </summary>
        /// <param name="scope">The scope to be added.</param>
        public void Add(WindowsLiveScope scope) {
            _list.Add(scope);
        }

        /// <summary>
        /// Returns an array of scopes based on the collection.
        /// </summary>
        /// <returns>Array of <see cref="WindowsLiveScope"/> contained in the location.</returns>
        public WindowsLiveScope[] ToArray() {
            return _list.ToArray();
        }

        /// <summary>
        /// Returns an array of strings representing each scope in the collection.
        /// </summary>
        /// <returns>Array of <see cref="String"/> representing each scope in the collection.</returns>
        public string[] ToStringArray() {
            return (from scope in _list select scope.Name).ToArray();
        }

        /// <summary>
        /// Returns a string representing the scopea added to the collection using a comma as a separator.
        /// </summary>
        /// <returns>A string of scopes separated by a comma.</returns>
        public override string ToString() {
            return String.Join(" ", from scope in _list select scope.Name);
        }

        #endregion

        #region Operator overloading

        /// <summary>
        /// Initializes a new collection based on a single <paramref name="scope"/>.
        /// </summary>
        /// <param name="scope">The scope the collection should be based on.</param>
        /// <returns>A new collection based on a single <paramref name="scope"/>.</returns>
        public static implicit operator WindowsLiveScopeCollection(WindowsLiveScope scope) {
            return new WindowsLiveScopeCollection(scope);
        }

        /// <summary>
        /// Initializes a new collection based on an <paramref name="array"/> of scopes.
        /// </summary>
        /// <param name="array">The array of scopes the collection should be based on.</param>
        /// <returns>A new collection based on an <paramref name="array"/> of scopes.</returns>
        public static implicit operator WindowsLiveScopeCollection(WindowsLiveScope[] array) {
            return new WindowsLiveScopeCollection(array ?? new WindowsLiveScope[0]);
        }

        /// <summary>
        /// Adds <paramref name="scope"/> to <paramref name="collection"/> using the plus operator.
        /// </summary>
        /// <param name="collection">The collection to which <paramref name="scope"/> will be added.</param>
        /// <param name="scope">The scope to be added to the <paramref name="collection"/>.</param>
        public static WindowsLiveScopeCollection operator +(WindowsLiveScopeCollection collection, WindowsLiveScope scope) {
            collection.Add(scope);
            return collection;
        }

        #endregion

    }

}