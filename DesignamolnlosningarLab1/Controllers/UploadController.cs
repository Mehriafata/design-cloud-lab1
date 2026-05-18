using DesignamolnlosningarLab1.Models;
using DesignamolnlosningarLab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesignamolnlosningarLab1.Controllers
{
    [ApiController]
    [Route("api")]
    public class UploadController : ControllerBase
    {
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request, [FromServices] BlobService blobService)
        {
            var file = request.File;
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var url = await blobService.UploadFileAsync(file);


            return Ok(new
            {
                message = "File uploaded successfully.",
                Url = url
            });
        }
    }
}
