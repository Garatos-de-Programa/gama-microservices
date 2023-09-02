using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.Interfaces.FileManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gama.Api.Controllers
{
    [ApiController]
    [Route("v1/traffic-fines/files")]
    public class FileController : Controller
    {
        private readonly IFileManager _fileManager;

        public FileController(
            IFileManager fileManager
            )
        {
            _fileManager = fileManager;
        }

        [HttpGet("{path}")]
        [Authorize(Roles = RolesName.Cop)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] string path, CancellationToken cancellationToken)
        {
            var fileStream = await _fileManager.GetFileAsync(path, cancellationToken);

            return File(fileStream.File, "application/octet-stream", fileStream.Name);
        }
    }
}
