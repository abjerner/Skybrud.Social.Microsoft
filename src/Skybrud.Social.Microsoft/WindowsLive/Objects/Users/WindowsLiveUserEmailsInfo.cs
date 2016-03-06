using Newtonsoft.Json.Linq;
using Skybrud.Social.Json.Extensions.JObject;
using Skybrud.Social.Microsoft.Objects;

namespace Skybrud.Social.Microsoft.WindowsLive.Objects.Users {
    
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
        /// Parses the specified <code>obj</code> into an instance of <see cref="WindowsLiveUserEmailsInfo"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>Returns an instance of <see cref="WindowsLiveUserEmailsInfo"/>.</returns>
        public static WindowsLiveUserEmailsInfo Parse(JObject obj) {
            return obj == null ? null : new WindowsLiveUserEmailsInfo(obj);
        }

        #endregion

    }

}