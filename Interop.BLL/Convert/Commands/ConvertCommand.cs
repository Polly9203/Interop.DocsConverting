using MediatR;
using System.Text.Json.Serialization;

namespace Interop.BLL.Convert.Commands
{
    public class ConvertCommand : IRequest<string>
    {
        [JsonPropertyName("original_file_path")]
        public string OriginalFilePath { get; set; }
    }
}
