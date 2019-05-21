using System;
using System.Collections.Generic;

namespace Dfreeze.Services
{
    public class NameGeneratorService : INameGeneratorService
    {
        private readonly List<string> _names = new List<string>() 
        {
            "Делаем даже миноры",
            "Давай подвинем",
            "Может зафичекатим",
            "Надо чтобы влезло",
            "Завтра выйдем в ноль",
            "Надо быстрее доставлять",
            "Главное не сломать",
            "Прыгаем выше головы",
            "Вроде успеваем",
        };

        public string GetName()
        {
            var currentDate = DateTime.UtcNow;
            int hash = currentDate.Year + currentDate.Month + currentDate.Day;
            int index = hash % _names.Count;
            return _names[index];
        }
    }
}