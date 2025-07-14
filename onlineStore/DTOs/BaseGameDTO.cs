namespace Store.WebAPI.DTOs
{
    // Цей клас описує саму гру, як вона надсилається в тілі запиту
    public class BaseGameDTO
    {
        // Назва гри
        public string Name { get; set; } = string.Empty;

        // Унікальний ключ гри (може використовуватись як slug або url)
        public string Key { get; set; } = string.Empty;

        // Опис гри, не обов'язковий
        public string? Description { get; set; }
    }
}
