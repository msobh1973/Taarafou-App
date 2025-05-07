using Taarafou.TextToSpeech.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;

namespace Taarafou.Speech.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class TextToSpeechController : ControllerBase
    {
        private readonly SpeechConfig _speechConfig;

        public TextToSpeechController(SpeechConfig speechConfig)  
        {
            _speechConfig = speechConfig;
        }

        [HttpPost]
        public async Task<IActionResult> ConvertTextToSpeech(TextToSpeechModel model)
        {
            // استخدام خدمة Azure Speech لتحويل النص إلى كلام
            using var synthesizer = new SpeechSynthesizer(_speechConfig);
            var result = await synthesizer.SpeakTextAsync(model.Text);
            
            // إعادة الصوت الناتج كملف
            return File(result.AudioData, "audio/wav");
        }
    }
}