namespace JuniorLeagueCup.WebSite.Services;

public class NewsImageService(IWebHostEnvironment env)
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp"
    };

    private const long MaxBytes = 5 * 1024 * 1024;

    public async Task<(bool Success, string? Path, string? Error)> SaveAsync(IFormFile file)
    {
        if (file.Length == 0)
            return (false, null, "Görsel dosyası seçilmedi.");

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            return (false, null, "Sadece JPG, PNG ve WEBP formatları desteklenir.");

        if (file.Length > MaxBytes)
            return (false, null, "Görsel en fazla 5 MB olabilir.");

        var uploadsDir = Path.Combine(env.WebRootPath, "uploads", "news");
        Directory.CreateDirectory(uploadsDir);

        var fileName = $"{Guid.NewGuid():N}{ext.ToLowerInvariant()}";
        var fullPath = Path.Combine(uploadsDir, fileName);

        await using (var stream = new FileStream(fullPath, FileMode.Create))
            await file.CopyToAsync(stream);

        return (true, $"/uploads/news/{fileName}", null);
    }

    public void DeleteIfUploaded(string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl) ||
            !imageUrl.StartsWith("/uploads/news/", StringComparison.OrdinalIgnoreCase))
            return;

        var relative = imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        var fullPath = Path.Combine(env.WebRootPath, relative);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
