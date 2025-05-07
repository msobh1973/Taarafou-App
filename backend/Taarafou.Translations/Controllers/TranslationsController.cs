using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azure.AI.Translation.Text;
using Taarafou.Translations.Models;

namespace Taarafou.Translations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationsController : ControllerBase
    {
        private readonly TextTranslationClient _translationClient;

        public TranslationsController(TextTranslationClient translationClient)
        {
            _translationClient = translationClient;
        }

        [HttpPost]
        public async Task<IActionResult> Translate([FromBody] TranslationRequest request)
        {
            if (request == null
                || string.IsNullOrWhiteSpace(request.Text)
                || string.IsNullOrWhiteSpace(request.To))
            {
                return BadRequest("Invalid payload.");
            }

            // نمرّر النص في مصفوفة ولائحة لغات الهدف في مصفوفة
            var response = await _translationClient.TranslateAsync(
                new[] { request.Text },   // مصفوفة المحتوى
                new[] { request.To }      // مصفوفة لغات الهدف
            );

            var firstResult    = response.Value[0];
            var translatedText = firstResult.Translations[0].Text;

            return Ok(new { translatedText });
        }
    }
}
