using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Models
{
    public class BaseGameRequiredModel
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        // Унікальний ключ гри (може використовуватись як slug або url)
        public string Key { get; set; } = string.Empty;

        // Опис гри, не обов'язковий
        public string? Description { get; set; }
    }
}
