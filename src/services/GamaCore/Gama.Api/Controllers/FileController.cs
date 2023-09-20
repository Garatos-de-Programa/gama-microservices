using Amazon.S3;
using Gama.Domain.Interfaces.FileManagement;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gama.Api.Controllers
{
    [ApiController]
    [Route("v1/files")]
    public class FileController : Controller
    {
        private readonly IFileManager _fileManager;

        public FileController(
            IFileManager fileManager
            )
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string path, CancellationToken cancellationToken)
        {
            try
            {
                var fileStream = await _fileManager.GetFileAsync(path, cancellationToken);

                return File(fileStream.File, "application/octet-stream", fileStream.Name);
            }
            catch (AmazonS3Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                var fileUrl = await _fileManager.UploadAsync(new FileObject(file), cancellationToken);

                return Ok(fileUrl);
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
        }
    }
}
