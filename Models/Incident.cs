using System.ComponentModel.DataAnnotations;

namespace IncidentAPI_Abdouli.Models
{
    public class Incident
    {
        public int Id { get; set; } // int par défaut est 0
        [Required]
        [MaxLength(30)]
        public String Title { get; set; } = null!; // Ou bien string.Empty
        [Required]
        [MaxLength(200)]
        public String Description { get; set; } = string.Empty; // ou bien null!
        [Required]
        public String Severity { get; set; } = string.Empty;
        [Required]
        public String Status { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; } // DateTime par défaut est 01/01/0001 00:00:00
    }   
}
    