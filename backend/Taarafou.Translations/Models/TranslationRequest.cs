namespace Taarafou.Translations.Models
{
    public class TranslationRequest
    {
        public string Text { get; set; } = string.Empty;
        public string From { get; set; } = "en";
        public string To   { get; set; } = string.Empty;
    }
}
