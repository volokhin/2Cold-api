using System;
using System.Collections.Generic;

namespace Dfreeze.Services
{
    public class FreezerTask
    {
        public FreezerIdentifier FreezerUniqueId { get; private set; }
        public bool IsEnabled { get; set; }

        public FreezerTask(int id, int floor, bool isEnabled)
        {
            FreezerUniqueId = new FreezerIdentifier(id);
            IsEnabled = isEnabled;
        }
    }
}