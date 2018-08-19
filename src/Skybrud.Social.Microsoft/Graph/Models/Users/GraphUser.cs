using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Social.Microsoft.Graph.Models.Users {

    /// <summary>
    /// Class representing a user as returned by the Microsoft Graph API.
    /// </summary>
    /// <see>
    ///     <cref>https://developer.microsoft.com/en-us/graph/docs/api-reference/v1.0/resources/users</cref>
    /// </see>
    public class GraphUser : GraphObject {

        #region Properties

        /// <summary>
        /// Gets the unique identifier for the user.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the name displayed in the address book for the user.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Gets the last name of the user.
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// Gets the first name of the user.
        /// </summary>
        public string GivenName { get; }

        /// <summary>
        /// Gets the user's principal name.
        /// </summary>
        public string UserPrincipalName { get; }

        // TODO: Add support for the "businessPhones" property
        // TODO: Add support for the "jobTitle" property
        // TODO: Add support for the "mail" property
        // TODO: Add support for the "mobilePhone" property
        // TODO: Add support for the "officeLocation" property
        // TODO: Add support for the "preferredLanguage" property

        #endregion

        #region Constructors

        private GraphUser(JObject obj) : base(obj) {
            Id = obj.GetString("id");
            DisplayName = obj.GetString("displayName");
            Surname = obj.GetString("surname");
            GivenName = obj.GetString("givenName");
            UserPrincipalName = obj.GetString("userPrincipalName");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GraphUser"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GraphUser"/>.</returns>
        public static GraphUser Parse(JObject obj) {
            return obj == null ? null : new GraphUser(obj);
        }

        #endregion

    }

}