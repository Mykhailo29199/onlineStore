using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Entities
{
    public class GameGenreEntity
    {
        [Required] 
        public Guid GameId { get; set; }
        public GameEntity Game { get; set; } = null!;

        [Required]
        public Guid GenreId { get; set; }

        public GenreEntity Genre { get; set; } = null!;
    }
}
