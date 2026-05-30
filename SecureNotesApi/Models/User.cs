using System.ComponentModel.DataAnnotations;

namespace SecureNotesApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<Note>? Notes { get; set; }
    }
}