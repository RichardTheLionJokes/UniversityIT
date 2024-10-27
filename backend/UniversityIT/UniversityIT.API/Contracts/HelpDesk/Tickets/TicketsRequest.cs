using System.ComponentModel.DataAnnotations;

namespace UniversityIT.API.Contracts.HelpDesk.Tickets
{
    public record TicketsRequest(
        [Required] string Name,
        string Description,
        string Place,
        [Required] bool IsCompleted);
}