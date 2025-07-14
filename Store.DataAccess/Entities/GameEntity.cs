using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Entities
{
    public class GameEntity
    {
        [Key]
        [Required]
        [Column("GameId")]
        public Guid Id { get; set; }

        [Required]
        [Column("GameName")]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]           
        [MaxLength(222)]     
        public required string GameKey { get; set; }

        [MaxLength(10000)]
        public string? Description { get; set; }

        public ICollection<GameGenreEntity> GameGenres { get; set; } = new List<GameGenreEntity>();
        public ICollection<GamePlatformEntity> GamePlatforms { get; set; } = new List<GamePlatformEntity>();

    }
}
