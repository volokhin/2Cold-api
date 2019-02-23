using System;
using System.Collections.Generic;
using System.IO;

namespace Dfreeze.Services
{
    public interface IHtmlParserService
    {
        PageType GetPageType(string html);
        IEnumerable<FreezerModel> Parse(string html);
    }
}