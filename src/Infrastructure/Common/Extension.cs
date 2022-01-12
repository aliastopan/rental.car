using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
    public static class Extension
    {
        static public string UpperCaseFirstCharacter(this string text) {
            return Regex.Replace(text, "^[a-z]", m => m.Value.ToUpper());
        }

        static public string ShortenGuid(this Guid guid){
            return Convert.ToBase64String(guid.ToByteArray());
        }
    }


}