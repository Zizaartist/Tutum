using System;
using System.Collections.Generic;
using System.Text;

namespace Tutum.StaticValues.StringResources
{
    public enum EnumLanguages 
    {
        english,
        russian
    }

    public class AvailableLanguages
    {
        public static List<string> Languages = new List<string> 
        {
            "English",
            "Русский"
        };

        public static Dictionary<string, EnumLanguages> StringToEnum = new Dictionary<string, EnumLanguages> 
        {
            { "English", EnumLanguages.english },
            { "Русский", EnumLanguages.russian }
        };
    }
}
