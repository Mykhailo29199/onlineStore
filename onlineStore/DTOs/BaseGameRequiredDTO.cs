using System.ComponentModel.DataAnnotations;

namespace Store.WebAPI.DTOs
{
    public class BaseGameRequiredDTO
    {
        // Назва гри
        [Required] 
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        // Унікальний ключ гри (може використовуватись як slug або url)
        public string Key { get; set; } = string.Empty;

        // Опис гри, не обов'язковий
        public string? Description { get; set; }
    }
}
