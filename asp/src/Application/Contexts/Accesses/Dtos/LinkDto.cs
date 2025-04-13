namespace Application.Contexts.Accesses.Dtos;

public class AccessDto
{
    public Guid Id { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string Browser { get; set; }
    public string OS { get; set; }
    public string? Origin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid LinkId { get; set; }
    public AccessDto() {}
}