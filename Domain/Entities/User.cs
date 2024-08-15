using System.Collections;
using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class User : EntityBase
{
    private const string TeacherEmailDomain = "unah.edu.hn";
    
    public string? Names { get; set; }
    public string? Lastnames { get; set; }
    public required string Email { get; set; }
    public string? EmailConfirmationToken { get; set; }
    public DateTime? EmailConfirmedAt { get; set; }
    public DateTime? EmailConfirmationTokenExpiresAt { get; set; }
    public DateTime? EmailConfirmationSentAt { get; set; }
    public long? AccountNumber { get; set; }
    public string? Password { get; set; }
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenSentAt { get; set; }
    public DateTime? ResetPasswordTokenExpiresAt { get; set; }
    public Role Role { get; set; }
    
    public int? CareerId { get; set; }
    public Career? Career { get; set; }

    public ICollection<Activity> RequestedActivities { get; set; } = null!;
    public ICollection<Activity> SupervisedActivities { get; set; } = null!;
    public ICollection<Activity> CoordinatedActivities { get; set; } = null!;
    public ICollection<Activity> ReviewedActivities { get; set; } = null!;
    public ICollection<Organization> Organizations { get; set; } = null!;
    public ICollection<ActivityMember> JoinedActivities { get; set; } = null!;

    public void SetRoleByEmail()
    {
        Role = GetRoleByEmail();
    }
    
    public void ConfirmEmail()
    {
        EmailConfirmedAt = DateTime.UtcNow;
        EmailConfirmationToken = null;
        EmailConfirmationTokenExpiresAt = null;
    }

    public bool IsAccoutNumberValid()
    {
        if (AccountNumber is null) return false;

        var role = GetRoleByEmail();
        var year = DateTime.UtcNow.Year;

        if (role == Role.Student)
        {
            var accountYear = int.Parse(AccountNumber.Value.ToString()[..4]);
            if (accountYear > year) return false;

            return AccountNumber.Value.ToString().Length == 11;
        }

        if (role == Role.Teacher)
            return AccountNumber.Value.ToString().Length == 5;

        return false;
    }

    private Role GetRoleByEmail()
    {
        return Email.EndsWith(TeacherEmailDomain) ? Role.Teacher : Role.Student;
    }

    public void GenerateResetPasswordToken()
    {
        ResetPasswordToken = GenerateToken();
        ResetPasswordTokenExpiresAt = DateTime.UtcNow.AddDays(1);
        ResetPasswordTokenSentAt = DateTime.UtcNow;
    }

    public void GenerateConfirmationToken()
    {
        EmailConfirmationToken = GenerateToken();
        EmailConfirmationSentAt = DateTime.UtcNow;
        EmailConfirmationTokenExpiresAt = DateTime.UtcNow.AddDays(3);
    }

    public void ResetPassword(string hashedPassword)
    {
        Password = hashedPassword;
        ResetPasswordToken = null;
        ResetPasswordTokenExpiresAt = null;
    }
    
    private string GenerateToken() => Guid.NewGuid().ToString();
}
