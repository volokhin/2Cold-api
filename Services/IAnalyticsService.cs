using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfreeze.Services
{
    public interface IAnalyticsService
    {
        void SendEvent(string name);
        void SendEvent(string name, IDictionary<string, object> data);
    }
}