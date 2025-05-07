using Microsoft.AspNetCore.Mvc;
namespace Taarafou.Speech.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeechController : ControllerBase
    {
        // مثال: تحويل الكلام إلى نص
        [HttpPost("speech-to-text")]
        public IActionResult SpeechToText(/* المعطيات هنا */)
        {
            // منطقك هنا
            return Ok();
        }

        // مثال: تحويل النص إلى كلام
        [HttpPost("text-to-speech")]
        public IActionResult TextToSpeech(/* المعطيات هنا */)
        {
            // منطقك هنا
            return Ok();
        }
    }
}
