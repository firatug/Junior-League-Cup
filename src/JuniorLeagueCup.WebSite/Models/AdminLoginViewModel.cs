using System.ComponentModel.DataAnnotations;

namespace JuniorLeagueCup.WebSite.Models;

public class AdminLoginViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı gerekli.")]
    [Display(Name = "Kullanıcı adı")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre gerekli.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;
}
