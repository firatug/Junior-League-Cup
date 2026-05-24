namespace JuniorLeagueCup.WebSite.Localization;

public static class UILayoutStrings
{
    private static string L(string tr, string en, string es, string de) => LocaleHelper.Pick(tr, en, es, de);

    public static string HtmlLang => LocaleHelper.HtmlLang;
    public static string DefaultMetaDescription => L(
        "Junior League Cup — Türkiye genelinde 7-8 yaş futbol takımları için bölgesel seçmeler ve Antalya final organizasyonu. Road to Antalya.",
        "Junior League Cup — Regional selections and Antalya final tournament for U7-U8 football teams across Turkey. Road to Antalya.",
        "Junior League Cup — Selecciones regionales y torneo final en Antalya para equipos de fútbol Sub-7/Sub-8 en Turquía. Road to Antalya.",
        "Junior League Cup — Regionale Auswahlspiele und Antalya-Finale für U7-U8-Fußballteams in der Türkei. Road to Antalya.");
    public static string DefaultOgDescription => L(
        "Road to Antalya — Geleceğin oyuncuları burada başlar.",
        "Road to Antalya — Where future stars begin.",
        "Road to Antalya — Donde comienzan las estrellas del futuro.",
        "Road to Antalya — Hier beginnen die Stars von morgen.");
    public static string DefaultTitle => L("Ana Sayfa", "Home", "Inicio", "Startseite");
    public static string BrandAria => L(
        "Junior League Cup — Ana Sayfa",
        "Junior League Cup — Home",
        "Junior League Cup — Inicio",
        "Junior League Cup — Startseite");
    public static string MenuToggle => L(
        "Menüyü aç/kapat",
        "Open/close menu",
        "Abrir/cerrar menú",
        "Menü öffnen/schließen");
    public static string NavHome => L("Ana Sayfa", "Home", "Inicio", "Startseite");
    public static string NavJlc => "JLC";
    public static string NavWhatIsJlc => L("JLC Nedir?", "What is JLC?", "¿Qué es JLC?", "Was ist JLC?");
    public static string NavRegional => L("Bölgesel Seçmeler", "Regional Selections", "Selecciones Regionales", "Regionale Auswahlspiele");
    public static string NavAntalyaFinal => L("Antalya Finali", "Antalya Final", "Final de Antalya", "Antalya-Finale");
    public static string NavMedia => L("Medya", "News", "Noticias", "Nachrichten");
    public static string NavAbout => L("Hakkımızda", "About Us", "Sobre Nosotros", "Über uns");
    public static string NavTeamApplication => L("Takım Başvurusu", "Team Application", "Solicitud de Equipo", "Teamanmeldung");
    public static string LiveAria => L(
        "Canlı skor ve fikstür",
        "Live scores and fixtures",
        "Marcadores y calendario en vivo",
        "Live-Ergebnisse und Spielplan");
    public static string FooterTagline => L(
        "Türkiye'nin genç futbol yetenekleri için bölgesel seçmelerden Antalya finaline uzanan profesyonel organizasyon markası.",
        "A professional youth football organization brand from regional selections to the Antalya final.",
        "Una marca profesional de organización de fútbol juvenil, desde las selecciones regionales hasta la final en Antalya.",
        "Eine professionelle Jugendfußball-Organisationsmarke von den regionalen Auswahlspielen bis zum Antalya-Finale.");
    public static string FooterOrganization => L("Organizasyon", "Organization", "Organización", "Organisation");
    public static string FooterParticipation => L("Katılım", "Participation", "Participación", "Teilnahme");
    public static string FooterContact => L("İletişim", "Contact", "Contacto", "Kontakt");
    public static string FooterProgram => L("Program / Fikstür", "Schedule / Fixtures", "Programa / Calendario", "Programm / Spielplan");
    public static string FooterTeams => L("Takımlar", "Teams", "Equipos", "Teams");
    public static string FooterParentExperience => L("Veli & Oyuncu Deneyimi", "Parent & Player Experience", "Experiencia de Padres y Jugadores", "Eltern- & Spielererlebnis");
    public static string FooterScout => L("Scout Sistemi", "Scout System", "Sistema de Scouting", "Scout-System");
    public static string FooterSponsorship => L("Sponsorluk", "Sponsorship", "Patrocinio", "Sponsoring");
    public static string FooterHotels => L("Otel & Konaklama", "Hotels & Accommodation", "Hoteles y Alojamiento", "Hotels & Unterkunft");
    public static string FooterLocation => L("Antalya & İstanbul, Türkiye", "Antalya & Istanbul, Turkey", "Antalya e Estambul, Turquía", "Antalya & Istanbul, Türkei");
    public static string FooterContactForm => L("İletişim Formu", "Contact Form", "Formulario de Contacto", "Kontaktformular");
    public static string FooterCopyright => L(
        $"© {DateTime.Now.Year} Junior League Cup — Zenit Event. Tüm hakları saklıdır.",
        $"© {DateTime.Now.Year} Junior League Cup — Zenit Event. All rights reserved.",
        $"© {DateTime.Now.Year} Junior League Cup — Zenit Event. Todos los derechos reservados.",
        $"© {DateTime.Now.Year} Junior League Cup — Zenit Event. Alle Rechte vorbehalten.");
    public static string FooterKvkk => L("KVKK", "Privacy (KVKK)", "Privacidad (KVKK)", "Datenschutz (KVKK)");
    public static string FooterCookies => L("Çerez Politikası", "Cookie Policy", "Política de Cookies", "Cookie-Richtlinie");
    public static string FooterTerms => L("Kullanım Şartları", "Terms of Use", "Términos de Uso", "Nutzungsbedingungen");
    public static string BreadcrumbHome => L("Ana Sayfa", "Home", "Inicio", "Startseite");
    public static string LangTr => "TR";
    public static string LangEn => "EN";
    public static string LangEs => "ES";
    public static string LangDe => "DE";
    public static string LangSwitchAria => L("Dil seçimi", "Language selection", "Selección de idioma", "Sprachauswahl");
}
