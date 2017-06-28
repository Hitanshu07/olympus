using System;
using System.Text.RegularExpressions;

namespace WebInkLibrary.Utils.DateTimeHelper
{
    public static class HtmlTagsRemoval
    {
        public static string GetCleanText(string content, int securityLevel)
        {
            var cleanContent = string.Empty;
            switch (securityLevel)
            {
                case 1:
                    cleanContent = CleanScriptTags(content);
                    break;
                case 2:
                    cleanContent = CleanScriptTags(content);
                    cleanContent = RemoveMaliciousHtmlTags(cleanContent);
                    break;
                case 3:
                    cleanContent = CleanScriptTags(content);
                    cleanContent = RemoveMaliciousHtmlTags(cleanContent);
                    cleanContent = CleanEmbedTag(cleanContent);
                    break;
                case 4:
                    cleanContent = CleanScriptTags(content);
                    cleanContent = RemoveMaliciousHtmlTags(cleanContent);
                    cleanContent = CleanEmbedTag(cleanContent);
                    cleanContent = CleanImgTag(cleanContent);
                    break;
                case 5:
                    cleanContent = CleanScriptTags(content);
                    cleanContent = RemoveMaliciousHtmlTags(cleanContent);
                    cleanContent = CleanEmbedTag(cleanContent);
                    cleanContent = CleanImgTag(cleanContent);
                    cleanContent = CleanAllOtherTags(cleanContent);
                    break;
            }


            return cleanContent;
        }

        private static string RemoveMaliciousHtmlTags(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                content = Regex.Replace(content,
                    @"(<applet>).*(</applet>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<body>).*(</body>)", string.Empty,
                    RegexOptions.IgnoreCase);

                content = Regex.Replace(content,
                    @"(<frame>).*(</frame>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<frameset>).*(</frameset>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<html>).*(</html>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<iframe>).*(</iframe>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<style>).*(</style>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<layer>).*(</layer>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<link>).*(</link>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<ilayer>).*(</ilayer>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<meta>).*(</meta>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<object>).*(</object>)", string.Empty,
                    RegexOptions.IgnoreCase);
                content = Regex.Replace(content,
                    @"(<meta>).*(</meta>)", string.Empty,
                    RegexOptions.IgnoreCase);

                return content;
            }
            return string.Empty;
        }

        private static string CleanEmbedTag(string content)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            content = Regex.Replace(content,
                @"(<embed>).*(</embed>)", string.Empty,
                RegexOptions.IgnoreCase);
            return content;
        }


        private static string CleanImgTag(string content)
        {
            if (string.IsNullOrEmpty(content)) return string.Empty;
            content = Regex.Replace(content,
                @"(<img>).*(</img>)", string.Empty,
                RegexOptions.IgnoreCase);

            content = Regex.Replace(content,
                @"(<img).*(</img>)", string.Empty,
                RegexOptions.IgnoreCase);

            content = Regex.Replace(content,
                @"(<img).*(/>)", string.Empty,
                RegexOptions.IgnoreCase);
            return content;
        }

        private static string CleanScriptTags(string content)
        {
            try
            {
                //string sMyScreenPath = absolutePath;
                //string[] values = sMyScreenPath.Split('/');
                //string filename = values.Last();
                // var match = Regex.Match(content, @"(<script).*(</script>)");
                //if (match.Length > 0)
                //{
                //    try
                //    {
                //        //SendMailToAdminForCleanTagsError(Content, filename);
                //        //LogIntoDatabase(filename);
                //    }

                //    catch (Exception)
                //    {
                //    }
                //}

                content = Regex.Replace(content,
                    @"(<script>).*(</script>)", string.Empty,
                    RegexOptions.IgnoreCase);

                content = Regex.Replace(content,
                    @"(<script).*(</script>)", string.Empty,
                    RegexOptions.IgnoreCase);


                return content;


            }
            catch
            {
                return string.Empty;
            }

        }

        private static string CleanAllOtherTags(string content)
        {
            //  Content = Regex.Replace(Content, @"(<).*(>)", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(content)) return string.Empty;
            content = Regex.Replace(content, @"<.*?>", string.Empty,
                RegexOptions.IgnoreCase);
            return content;
        }


        // If valuenis valid numeric value
        public static bool IsNumeric(string content)
        {
            int number;
            return Int32.TryParse(content, out number);
        }

        // If valuenis valid boolean value
        public static bool IsBoolean(string content)
        {
            bool flag;
            return Boolean.TryParse(content, out flag);
        }

        // If valuenis valid date time value
        public static bool IsDateTime(string content)
        {
            DateTime date;
            return DateTime.TryParse(content, out date);
        }

        // If value is valid numeric value
        public static int GetNumeric(string content)
        {
            int number;
            Int32.TryParse(content, out number);
            return number;
        }

        // If value is valid DateTime value
        public static DateTime GetDateTime(string content)
        {
            DateTime date;
            DateTime.TryParse(content, out date);
            return date;
        }

        // If value is valid DateTime value
        public static bool GetBoolean(string content)
        {
            bool flag;
            Boolean.TryParse(content, out flag);
            return flag;
        }

        public static string ValidValueToAdd(string txtToAdd)
        {
            return GetCleanText(txtToAdd.Replace("\n", string.Empty), 5);
        }

        public static string ValidValueToAdd(string txtToAdd, int maxLength)
        {
            string valToadd = ValidValueToAdd(txtToAdd);
            if (maxLength > 0)
                valToadd = valToadd.Length > maxLength ? valToadd.Substring(0, maxLength - 1) : valToadd;
            return valToadd;
        }
    }
}
