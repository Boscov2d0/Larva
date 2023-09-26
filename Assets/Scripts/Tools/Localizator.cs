using System;
using System.Collections.Generic;

namespace Larva.Tools
{
    public static class Localizator
    {
        public static string GetLocalizedValue<T>(Dictionary<string, string> localizedText, T key)
        {
            if (localizedText.ContainsKey(key.ToString()))
            {
                return localizedText[key.ToString()];
            }
            else
            {
                throw new Exception("Localized text with key \"" + key + "\" not found");
            }
        }
    }
}