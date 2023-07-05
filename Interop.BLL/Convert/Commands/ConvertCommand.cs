using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Interop.BLL.Convert.Commands
{
    public class ConvertCommand : IRequest<string>
    {
        [JsonPropertyName("original_file_path")]
        public IFormFile OriginalFile { get; set; }
    }
}
