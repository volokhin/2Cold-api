using System;
using System.IO;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;

namespace Dfreeze.Services
{
    public class HtmlParserService : IHtmlParserService
    {
        public bool IsLoginPage(string html)
        {
            return html?.Contains("<form method=\"post\" action=\"LoginPage.aspx\" id=\"LoginForm\" autocomplete=\"off\">") ?? false;
        }

        public IEnumerable<FreezerModel> Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var result = from info in Constants.FreezerInfos
                         select new FreezerModel()
                         {
                             Id = info.Id,
                             Place = info.Place,
                             Name = info.Name,
                             ToggleCommandId = info.ToggleCommandId,
                             IsEnabled = HtmlParserService.IsEnabled(info, doc)
                         };

            return result;
        }

        private static bool IsEnabled(FreezerModel freezer, HtmlDocument doc)
        {
            try
            {
                var toggleId = $"dataList_toggle_{freezer.ToggleCommandId}";
                var xpath = $"(//a[@id='{toggleId}']/span)[1]";
                var result = doc.DocumentNode.SelectSingleNode(xpath);
                return result.InnerHtml == "1";
            }
            catch
            {
                return false;
            }
        }
    }

}