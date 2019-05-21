using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dfreeze.Services
{
    public class FreezerStateHolder : IFreezerStateHolder
    {
        private readonly FreezerIdentifier _kamchatkaId = new FreezerIdentifier(floor: 8, id: 6);
        private readonly IDictionary<FreezerIdentifier, FreezerModel> _state;
        private readonly IFreezerTasksProcessor _processor;
        private readonly INameGeneratorService _nameGenerator;

        

        public FreezerStateHolder(IFreezerTasksProcessor processor, INameGeneratorService nameGenerator)
        {
            _processor = processor;
            _nameGenerator = nameGenerator;
            lock (this)
            {
                _state = DefaultState.GetCopy();
            }
        }

        public void UpdateState(IEnumerable<FreezerModel> freezers)
        {
            lock (this)
            {
                foreach (var item in freezers)
                {
                    FreezerModel freezer;
                    if (_state.TryGetValue(item.UniqueId, out freezer))
                    {
                        freezer.IsEnabled = item.IsEnabled;
                    }
                }
            }
        }

        public IEnumerable<FreezerModel> GetFreezers()
        {
            lock (this)
            {
                var state = GetStateCopy();
                var kamchatka = state[_kamchatkaId];
                kamchatka.Name = _nameGenerator.GetName();
                var tasks = _processor.GetTasks();
                foreach (var task in tasks)
                {
                    FreezerModel freezer;
                    if (state.TryGetValue(task.FreezerUniqueId, out freezer))
                    {
                        freezer.IsEnabled = task.IsEnabled;
                        freezer.IsDirty = true;
                    }
                }
                return state.Values
                    .OrderBy(x => x.Floor)
                    .ThenBy(x => x.Place)
                    .ThenBy(x => x.Id);
            }
        }

        private IDictionary<FreezerIdentifier, FreezerModel> GetStateCopy()
        {
            var result = _state.Values
                .Select(x => x.Clone())
                .ToDictionary(x => x.UniqueId);
            return result;
        }
    }
}