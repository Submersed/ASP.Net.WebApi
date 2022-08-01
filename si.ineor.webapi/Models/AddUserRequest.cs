using System.ComponentModel.DataAnnotations;

namespace si.ineor.webapi.Models
{
    public class AddUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string Email { get; set; }
    }
}
