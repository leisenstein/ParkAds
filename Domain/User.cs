using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
