using MediatR;
using Microsoft.Office.Interop.Word;

namespace Interop.BLL.Convert.Commands
{
    public class ConvertCommandHandler : IRequestHandler<ConvertCommand, string>
    {

        public async Task<string> Handle(ConvertCommand command, CancellationToken cancellationToken)
        {
            var wordApplication = new Application();

            var extension = Path.GetExtension(command.OriginalFile.FileName).ToLower();
            var wordDocument = wordApplication.Documents.Add();

            var tempFilePath = Path.GetTempFileName();
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                await command.OriginalFile.CopyToAsync(fileStream);
            }

            if (extension == ".doc" || extension == ".docx")
            {
                wordDocument = wordApplication.Documents.Open(tempFilePath);
            }
            else
            {
                //var textContent = await File.ReadAllTextAsync(tempFilePath);
                //wordDocument.Content.Text = textContent;
            }

            var pdfFilePath = Path.ChangeExtension(command.OriginalFile.FileName, ".pdf");
            wordDocument.ExportAsFixedFormat(pdfFilePath, WdExportFormat.wdExportFormatPDF);

            wordDocument.Close();
            File.Delete(tempFilePath);
            wordApplication.Quit();

            return pdfFilePath;
        }
    }
}
