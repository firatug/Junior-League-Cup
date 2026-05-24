using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JuniorLeagueCup.WebSite.Models;

public class NewsEditViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Başlık gerekli.")]
    [StringLength(200)]
    [Display(Name = "Başlık")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Özet gerekli.")]
    [StringLength(500)]
    [Display(Name = "Özet")]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "İçerik gerekli.")]
    [Display(Name = "İçerik")]
    public string Content { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Başlık (EN)")]
    public string? TitleEn { get; set; }

    [StringLength(500)]
    [Display(Name = "Özet (EN)")]
    public string? SummaryEn { get; set; }

    [Display(Name = "İçerik (EN)")]
    public string? ContentEn { get; set; }

    [StringLength(200)]
    [Display(Name = "Başlık (ES)")]
    public string? TitleEs { get; set; }

    [StringLength(500)]
    [Display(Name = "Özet (ES)")]
    public string? SummaryEs { get; set; }

    [Display(Name = "İçerik (ES)")]
    public string? ContentEs { get; set; }

    [StringLength(200)]
    [Display(Name = "Başlık (DE)")]
    public string? TitleDe { get; set; }

    [StringLength(500)]
    [Display(Name = "Özet (DE)")]
    public string? SummaryDe { get; set; }

    [Display(Name = "İçerik (DE)")]
    public string? ContentDe { get; set; }

    [Required]
    [Display(Name = "Kategori")]
    public string Category { get; set; } = "Haber";

    public string? ImageUrl { get; set; }

    [Display(Name = "Görsel")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Yayında")]
    public bool IsPublished { get; set; } = true;

    [Display(Name = "Ana sayfada göster")]
    public bool ShowOnHomepage { get; set; } = true;

    [Display(Name = "Yayın tarihi")]
    public DateTime PublishedAt { get; set; } = DateTime.Now;

    public static readonly string[] Categories =
    [
        "Haber", "Video", "Röportaj", "Foto Galeri", "Duyuru", "Sponsor İçeriği"
    ];
}
