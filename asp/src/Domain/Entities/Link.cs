using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Links")]
public class Link
{
    [Key]
    public Guid Id { get; private set; }
    public string Mask { get; private set; }
    public Uri Destination { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string UserId { get; set; } // need to do this because identity
    public User? User { get; set; }
}