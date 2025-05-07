namespace Taarafou.TextToSpeech.Models
{
    public class TextToSpeechModel
    {
        // النص المراد تحويله إلى كلام
        public string Text { get; set; } = string.Empty;
        
        // (اختياري) يمكنك إضافة خصائص أخرى مثل Voice أو Format
        // public string Voice { get; set; } = "en-US-JennyNeural";
    }
}
