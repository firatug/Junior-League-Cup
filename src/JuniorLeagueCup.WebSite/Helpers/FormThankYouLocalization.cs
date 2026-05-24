using JuniorLeagueCup.WebSite.Localization;
using JuniorLeagueCup.WebSite.Models;

namespace JuniorLeagueCup.WebSite.Helpers;

public static class FormThankYouLocalization
{
    public static (string Title, string Message) GetContent(string formType) =>
        LocaleHelper.Current switch
        {
            "en" => GetEnglish(formType),
            "es" => GetSpanish(formType),
            "de" => GetGerman(formType),
            _ => FormThankYouTypes.GetContent(formType)
        };

    private static (string Title, string Message) GetEnglish(string formType) => formType switch
    {
        FormThankYouTypes.TakimBasvurusu => (
            "Application Received",
            "Your team application has been submitted successfully. We will contact you shortly."),
        FormThankYouTypes.Iletisim => (
            "Message Received",
            "Your contact form has been submitted successfully. We will get back to you soon."),
        FormThankYouTypes.Sponsorluk => (
            "Request Received",
            "Your sponsorship request has been submitted successfully. Our team will contact you shortly."),
        _ => ("Thank You", "Your form has been submitted successfully.")
    };

    private static (string Title, string Message) GetSpanish(string formType) => formType switch
    {
        FormThankYouTypes.TakimBasvurusu => (
            "Solicitud Recibida",
            "Su solicitud de equipo se ha enviado correctamente. Nos pondremos en contacto con usted pronto."),
        FormThankYouTypes.Iletisim => (
            "Mensaje Recibido",
            "Su formulario de contacto se ha enviado correctamente. Le responderemos pronto."),
        FormThankYouTypes.Sponsorluk => (
            "Solicitud Recibida",
            "Su solicitud de patrocinio se ha enviado correctamente. Nuestro equipo se pondrá en contacto pronto."),
        _ => ("Gracias", "Su formulario se ha enviado correctamente.")
    };

    private static (string Title, string Message) GetGerman(string formType) => formType switch
    {
        FormThankYouTypes.TakimBasvurusu => (
            "Anmeldung Erhalten",
            "Ihre Teamanmeldung wurde erfolgreich übermittelt. Wir werden uns in Kürze bei Ihnen melden."),
        FormThankYouTypes.Iletisim => (
            "Nachricht Erhalten",
            "Ihr Kontaktformular wurde erfolgreich übermittelt. Wir melden uns bald bei Ihnen."),
        FormThankYouTypes.Sponsorluk => (
            "Anfrage Erhalten",
            "Ihre Sponsoring-Anfrage wurde erfolgreich übermittelt. Unser Team wird sich in Kürze melden."),
        _ => ("Vielen Dank", "Ihr Formular wurde erfolgreich übermittelt.")
    };
}
