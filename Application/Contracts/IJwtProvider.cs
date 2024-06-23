namespace Application.Contracts;

public interface IJwtProvider
{
    public string GenerateToken(int userId, string email);
    
}