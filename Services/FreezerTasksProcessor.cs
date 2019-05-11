using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfreeze.Services
{
    public class FreezerTasksProcessor : IFreezerTasksProcessor
    {
        private readonly IFreezeService _freezeService;
        private Queue<FreezerTask> _todo = new Queue<FreezerTask>();
        private IList<FreezerTask> _doing = new List<FreezerTask>();

        public FreezerTasksProcessor(IFreezeService freezeService)
        {
            _freezeService = freezeService;
        }

        public IEnumerable<FreezerTask> GetTasks()
        {
            lock (this)
            {
                return _doing.Concat(_todo);
            }
        }

        public void Enqueue(FreezerTask task)
        {
            if (!DefaultState.Freezers.ContainsKey(task.FreezerUniqueId))
            {
                throw new ArgumentException($"No freezer with id {task.FreezerUniqueId} found.");
            }
            lock (this)
            {
                var existing = _todo.FirstOrDefault(x => x.FreezerUniqueId == task.FreezerUniqueId);
                if (existing != null)
                {
                    existing.IsEnabled = task.IsEnabled;
                }
                else
                {
                    _todo.Enqueue(task);
                }
            }
        }

        public async Task<IEnumerable<FreezerModel>> ProcessNextTaskAsync()
        {
            FreezerTask task = null;
            IEnumerable<FreezerModel> result = null;
            lock (this)
            {
                if (_todo.Count > 0)
                {
                    task = _todo.Dequeue();
                    _doing.Add(task);
                }
            }
            if (task != null)
            {
                try
                {
                    result = await _freezeService.SetEnabledAsync(task.FreezerUniqueId, task.IsEnabled);
                }
                finally
                {
                    lock (this)
                    {
                        _doing.Remove(task);
                    }
                }
            }
            return result;
        }
    }
}
