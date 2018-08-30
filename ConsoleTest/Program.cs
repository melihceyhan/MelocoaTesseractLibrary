
using MelocoaTesseractLibrary;
using MelocoaTesseractLibrary.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourcePdfPath = "";
            var finalPdfPath = "";
            var finalPdfPath2 = "";
            var finalTextPath = "";
            var progress = new OcrProcess();
            var result  = progress.Ocr(sourcePdfPath, ResultTypeEnum.All);
            File.WriteAllBytes(finalPdfPath, result.OcredBest);
            File.WriteAllBytes(finalPdfPath2, result.OcredCompressed);
            File.WriteAllText(finalTextPath, result.Text);
        }
    }
}
