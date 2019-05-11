using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Dfreeze.Services
{
    public interface IFreezeService
    {
        Task<IEnumerable<FreezerModel>> GetFreezersAsync();
        Task<IEnumerable<FreezerModel>> SetEnabledAsync(FreezerIdentifier id, bool enabled);
    }
}