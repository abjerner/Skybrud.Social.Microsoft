using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Time;
using Skybrud.Social.Microsoft.Models;

namespace Skybrud.Social.Microsoft.WindowsLive.Models.Users {
    
    /// <summary>
    /// Class representing a Windows Live user.
    /// </summary>
    public class WindowsLiveUser : MicrosoftObject {

        #region Properties

        public string Id { get; }

        public string Name { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Gender { get; }

        public string Link { get; }

        public WindowsLiveUserEmailsInfo Emails { get; }

        public string Locale { get; }

        public EssentialsDateTime UpdatedTime { get; }

        #endregion

        #region Constructors

        private WindowsLiveUser(JObject obj) : base(obj) {
            Id = obj.GetString("id");
            Name = obj.GetString("name");
            FirstName = obj.GetString("first_name");
            LastName = obj.GetString("last_name");
            Gender = obj.GetString("gender");
            Link = obj.GetString("link");
            Emails = obj.GetObject("emails", WindowsLiveUserEmailsInfo.Parse);
            Locale = obj.GetString("locale");
            UpdatedTime = obj.GetString("updated_time", EssentialsDateTime.Parse);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="WindowsLiveUser"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="WindowsLiveUser"/>.</returns>
        public static WindowsLiveUser Parse(JObject obj) {
            return obj == null ? null : new WindowsLiveUser(obj);
        }

        #endregion

    }

}