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
            var pdfFilePath = Path.ChangeExtension(command.OriginalFile.FileName, ".pdf");

            if (extension == ".doc" || extension == ".docx")
            {
                var tempFilePath = Path.GetTempFileName();
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await command.OriginalFile.CopyToAsync(fileStream);
                }

                var wordDocument = wordApplication.Documents.Open(tempFilePath);
                wordDocument.ExportAsFixedFormat(pdfFilePath, WdExportFormat.wdExportFormatPDF);

                wordDocument.Close();
                File.Delete(tempFilePath);
                wordApplication.Quit();
            }
            else
            {
                throw new NotSupportedException("Unsupported file format.");
            }

            return pdfFilePath;
        }
    }
}
