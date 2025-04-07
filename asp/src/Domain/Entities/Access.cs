using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Domain.Entities;

[Table("Accesses")]
public class Access : Entity
{
    public Guid Id { get; private set; }
    public IPAddress Ip { get; private set; }
    public string? Country { get; private set; }
    public string? Region { get; private set; }
    public string? City { get; private set; }
    public string Browser { get; private set; }
    public string OS { get; private set; }
    public string? Origin { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public Guid LinkId { get; set; }
    public Link? Link { get; set; }

    protected Access() {}
    public Access(
        IPAddress ip,
        string browser,
        string os,
        string? country,
        string? region,
        string? city, 
        string? origin,
        Guid linkId
    )
    {
        Ip = ip;
        Country = country;
        Region = region;
        City = city;
        Browser = browser;
        OS = os;
        Origin = origin;
        LinkId = linkId;
    }
}