# Junior League Cup

Zenit Event tarafından geliştirilen, 7-8 yaş futbol takımları için bölgesel seçmeler ve Antalya final organizasyonu platformu.

**GitHub:** https://github.com/firatug/Junior-League-Cup

## Çözüm Yapısı

| Proje | Açıklama |
|-------|----------|
| **Junior League Cup Web Site** | ASP.NET Core MVC web sitesi (`src/JuniorLeagueCup.WebSite`) |

Gelecekte eklenecek: SQL veritabanı, admin paneli ve dinamik içerik yönetimi.

## Gereksinimler

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Çalıştırma

```bash
cd src/JuniorLeagueCup.WebSite
dotnet run
```

## Sayfa URL'leri

| URL | Sayfa |
|-----|-------|
| `/` | Ana Sayfa |
| `/jlc-nedir` | JLC Nedir? |
| `/bolgesel-secmeler` | Bölgesel Seçmeler |
| `/antalya-finali` | Antalya Finali |
| `/takim-basvurusu` | Takım Başvurusu |
| `/veli-oyuncu-deneyimi` | Veli & Oyuncu Deneyimi |
| `/scout-sistemi` | Scout Sistemi |
| `/sponsorluk` | Sponsorluk |
| `/medya` | Medya |
| `/program-fikstur` | Program / Fikstür |
| `/takimlar` | Takımlar |
| `/oteller` | Otel & Konaklama |
| `/hakkimizda` | Hakkımızda |
| `/iletisim` | İletişim |

## Tasarım

Tarkamo futbol turnuvası temasından esinlenilmiş premium spor markası arayüzü: koyu hero alanı, beyaz içerik bölümleri, kırmızı/altın vurgu renkleri ve Apple.com sistem font yığını (SF Pro / `-apple-system`).
