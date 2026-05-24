namespace JuniorLeagueCup.WebSite.Models;

public static class FormThankYouTypes
{
    public const string TakimBasvurusu = "takim-basvurusu";
    public const string Iletisim = "iletisim";
    public const string Sponsorluk = "sponsorluk";

    public static readonly HashSet<string> All = [TakimBasvurusu, Iletisim, Sponsorluk];

    public static (string Title, string Message) GetContent(string formType) => formType switch
    {
        TakimBasvurusu => (
            "Başvurunuz Alındı",
            "Takım başvurunuz başarıyla iletildi. En kısa sürede sizinle iletişime geçeceğiz."),
        Iletisim => (
            "Mesajınız Alındı",
            "İletişim formunuz başarıyla iletildi. En kısa sürede size dönüş yapacağız."),
        Sponsorluk => (
            "Talebiniz Alındı",
            "Sponsorluk talebiniz başarıyla iletildi. Ekibimiz sizinle iletişime geçecektir."),
        _ => ("Teşekkürler", "Formunuz başarıyla iletildi.")
    };
}
