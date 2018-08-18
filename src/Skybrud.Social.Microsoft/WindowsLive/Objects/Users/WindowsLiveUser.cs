using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Time;
using Skybrud.Social.Microsoft.Objects;

namespace Skybrud.Social.Microsoft.WindowsLive.Objects.Users {
    
    /// <summary>
    /// Class representing a Windows Live user.
    /// </summary>
    public class WindowsLiveUser : MicrosoftObject {

        #region Properties

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Gender { get; private set; }

        public string Link { get; private set; }

        public WindowsLiveUserEmailsInfo Emails { get; private set; }

        public string Locale { get; private set; }

        public EssentialsDateTime UpdatedTime { get; private set; }

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