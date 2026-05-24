using System.ComponentModel.DataAnnotations;

namespace JuniorLeagueCup.WebSite.Models;

public class TakimBasvurusuFormModel
{
    [Required(ErrorMessage = "Kulüp adı gerekli.")]
    [Display(Name = "Kulüp / Futbol Okulu Adı")]
    public string ClubName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yetkili adı gerekli.")]
    [Display(Name = "Yetkili Kişi Adı")]
    public string ContactName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon gerekli.")]
    [Display(Name = "Telefon")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta gerekli.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şehir seçin.")]
    [Display(Name = "Şehir")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yaş grubu seçin.")]
    [Display(Name = "Takım Yaş Grubu")]
    public string AgeGroup { get; set; } = string.Empty;

    [Display(Name = "Oyuncu Sayısı")]
    public int? PlayerCount { get; set; }

    [Display(Name = "Daha Önce Katıldığı Organizasyonlar")]
    public string? PastEvents { get; set; }

    [Display(Name = "Notlar")]
    public string? Notes { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "KVKK onayı gerekli.")]
    public bool Kvkk { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Çerez politikası onayı gerekli.")]
    public bool Cerez { get; set; }
}

public class IletisimFormModel
{
    [Required(ErrorMessage = "Ad soyad gerekli.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon gerekli.")]
    [Display(Name = "Telefon")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta gerekli.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Konu seçin.")]
    [Display(Name = "Konu")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mesaj gerekli.")]
    [Display(Name = "Mesaj")]
    public string Message { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "KVKK onayı gerekli.")]
    public bool Kvkk { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Çerez politikası onayı gerekli.")]
    public bool Cerez { get; set; }
}

public class SponsorlukFormModel
{
    [Required(ErrorMessage = "Firma adı gerekli.")]
    [Display(Name = "Firma Adı")]
    public string Company { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yetkili adı gerekli.")]
    [Display(Name = "Yetkili Kişi")]
    public string Contact { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon gerekli.")]
    [Display(Name = "Telefon")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta gerekli.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "İlgilenilen Sponsorluk Türü")]
    public string? SponsorType { get; set; }

    [Display(Name = "Mesaj")]
    public string? Message { get; set; }
}
