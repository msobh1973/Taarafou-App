using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.PersonalizerRuntime;
using System.Threading.Tasks;
using Taarafou.Recommendations.Models;

namespace Taarafou.Recommendations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalizationController : ControllerBase
    {
        private readonly PersonalizerRuntimeClient _personalizerClient;

        public PersonalizationController(PersonalizerRuntimeClient personalizerClient)
        {
            _personalizerClient = personalizerClient;
        }

        [HttpPost]
        public async Task<IActionResult> GetRecommendation(RecommendationRequestModel model)
        {
            var request = new RankRequest(actions: model.Actions, contextFeatures: model.Context, userId: model.UserId);
            var response = await _personalizerClient.RankAsync(request);

            return Ok(response.RewardActionId);
        }
    }
}