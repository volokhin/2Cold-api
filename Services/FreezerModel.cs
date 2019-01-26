using System;
using Newtonsoft.Json;

namespace Dfreeze.Services
{
    public class FreezerModel
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int ToggleCommandId { get; set; }

        public bool IsEnabled { get; set; }

        public string Place { get; set; }

        public string Name { get; set; }
    }
}