using Store.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Models
{
    public class GameModel
    {
        // Вкладений об'єкт з інформацією про гру
        public BaseGameModel Game { get; set; } = null!;
        // Список жанрів (ID-шники), з якими пов'язується гра
        public List<Guid> Genres { get; set; } = new();

        // Список платформ (ID-шники), на яких буде доступна гра
        public List<Guid> Platforms { get; set; } = new();
    }
}
