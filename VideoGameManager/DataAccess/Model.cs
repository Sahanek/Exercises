using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameManager.DataAccess
{
    public class GameGenre
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

        public List<Game> Games { get; set; }


    }

    public class Game
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; } = string.Empty;

        public GameGenre Genre { get; set; }

        public int PersonalRating { get; set; }

    }
}
