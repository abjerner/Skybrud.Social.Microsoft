using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Social.Microsoft.WindowsLive.Models.Users {
    
    
    public class WindowsLiveUserEmailsInfo : WindowsLiveObject {

        #region Properties

        public string Preferred { get; }

        public string Account { get; }

        public string Personal { get; }

        public string Business { get; }

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