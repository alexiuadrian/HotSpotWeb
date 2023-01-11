﻿using HotSpotWeb.Debugging;

namespace HotSpotWeb
{
    public class HotSpotWebConsts
    {
        public const string LocalizationSourceName = "HotSpotWeb";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3de9841a505a439b85fc4a34cadbd393";
    }
}