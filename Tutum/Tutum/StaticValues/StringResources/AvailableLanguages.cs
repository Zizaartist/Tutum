using System;
using System.Collections.Generic;
using System.Text;

namespace Tutum.StaticValues.StringResources
{
    public class AvailableLanguages
    {
        public static List<string> Languages = new List<string> 
        {
            "English",
            "Русский"
        };

        public static Dictionary<string, string> StringToStandart = new Dictionary<string, string> 
        {
            { "English", "en-EN" },
            { "Русский", "ru-RU" }
        };

        public static Dictionary<string, string> StandartToString = new Dictionary<string, string>
        {
            { "en-EN", "English" },
            { "ru-RU", "Русский" }
        };
    }
}
