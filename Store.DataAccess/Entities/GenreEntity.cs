using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Entities
{
    public class GenreEntity
    {
        [Key]
        [Required]
        [Column("GenreId")]
        public Guid Id { get; set; }

        [Required]
        [Column("GenreName")]
        public required string Name { get; set; } = string.Empty;

        public Guid? ParentGenreId { get; set; }
        public GenreEntity? ParentGenre { get; set; }
        public ICollection<GenreEntity> SubGenres { get; set; } = new List<GenreEntity>();
    }
}
