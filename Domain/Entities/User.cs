using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class User : EntityBase
{
    
    public string? Names { get; set; }
    public string? Lastnames { get; set; }
    public required string Email { get; set; }
    public string? EmailConfirmationToken { get; set; }
    public DateTime? EmailConfirmedAt { get; set; }
    public DateTime? EmailConfirmationTokenExpiresAt { get; set; }
    public DateTime? EmailConfirmationSentAt { get; set; }
    public long? AccountNumber { get; set; }
    public string? Password { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenSentAt { get; set; }
    public DateTime? PasswordResetTokenExpiresAt { get; set; }
    
    public Role Role { get; set; }
}
