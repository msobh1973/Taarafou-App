using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace Taarafou.Storage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        
        public StorageController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("uploads");
            var blobClient = containerClient.GetBlobClient(file.FileName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream);
            
            return Ok(blobClient.Uri.ToString());
        }
    }
}