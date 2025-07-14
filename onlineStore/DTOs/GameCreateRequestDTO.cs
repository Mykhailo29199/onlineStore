namespace Store.WebAPI.DTOs
{
    public class GameCreateRequestDTO
    {
        // Вкладений об'єкт з інформацією про гру
        public BaseGameDTO Game { get; set; } = null!;

        // Список жанрів (ID-шники), з якими пов'язується гра
        public List<Guid> Genres { get; set; } = new();

        // Список платформ (ID-шники), на яких буде доступна гра
        public List<Guid> Platforms { get; set; } = new();
    }

}

