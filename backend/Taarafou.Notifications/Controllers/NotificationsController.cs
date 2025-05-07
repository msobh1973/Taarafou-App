using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Taarafou.Notifications.Models;

namespace Taarafou.Notifications.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationHubClient _notificationHubClient;

        public NotificationsController(NotificationHubClient notificationHubClient)
        {
            _notificationHubClient = notificationHubClient;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] NotificationModel model)
        {
            if (model == null)
                return BadRequest("Notification payload is null.");

            var payloadDict = new Dictionary<string, string>
            {
                { "title", model.Title },
                { "message", model.Message }
            };

            var payloadJson = JsonSerializer.Serialize(payloadDict);

            try
            {
                await _notificationHubClient.SendFcmNativeNotificationAsync(payloadJson);
                return Ok(new { Status = "NotificationSent" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error sending notification: {ex.Message}");
            }
        }
    }
}
