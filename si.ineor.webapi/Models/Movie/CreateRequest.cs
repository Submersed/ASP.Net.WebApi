using System.ComponentModel.DataAnnotations;

namespace si.ineor.webapi.Models.Movie
{
    public class CreateRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        //validacija...
        public DateTime ReleaseDate { get; set; }
    }
}
