using System.Text.RegularExpressions;

namespace WebInkLibrary.Utils.HtmlHelper
{
    public class HtmlHelper
    {
        public string EscapeIllegalCharacter(string strValue)
        {
            return strValue.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("<br />", "\n");
        }

        public string ReplaceIllegalCharacter(string strValue)
        {
            return strValue.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("\n", "<br />").Replace("&rsquo;", "'");
        }
        public static string GetFileNameWebSafe(string orignalFileName, int maxLength)
        {
            try
            {
                // string CheckPreviousFileName = OrignalFileName;
                if (orignalFileName.Length > 0) // && OrignalFileName != "0"
                {

                    var regexpForFileName = new Regex(@"[^a-zA-Z0-9_.]");
                    var fileExt = System.IO.Path.GetExtension(orignalFileName);
                    string newFileName;
                    if (regexpForFileName.IsMatch(orignalFileName))
                    {
                        //for (int i = 0; i <= CheckPreviousFileName.Length; i++)
                        //{
                        //    if (System.Text.RegularExpressions.Regex.IsMatch(OrignalFileName, "[^a-zA-Z0-9_.]"))
                        //    {

                        var validName = Regex.Replace(orignalFileName, "[^a-zA-Z0-9_.]", "", RegexOptions.Compiled);
                        newFileName = validName;

                        //    }

                        //    else
                        //    { }
                        //}
                    }
                    else
                    { newFileName = orignalFileName; }

                    newFileName = newFileName.Replace(System.IO.Path.GetExtension(newFileName), "");
                    newFileName = newFileName.Replace(".", "");
                    //NewFileName = NewFileName.Replace("_", "");

                    if (newFileName.Length > 32)
                    {
                        newFileName = newFileName.Remove(32);

                    }
                    if (maxLength > 0 && (newFileName.Length + fileExt.Length) > maxLength)
                        newFileName = newFileName.Substring(0, (maxLength - fileExt.Length) - 1);
                    newFileName = newFileName + fileExt;
                    return newFileName;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }


    }
}
