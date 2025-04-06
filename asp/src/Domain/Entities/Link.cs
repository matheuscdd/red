using System.ComponentModel.DataAnnotations.Schema;
using Domain.Exceptions;

namespace Domain.Entities;

[Table("Links")]
public class Link : Entity
{
    public string Mask { get; private set; }
    public Uri Destination { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string UserId { get; set; } // need to do this because identity
    public User? User { get; set; }

    protected Link() {}
    public Link(
        string? mask,
        Uri? destination,
        string? userId
    )
    {
        validateMask(mask);
        validateDestination(destination);
        validateUserId(userId);

        SetMask(mask);
        SetDestination(destination);
        SetUserId(userId);
    }

    public void SetMask(string? mask)
    {
        validateMask(mask);
        Mask = mask!;
    }

    public void SetDestination(Uri? destination)
    {
        validateDestination(destination);
        Destination = destination!;
    }

    public void SetUserId(string? userId)
    {
        validateUserId(userId);
        UserId = userId!;
    }

    private void validateMask(string? mask)
    {
        const string name = nameof(Mask);
        validateEmpty(mask, name);
        validateLength(mask!, name, 1, 150);
    }

    private void validateUserId(string? userId)
    {
        const string name = nameof(UserId);
        validateEmpty(userId, name);
        validateGuid(userId!, name);
    }

    private void validateDestination(Uri? destination)
    {
        const string name = nameof(Destination);
        validateEmpty(destination, name);
        validateLength(destination!.ToString(), name, 1, 2048);
    }

    private void validateEmpty(Uri? value, string name)
    {
        if (value == null) 
        {
            throw new ValidationCustomException($"{name} cannot be empty");
        }
    }

    protected void validateGuid(string guid, string name)
    {
        if (!Guid.TryParse(guid, out Guid _))
        {
             throw new ValidationCustomException($"{name} is invalid Guid");
        }
    }
}