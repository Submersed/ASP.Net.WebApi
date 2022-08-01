namespace si.ineor.webapi.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
