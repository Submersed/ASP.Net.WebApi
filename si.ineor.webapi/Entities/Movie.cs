using si.ineor.webapi.Models.Movie;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace si.ineor.webapi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        //validacija...
        public DateTime ReleaseDate { get; set; }

        public Movie(CreateRequest movie)
        {
            Title = movie.Title;
            Description = movie.Description;
            ReleaseDate = movie.ReleaseDate;
        }
        public Movie()
        {

        }
    }
}
