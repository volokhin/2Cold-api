using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfreeze.Services
{
    public interface IFreezerTasksProcessor
    {
        void Enqueue(FreezerTask task);
        IEnumerable<FreezerTask> GetTasks();
        Task<IEnumerable<FreezerModel>> ProcessNextTaskAsync();
    }
}
