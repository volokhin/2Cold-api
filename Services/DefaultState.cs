using System;
using System.Collections.Generic;
using System.Linq;

namespace Dfreeze.Services
{
    public static class DefaultState
    {
        private static readonly IList<FreezerModel> Cache = new List<FreezerModel>
        {
            // 8 этаж
            new FreezerModel(id: 10, floor: 8, name: "Даша", place: "iOS", toggleCommandId: 48),
            new FreezerModel(id: 11, floor: 8, name: "Маша", place: "iOS", toggleCommandId: 54),
            new FreezerModel(id: 12, floor: 8, name: "Вадим", place: "iOS", toggleCommandId: 60),
            new FreezerModel(id: 7, floor: 8, name: "Данил", place: "Core", toggleCommandId: 30),
            new FreezerModel(id: 5, floor: 8, name: "Оксана", place: "HR", toggleCommandId: 18),
            new FreezerModel(id: 4, floor: 8, name: "Светлана", place: "Кухня", toggleCommandId: 12),
            new FreezerModel(id: 2, floor: 8, name: "Кирилл", place: "D&R", toggleCommandId: 0),
            new FreezerModel(id: 13, floor: 8, name: "Руслан", place: "Android", toggleCommandId: 66),
            new FreezerModel(id: 14, floor: 8, name: "Сергей", place: "Android", toggleCommandId: 72),
            new FreezerModel(id: 9, floor: 8, name: "Artöm", place: "CoreNavi", toggleCommandId: 42),
            new FreezerModel(id: 8, floor: 8, name: "Юля", place: "Support", toggleCommandId: 36),
            new FreezerModel(id: 6, floor: 8, name: "???", place: "Камчатка", toggleCommandId: 24),

            // 5 этаж
            new FreezerModel(id: 999, floor: 5, name: "???", place: "5 этаж", toggleCommandId: 999)
        };

        public static IDictionary<FreezerIdentifier, FreezerModel> Freezers =
            DefaultState.Cache.ToDictionary(x => new FreezerIdentifier(x));

        public static IDictionary<FreezerIdentifier, FreezerModel> GetCopy()
        {
            var result = DefaultState.Freezers.Values
                .Select(x => x.Clone())
                .ToDictionary(x => new FreezerIdentifier(x));
            return result;
        }
    }
}