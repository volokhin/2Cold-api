using System;
using System.Collections.Generic;

namespace Dfreeze.Services
{
    public static class Models
    {
        public static readonly IList<FreezerModel> FreezerInfos = new List<FreezerModel>
        {
            new FreezerModel
            {
                Id = 10,
                ToggleCommandId = 48,
                Name = "Даша",
                Place = "iOS",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 11,
                ToggleCommandId = 54,
                Name = "Маша",
                Place = "iOS",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 12,
                ToggleCommandId = 60,
                Name = "Вадим",
                Place = "iOS",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 7,
                ToggleCommandId = 30,
                Name = "Данил",
                Place = "Core",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 5,
                ToggleCommandId = 18,
                Name = "Оксана",
                Place = "HR",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 4,
                ToggleCommandId = 12,
                Name = "Светлана",
                Place = "Кухня",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 2,
                ToggleCommandId = 0,
                Name = "Кирилл",
                Place = "D&R",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 13,
                ToggleCommandId = 66,
                Name = "Руслан",
                Place = "Android",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 14,
                ToggleCommandId = 72,
                Name = "Сергей",
                Place = "Android",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 9,
                ToggleCommandId = 42,
                Name = "Артём",
                Place = "CoreNavi",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 8,
                ToggleCommandId = 36,
                Name = "???",
                Place = "Support",
                IsEnabled = false
            },
            new FreezerModel
            {
                Id = 6,
                ToggleCommandId = 24,
                Name = "???",
                Place = "Камчатка",
                IsEnabled = false
            }
        };
    }
}