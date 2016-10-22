using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Sitemap.Presentation.Services
{
    class RegexPatternService
    {
        ///<summary>
        ///Return a URL without scheme, if it's necessary
        ///</summary>
        public string CheckHttp(string url)
        {
            Regex regexHttp = new Regex(@"^https://|^http://");
            if (Regex.IsMatch(url, regexHttp.ToString()))
            {
                return regexHttp.Replace(url, string.Empty);
            }
            return url;

        }
        /// <summary>
        /// Add the input root relative path to the input domain, if it's necessary
        /// </summary>
        /// <param name="rootRelativePath"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public string RootRelativePathToAbsolute(string rootRelativePath, string domain)
        {
            Regex regexRelativePathToAbsolute = new Regex(@"(^/)|(^\w+/)");
            Regex regexRelativePathWithSlash = new Regex(@"(^/)");
            Regex regexRootRelativePathWithoutSlash = new Regex(@"(^\w+/)");
            if (Regex.IsMatch(rootRelativePath, regexRelativePathWithSlash.ToString()))
            {
                return regexRelativePathToAbsolute.Replace(rootRelativePath, domain); ;
            }
            else if (Regex.IsMatch(rootRelativePath, regexRootRelativePathWithoutSlash.ToString()))
            {
                return domain + rootRelativePath;
            }
            return rootRelativePath;
        }
        /// <summary>
        /// Remove last backslash in the input string, if it's necessary
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string RemoveLastBSlash(string url)
        {
            Regex regexRemoveLastBackSlash = new Regex(@"/+$");
            if (Regex.IsMatch(url, regexRemoveLastBackSlash.ToString()))
            {
                return regexRemoveLastBackSlash.Replace(url, string.Empty);
            }
            return url;
        }
        /// <summary>
        /// Remove a double backslash at the beginning
        /// of the input string, if it's necessary
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string RemoveDoubleBSlash(string url)
        {
            Regex regexRemoveDoubleBackSlash = new Regex(@"^//");
            if (Regex.IsMatch(url, regexRemoveDoubleBackSlash.ToString()))
            {
                return regexRemoveDoubleBackSlash.Replace(url, string.Empty); ;
            }
            return url;
        }
        /// <summary>
        /// Convert the relative path to absolute
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="parentPagePath"></param>
        /// <returns></returns>
        public string RelativePath(string relativePath, string parentPagePath)
        {
            Regex regexRelativePath = new Regex(@"\A(\.\./)+");
            if (Regex.IsMatch(relativePath, regexRelativePath.ToString()))
            {
                string temp = string.Empty;
                char splitSymbols = '/';
                int counter = Regex.Matches(relativePath, @"(?=(\.\./)+)").Count;
                string[] partsOfUrl = parentPagePath.Split(splitSymbols);
                for (int j = 0; j < partsOfUrl.Count() - counter - 1; j++)
                {
                    temp += partsOfUrl[j] + "/";
                }
                return temp + regexRelativePath.Replace(relativePath, string.Empty);
            }
            return relativePath;
        }
        /// <summary>
        /// Check the input URL belong to the input domain
        /// </summary>
        /// <param name="url"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool CheckDomain(string url, string host)
        {
            Regex regexCheckDomain = new Regex(host + @"(\W*)|^/(\w+)|\A(\.\./)+(\w+)");
            if (Regex.IsMatch(url, regexCheckDomain.ToString()))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// If URL have a GET request or anchor return true
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CheckReqOrAnc(string url)
        {
            Regex regexCheckGetRequest = new Regex(@"(\S*#\S*)|(\S*\?\S*)");
            if (Regex.IsMatch(url, regexCheckGetRequest.ToString()))
            {
                return true;
            }
            return false;
        }
    }
}