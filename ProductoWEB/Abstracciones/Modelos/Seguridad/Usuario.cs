using System.ComponentModel.DataAnnotations;

public class UsuarioBase
{
    [Required]
    public string NombreUsuario { get; set; } 

    public string? PasswordHash { get; set; }

    [Required]
    [EmailAddress]
    public string CorreoElectronico { get; set; } 
}

public class Usuario : UsuarioBase
{
    [Required]
    public string Password { get; set; } 

    [Required]
    public string ConfirmarPassword { get; set; } 
}
