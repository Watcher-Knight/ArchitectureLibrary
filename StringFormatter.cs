using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ArchitectureLibrary
{
    public static class StringFormatter
	{
		public static string ToTitleCase(string text)
        {
			if (text != "")
			{
				text = Regex.Replace(text, @"_", " ");
				text = Regex.Replace(text, @"^\s+|\s+$", "");
				text = Regex.Replace(text, @"\s+", " ");
				text = CapitalizeFirst(text);
				text = Regex.Replace(text, @"(\S)([A-Z])", "$1 $2");
                text = Regex.Replace(text, @"(\D)(\d)", "$1 $2");
			}
	
            return text;
        }

		public static string CapitalizeFirst(string text)
		{
			if (text.Length > 0) return text[..1].ToUpper() + text[1..];
			return "";
		}

		public static T[] JsonToArray<T>(string json)
		{
			List<T> list = JsonUtility.FromJson<List<T>>(json);
			return list.ToArray();
		}
		public static string ArrayToJson(object[] array)
		{
			List<object> list = new(array);
			return JsonUtility.ToJson(list);
		}
	}
}