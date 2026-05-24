namespace JuniorLeagueCup.WebSite.Options;

public class EmailSettings
{
    public const string SectionName = "Email";

    public string SmtpHost { get; set; } = "mail.ataventures.tr";
    public int SmtpPort { get; set; } = 587;
    public bool UseSsl { get; set; } = true;
    public string Username { get; set; } = "system@ataventures.tr";
    public string Password { get; set; } = string.Empty;
    public string FromAddress { get; set; } = "system@ataventures.tr";
    public string FromName { get; set; } = "Junior League Cup";
    public string ToAddress { get; set; } = "firatug@gmail.com";
}
