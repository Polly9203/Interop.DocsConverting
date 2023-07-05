using MediatR;
using Microsoft.Office.Interop.Word;

namespace Interop.BLL.Convert.Commands
{
    public class ConvertCommandHandler : IRequestHandler<ConvertCommand, string>
    {

        public async Task<string> Handle(ConvertCommand command, CancellationToken cancellationToken)
        {
            var wordApplication = new Application();

            var extension = Path.GetExtension(command.OriginalFilePath).ToLower();
            var wordDocument = wordApplication.Documents.Add();

            if (extension == ".doc" || extension == ".docx")
            {
                wordDocument = wordApplication.Documents.Open(command.OriginalFilePath);
            }
            else
            {
                var textContent = await File.ReadAllTextAsync(command.OriginalFilePath);
                wordDocument.Content.Text = textContent;
            }

            var pdfFilePath = Path.ChangeExtension(command.OriginalFilePath, ".pdf");
            wordDocument.ExportAsFixedFormat(pdfFilePath, WdExportFormat.wdExportFormatPDF);

            return pdfFilePath;
        }
    }
}
