namespace IncidentAPI_Abdouli.Models
{
    public class Incident
    {
        public int Id { get; set; } // int par défaut est 0
        public String Title { get; set; } = null!; // Ou bien string.Empty
        public String Description { get; set; } = string.Empty; // ou bien null!
        public String Severity { get; set; } = string.Empty;
        public String Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } // DateTime par défaut est 01/01/0001 00:00:00
    }   
}
    