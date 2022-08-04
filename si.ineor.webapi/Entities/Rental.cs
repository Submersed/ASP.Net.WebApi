using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace si.ineor.webapi.Entities
{
    public class Rental
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
        public DateTime Rented { get; set; }
        public DateTime Returned { get; set; }
    }
}
