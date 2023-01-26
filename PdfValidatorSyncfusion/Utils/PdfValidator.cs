using Syncfusion.Pdf.Parsing;

namespace PdfValidatorSyncfusion.Utils
{
    public class PdfValidator
    {
        public bool AnalizePdfFile(Stream pdfStream)
        {
            //Create a new instance of PDF document syntax analyzer.
            PdfDocumentAnalyzer analyzer = new PdfDocumentAnalyzer(pdfStream);
            //Analyze the syntax and return the results.
            SyntaxAnalyzerResult analyzerResult = analyzer.AnalyzeSyntax();

            bool isCorrupted;

            //Check whether the document is corrupted or not.
            if (analyzerResult.IsCorrupted)
            {
                isCorrupted = true;
            }
            else
            {
                isCorrupted = false;
            }
            analyzer.Close();

            return isCorrupted;
        }

        //public async Task CorrectFile(Stream pdfStream, string fileName)
        //{
        //    //load the corrupted document by setting the openAndRepair flag to true to repair the document.
        //    PdfLoadedDocument loadedPdfDocument = new PdfLoadedDocument(pdfStream, true);
         
        //    string correctedFileFolder = ConfigurationManager.AppSettings["correctedFileFolder"]; ;

        //    //Save the document.
        //    using (FileStream outputStream = new FileStream(Path.Combine(correctedFileFolder, fileName), FileMode.Create))
        //    {
        //        loadedPdfDocument.Save(outputStream);
        //    }
        //    //Close the document.
        //    loadedPdfDocument.Close(true);
        //    pdfStream.Dispose();
        //    await Task.CompletedTask;
        //}

    }   
}
