using System.ComponentModel.DataAnnotations;

namespace SecureNotesApi.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}