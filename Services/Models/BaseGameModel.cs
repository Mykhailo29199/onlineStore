using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Models
{
    public class BaseGameModel
    {
        // Цей клас описує саму гру, як вона надсилається в тілі запиту

            // Назва гри
            public string Name { get; set; } = string.Empty;

            // Унікальний ключ гри (може використовуватись як slug або url)
            public string Key { get; set; } = string.Empty;

            // Опис гри, не обов'язковий
            public string? Description { get; set; }
    }
}
