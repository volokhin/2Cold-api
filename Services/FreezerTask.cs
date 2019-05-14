using System;
using System.Collections.Generic;

namespace Dfreeze.Services
{
    public class FreezerTask
    {
        public FreezerIdentifier FreezerUniqueId { get; private set; }
        public bool IsEnabled { get; set; }

        public FreezerTask(int floor, int id, bool isEnabled)
        {
            FreezerUniqueId = new FreezerIdentifier(floor, id);
            IsEnabled = isEnabled;
        }
    }
}