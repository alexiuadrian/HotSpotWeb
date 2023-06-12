using System.Collections.Generic;
using HotSpotWeb.Debugging;

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

        public static readonly Dictionary<int, string> Status = new Dictionary<int, string>()
        {
            { 0, "Pending" },
            { 1, "Active" },
            { 2, "Inactive" },
            { 3, "Deleted" },
        };

        public static readonly Dictionary<int, string> Languages = new Dictionary<int, string>()
        {
            { 0, "Ruby" },
            { 1, "C#" },
            { 2, "Javascript" }
        };
    }
}
