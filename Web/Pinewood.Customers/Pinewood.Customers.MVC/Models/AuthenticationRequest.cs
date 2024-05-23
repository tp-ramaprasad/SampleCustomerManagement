namespace Pinewood.Customers.MVC.Models;

using System.ComponentModel.DataAnnotations;

public class AuthenticationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }

    public ErrorViewModel? ErrorVMModel { get; set; }
}