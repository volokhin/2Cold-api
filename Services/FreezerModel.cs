using System;
using Newtonsoft.Json;

namespace Dfreeze.Services
{
    public class FreezerModel
    {
        [JsonIgnore]
        public int ToggleCommandId { get; private set; }

        [JsonIgnore]
        public FreezerIdentifier UniqueId { get; private set; }

        public int Id { get; private set; }
        public int Floor { get; private set; }
        public string Place { get; private set; }
        public string Name { get; private set; }
        public bool IsEnabled { get; set; }
        public bool IsDirty { get; set; }

        private FreezerModel()
        {

        }

        public FreezerModel(int id, int floor, string place, string name, int toggleCommandId)
        {
            UniqueId = new FreezerIdentifier(this);
            Id = id;
            Floor = floor;
            Place = place;
            Name = name;
            ToggleCommandId = toggleCommandId;
            IsEnabled = false;
            IsDirty = false;
        }

        public FreezerModel Clone()
        {
            return new FreezerModel()
            {
                Id = this.Id,
                Place = this.Place,
                Name = this.Name,
                ToggleCommandId = this.ToggleCommandId,
                IsEnabled = this.IsEnabled,
                Floor = this.Floor,
                IsDirty = this.IsDirty,
                UniqueId = new FreezerIdentifier(this)
            };
        }

        public FreezerModel Clone(bool IsEnabledOverride)
        {
            var clone = Clone();
            clone.IsEnabled = IsEnabledOverride;
            return clone;
        }
    }
}