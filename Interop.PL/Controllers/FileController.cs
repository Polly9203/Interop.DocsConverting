using Interop.BLL.Convert.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interop.PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        protected IMediator Mediator { get; set; }

        public FileController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Convert(IFormFile originalFile)
        {
            var command = new ConvertCommand() {OriginalFile = originalFile};
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
