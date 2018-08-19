namespace Skybrud.Social.Microsoft.Graph.Scopes {
    
    /// <see>
    ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/permissions_reference#user-permissions</cref>
    /// </see>
    public static class MicrosoftGraphScopes {

        /// <summary>
        /// Allows the app to read and update user data, even when they are not currently using the app.
        /// </summary>
        public static readonly MicrosoftGraphScope OfflineAccess = MicrosoftGraphScope.RegisterScope(
            "offline_access",
            "Access user's data anytime",
            "Allows the app to read and update user data, even when they are not currently using the app."
        );

        /// <see>
        ///     <cref>https://developer.microsoft.com/en-us/graph/docs/concepts/permissions_reference#user-permissions</cref>
        /// </see>
        public static class Users {

            /// <summary>
            /// Allows users to sign-in to the app, and allows the app to read the profile of signed-in users. It also allows the app to read basic company information of signed-in users.
            /// </summary>
            public static readonly MicrosoftGraphScope Read = MicrosoftGraphScope.RegisterScope(
                "User.Read",
                "Sign-in and read user profile", "Allows users to sign-in to the app, and allows the app to read the profile of signed-in users. It also allows the app to read basic company information of signed-in users."
            );

            /// <summary>
            /// Allows the app to read the signed-in user's full profile. It also allows the app to update the signed-in user's profile information on their behalf.
            /// </summary>
            public static readonly MicrosoftGraphScope ReadWrite = MicrosoftGraphScope.RegisterScope(
                "User.ReadWrite",
                "Sign-in and read user profile",
                "Allows the app to read the signed-in user's full profile. It also allows the app to update the signed-in user's profile information on their behalf."
            );

            /// <summary>
            /// Allows the app to read a basic set of profile properties of other users in your organization on behalf of the signed-in user. This includes display name, first and last name, email address, open extensions and photo. Also allows the app to read the full profile of the signed-in user.
            /// </summary>
            public static readonly MicrosoftGraphScope ReadBasicAll = MicrosoftGraphScope.RegisterScope(
                "User.ReadBasic.All",
                "Read all users' basic profiles",
                "Allows the app to read a basic set of profile properties of other users in your organization on behalf of the signed-in user. This includes display name, first and last name, email address, open extensions and photo. Also allows the app to read the full profile of the signed-in user."
            );

            /// <summary>
            /// Allows the app to read the full set of profile properties, reports, and managers of other users in your organization, on behalf of the signed-in user.
            /// 
            /// <strong>Requires admin content</strong>
            /// </summary>
            public static readonly MicrosoftGraphScope ReadAll = MicrosoftGraphScope.RegisterScope(
                "User.Read.All",
                "Read all users' full profiles",
                "Allows the app to read the full set of profile properties, reports, and managers of other users in your organization, on behalf of the signed-in user.",
                true
            );

            /// <summary>
            /// Allows the app to read and write the full set of profile properties, reports, and managers of other users in your organization, on behalf of the signed-in user. Also allows the app to create and delete users as well as reset user passwords on behalf of the signed-in user.
            /// 
            /// </summary>
            public static readonly MicrosoftGraphScope ReadWriteAll = MicrosoftGraphScope.RegisterScope(
                "User.ReadWrite.All",
                "Read and write all users' full profiles",
                "Allows the app to read and write the full set of profile properties, reports, and managers of other users in your organization, on behalf of the signed-in user. Also allows the app to create and delete users as well as reset user passwords on behalf of the signed-in user.",
                true
            );

            /// <summary>
            /// Allows the app to invite guest users to your organization, on behalf of the signed-in user.
            /// 
            /// <strong>Requires admin content</strong>
            /// </summary>
            public static readonly MicrosoftGraphScope InviteAll = MicrosoftGraphScope.RegisterScope(
                "User.Invite.All",
                "Invite guest users to the organization",
                "Allows the app to invite guest users to your organization, on behalf of the signed-in user.",
                true
            );

            /// <summary>
            /// Allows the app to export an organizational user's data, when performed by a Company Administrator.
            /// 
            /// <strong>Requires admin content</strong>
            /// </summary>
            public static readonly MicrosoftGraphScope ExportAll = MicrosoftGraphScope.RegisterScope(
                "User.Export.All",
                "Export users' data",
                "Allows the app to export an organizational user's data, when performed by a Company Administrator.",
                true
            );

            /// <summary>
            /// Gets an array of all scopes of the users endpoint.
            /// </summary>
            public static readonly MicrosoftGraphScope[] All = { Read, ReadWrite, ReadBasicAll, ReadAll, ReadWriteAll, InviteAll, ExportAll };

        }

    }

}