
using MelocoaTesseractLibrary;
using MelocoaTesseractLibrary.HelperClasses;
using System.IO;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourcePdfPath = ""; //@"C:\Users\Melocoa\Desktop\devops\a.pdf";
            var finalPdfPath =  ""; //@"C:\Users\Melocoa\Desktop\devops\b.pdf"; ;
            var finalPdfPath2 = ""; //@"C:\Users\Melocoa\Desktop\devops\c.pdf"; ;
            var finalTextPath = ""; //@"C:\Users\Melocoa\Desktop\devops\d.txt"; ;
            var progress = new OcrProcess();
            var result  = progress.Ocr(sourcePdfPath, ResultTypeEnum.All);
            File.WriteAllBytes(finalPdfPath, result.OcredBest);
            File.WriteAllBytes(finalPdfPath2, result.OcredCompressed);
            File.WriteAllText(finalTextPath, result.Text);
        }
    }
}
