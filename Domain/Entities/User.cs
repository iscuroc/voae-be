using Domain.Base;
using Domain.Enums;

namespace Domain.Entities;

public class User : EntityBase
{
    public required string Names { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public int AccountNumber { get; set; }
    public required string Password { get; set; }
    public Role Role { get; set; }
}