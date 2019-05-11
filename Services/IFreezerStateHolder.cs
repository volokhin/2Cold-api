using System;
using System.Collections.Generic;

namespace Dfreeze.Services
{
    public interface IFreezerStateHolder
    {
       IEnumerable<FreezerModel> GetFreezers();
       void UpdateState(IEnumerable<FreezerModel> freezers);
    }
}