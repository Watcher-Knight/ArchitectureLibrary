using System.Text.RegularExpressions;

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
				text = text.Substring(0, 1).ToUpper() + text.Substring(1);
				text = Regex.Replace(text, @"(\S)([A-Z])", "$1 $2");
                text = Regex.Replace(text, @"(\D)(\d)", "$1 $2");
			}
	
            return text;
        }
	}
}