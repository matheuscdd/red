namespace Application.Contexts.Links.Dtos;

public class LinkDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Mask { get; set; }
    public Uri Destination { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserId { get; set; }
    public LinkDto() {}
    public LinkDto(
        Guid id,
        string userName,
        string mask,
        Uri destination,
        DateTime createdAt,
        string userId
    ) 
    {
        Id = id;
        UserName = userName;
        Mask = mask;
        Destination = destination;
        CreatedAt = createdAt;
        UserId = userId;
    }
}