using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.Social.Microsoft.WindowsLive.Models {

    /// <summary>
    /// Class representing a basic object from the Windows Live API derived from an instance of <see cref="JObject"/>.
    /// </summary>
    public class WindowsLiveObject {

        #region Properties

        /// <summary>
        /// Gets a reference the internal <see cref="JObject"/> the object was created from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> representing the object.</param>
        protected WindowsLiveObject(JObject obj) {
            JObject = obj;
        }

        #endregion

    }

}