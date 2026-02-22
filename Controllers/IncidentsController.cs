using IncidentAPI_Abdouli.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace IncidentAPI_Abdouli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private static readonly List<Incident> _incidents = new();
        private static int _nextId = 1;
        private static readonly string[] AllowedSeverities = {"LOW", "MEDIUM", "HIGH","CRITICAL"};
        private static readonly string[] AllowedStatuses = {"OPEN", "IN_PROGRESS", "RESOLVED"};

        [HttpPost("create-incident")]
        public IActionResult CreateIncident([FromBody] Incident incident)
        {
            if (!AllowedSeverities.Contains(incident.Severity))
                return BadRequest("Gravité (Severity) donné n'est pas autorisé");

            incident.Id = _nextId++;
            incident.Status = "OPEN";
            incident.CreatedAt = DateTime.Now;

            _incidents.Add(incident);
            return Ok(incident);
        }
        [HttpGet("get-all")]
        public IActionResult GetAllIncidents()
        {
            return Ok(_incidents);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetIncidentById(int id)
        {
            if (id >= _nextId)
                return BadRequest("L\'id envoyé n'existe pas!");

            var incident = _incidents.First(i => i.Id == id);

            if (incident == null)
                return NotFound();

            return Ok(incident);
        }

        [HttpPut("update-status/{id}")]
        public IActionResult UpdateIncidentStatus(int id, [FromBody] string status)
        {
            var incident = _incidents.First(i => i.Id == id);
            if (incident == null)
                return NotFound();
            if (!AllowedStatuses.Contains(status))
                return BadRequest("Statut donné n'est pas autorisé");
            incident.Status = status;

            return Ok(incident);
        }

        [HttpDelete("delete-incident/{id}")]
        public IActionResult DeleteIncident(int id)
        {
            var incident = _incidents.First(i => i.Id == id);
            if (incident == null)
                return NotFound();

            if (incident.Severity == "CRITICAL" && incident.Status == "OPEN")
                return BadRequest("Impossible de supprimer un incident de gravité = 'CRITICAL' et statut = 'OPEN'");

            _incidents.Remove(incident);
            return NoContent();
        }

        [HttpGet("filterbystatus/{status}")]
        public IActionResult FilterByStatus( string status)
        {
            List<Incident> _incidents_filtered = new();
            for (int i =0; i < _incidents.Count; i++)
            {
                if (_incidents[i].Status.Contains(status))
                    _incidents_filtered.Add(_incidents[i]);
            }
            return Ok(_incidents_filtered);
        }

        [HttpGet("filterbyseverity{severity}")]
        public IActionResult FilterBySeverity(string severity)
        {
            List<Incident> _incidents_filtered = new();
            for(int i = 0; i < _incidents.Count; i++)
            {
                if (_incidents[i].Severity.Contains(severity))
                    _incidents_filtered.Add(_incidents[i]);
            }
            return Ok(_incidents_filtered);
        }
    }
}
