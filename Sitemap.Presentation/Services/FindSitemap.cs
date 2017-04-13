using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Sitemap.Presentation.Services
{
    class FindSitemap
    {
        /// <summary>
        /// Try find a sitemap on input site
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<List<string>> SitemapsFinder(string path)
        {
            ParseService parserClass = new ParseService();
            Uri uri = new Uri(path);

            string domain = uri.Scheme + "://" + uri.Host + "/";
            string defaultPath = domain + "sitemap.xml";

            string robotsPath = await SearchSitemapInRobotsAsync(domain + "robots.txt");

            List<string> urls = new List<string>();
            string pathToSitemap;

            if (robotsPath != "")
            {
                pathToSitemap = robotsPath;
            }
            else
            {
                pathToSitemap = defaultPath;
            }
            List<string> pathes = await SearchSitemapInXmlAsync(pathToSitemap);
            if (pathes != null) 
            {
                return pathes;
            }
            else if (parserClass.GetResponse(pathToSitemap) != -1)
            {
                urls.Add(pathToSitemap);
                return urls;
            }
            return null;
        }
        /// <summary>
        /// Try find pathes to sitemap on page
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        private async Task<List<string>> SearchSitemapInXmlAsync(string url)
        {
            Uri uri = new Uri(url);
            ParseService parserClass = new ParseService();
            List<string> foundSitemapPath = new List<string>();
            string xmlDocumetn = await parserClass.DownloadDocumentAsync(url);
            if (xmlDocumetn == null)
            {
                return null;
            }
            //if links was found, but it's not sitemap, then redirect to PageReader
            List<string> foundLinks = parserClass.XmlLinksParser(xmlDocumetn);
            Regex regexSearchSitemapInXml = new Regex(@"(" + uri.Host + @")+(\w|\W)+sitemap(\w|\W)*\.xml$", RegexOptions.IgnoreCase);
            for (int j = 0; j < foundLinks.Count; j++)
            {
                if (Regex.IsMatch(foundLinks[j], regexSearchSitemapInXml.ToString()))
                {
                    foundSitemapPath.Add(foundLinks[j]);
                }
            }
            if (foundSitemapPath.Count != 0)
            {
                return foundSitemapPath;
            }
            return null;

        }
        /// <summary>
        /// Try find path to sitemap on URL/robots.txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<string> SearchSitemapInRobotsAsync(string URL)
        {
            ParseService parserClass = new ParseService();
            string foundPath;
            Regex regexSearchSitemapInRobots = new Regex(@"(?<=sitemap: )(https://|http://)(\w|\W)+\.xml", RegexOptions.IgnoreCase);
            try
            {
                if (parserClass.GetResponse(URL) != -1)
                {
                    string document = await parserClass.DownloadDocumentAsync(URL);
                    Match pathToSitemap = regexSearchSitemapInRobots.Match(document);
                    foundPath = pathToSitemap.Groups[0].ToString();
                    return foundPath;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}