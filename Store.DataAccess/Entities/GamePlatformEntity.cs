using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Entities
{
    public class GamePlatformEntity
    {
        [Required]
        public Guid GameId { get; set; }
        public GameEntity Game { get; set; } = null!;

        [Required] 
        public Guid PlatformId { get; set; }
        public PlatformEntity Platform { get; set; } = null!;
    }
}
