namespace Livraria.Infra.Auth;

using System.ComponentModel.DataAnnotations;

public class AuthenticateModel
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}