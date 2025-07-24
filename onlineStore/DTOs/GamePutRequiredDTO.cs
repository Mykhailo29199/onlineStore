using System.ComponentModel.DataAnnotations;

namespace Store.WebAPI.DTOs
{
    public class GamePutRequiredDTO
    {
        // Вкладений об'єкт з інформацією про гру
        public required BaseGameRequiredDTO Game { get; set; }

        // Список жанрів (ID-шники), з якими пов'язується гра

        [Required]
        public required List<Guid> Genres { get; set; }

        // Список платформ (ID-шники), на яких буде доступна гра

        [Required]
        public required List<Guid> Platforms { get; set; }
    }
}
