using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Social.Microsoft.Objects;

namespace Skybrud.Social.Microsoft.WindowsLive.Objects.Users {
    
    /// <summary>
    /// Class representing email information about a Windows Live user.
    /// </summary>
    public class WindowsLiveUserEmailsInfo : MicrosoftObject {

        #region Properties

        public string Preferred { get; private set; }

        public string Account { get; private set; }

        public string Personal { get; private set; }

        public string Business { get; private set; }

        #endregion

        #region Constructors

        private WindowsLiveUserEmailsInfo(JObject obj) : base(obj) {
            Preferred = obj.GetString("preferred");
            Account = obj.GetString("account");
            Personal = obj.GetString("personal");
            Business = obj.GetString("business");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="WindowsLiveUserEmailsInfo"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="WindowsLiveUserEmailsInfo"/>.</returns>
        public static WindowsLiveUserEmailsInfo Parse(JObject obj) {
            return obj == null ? null : new WindowsLiveUserEmailsInfo(obj);
        }

        #endregion

    }

}