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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<string>> Convert([FromBody] ConvertCommand command)
        {
            var result = await Mediator.Send(command);
            return this.Ok($"Pdf file path: {result}");
        }
    }
}
