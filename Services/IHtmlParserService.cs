using System;
using System.Collections.Generic;
using System.IO;

namespace Dfreeze.Services
{
    public interface IHtmlParserService
    {
        bool IsLoginPage(string html);
        IEnumerable<FreezerModel> Parse(string html);
    }
}