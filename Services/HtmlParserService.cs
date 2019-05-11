using System;
using System.IO;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;

namespace Dfreeze.Services
{
    public class HtmlParserService : IHtmlParserService
    {
        public PageType GetPageType(string html)
        {
            if (String.IsNullOrEmpty(html))
            {
                return PageType.Unknown;
            }
            else
            {
                var isLoginPage = html.Contains("<form method=\"post\" action=\"LoginPage.aspx\" id=\"LoginForm\" autocomplete=\"off\">");
                if (isLoginPage)
                {
                    return PageType.Login;
                }

                var isInvalidRoomIdPage = html.Contains("The specified room did not exist");
                if (isInvalidRoomIdPage)
                {
                    return PageType.InvalidRoomId;
                }

                var isFreezersInfoPage = html.Contains("dataList_toggle");
                if (isFreezersInfoPage)
                {
                    return PageType.FreezersInfo;
                }

                return PageType.Unknown;
            }
        }

        public IEnumerable<FreezerModel> Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var result = DefaultState.Freezers.Values
                .Select(x => x.Clone(HtmlParserService.IsEnabled(x, doc)));
            return result;
        }

        private static bool IsEnabled(FreezerModel freezer, HtmlDocument doc)
        {
            try
            {
                var toggleId = $"dataList_toggle_{freezer.ToggleCommandId}";
                var xpath = $"(//a[@id='{toggleId}']/span)[1]";
                var result = doc.DocumentNode.SelectSingleNode(xpath);
                return result?.InnerHtml == "1";
            }
            catch
            {
                return false;
            }
        }
    }

}