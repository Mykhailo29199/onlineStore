using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Entities
{
    public class PlatformEntity
    {
        [Required]
        [Column("PlatformId")]
        public Guid Id { get; set; }

        [Required]
        [Column("PlatformName")]
        public required string Name { get; set; } = string.Empty;
    }
}
