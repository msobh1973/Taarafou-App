using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator;

namespace Taarafou.Moderation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentModerationController : ControllerBase
    {
        private readonly ContentModeratorClient _client;

        public ContentModerationController(ContentModeratorClient client)
        {
            _client = client;
        }

        [HttpPost("text")]
        public async Task<IActionResult> ModerateText(TextModerationModel model)
        {
            var screen = await _client.TextModeration.ScreenTextAsync(modelToScreen: model.Text, language: "ara", autocorrect: true, pII: true, classify: true);

            return Ok(screen);
        }

        [HttpPost("image")]
        public async Task<IActionResult> ModerateImage(IFormFile image)
        {
            await using var imageStream = image.OpenReadStream();
            var screen = await _client.ImageModeration.EvaluateFileInputAsync(imageStream, cacheImage: true);

            return Ok(screen);
        }
    }
}