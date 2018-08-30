using Clock.Pdf;
using iTextSharp.text.pdf;
using MelocoaTesseractLibrary.HelperClasses;
using System;
using System.IO;

namespace MelocoaTesseractLibrary
{
    public class OcrProcess
    {
        private readonly FileHelper _fileHelper = new FileHelper();
        public OcrProcess(bool forceGetRequiedEmbeddedReaources = false)
        {
            if (!File.Exists(_fileHelper.AssemblyDirectory + @"\" + "AllRequired.rar") || forceGetRequiedEmbeddedReaources)
            {
                _fileHelper.ExtractEmbeddedResource(_fileHelper.AssemblyDirectory, "AllRequired.rar");
                _fileHelper.ExtractRarFile("AllRequired.rar", string.Empty, _fileHelper.AssemblyDirectory);
            }
            if (Environment.GetEnvironmentVariable("TESSDATA_PREFIX", EnvironmentVariableTarget.Machine) == null || Environment.GetEnvironmentVariable("TESSDATA_PREFIX", EnvironmentVariableTarget.Machine) != _fileHelper.AssemblyDirectory)
                Environment.SetEnvironmentVariable("TESSDATA_PREFIX", _fileHelper.AssemblyDirectory, EnvironmentVariableTarget.Machine);
        }

        public OcrResult Ocr(string pdfPath,ResultTypeEnum resultTypeEnum , string language = "tur")
        {
            var result = new OcrResult();
            var doc = PDFDoc.Open(pdfPath);
            if (!string.IsNullOrEmpty(doc.GetText())) return result;
            doc.Ocr(Clock.Utils.OcrMode.Tesseract, language, WriteTextMode.Word, null);

            if (resultTypeEnum.HasFlag(ResultTypeEnum.OcrCompressed) || resultTypeEnum.HasFlag(ResultTypeEnum.All))
            {
                var reader = new PdfReader(doc.PDFBytes);
                var stamper = new PdfStamper(reader, new FileStream(Path.Combine(Path.GetTempPath(), "Clock.hocr", Path.GetFileNameWithoutExtension(pdfPath) + "ocrCompressed" + Path.GetExtension(pdfPath)), FileMode.Create), PdfWriter.VERSION_1_5);
                var pageNum = reader.NumberOfPages;
                for (var i = 1; i <= pageNum; i++)
                {
                    reader.SetPageContent(i, reader.GetPageContent(i));
                }
                stamper.SetFullCompression();
                stamper.Close();
            }
            if(resultTypeEnum.HasFlag(ResultTypeEnum.Text) || resultTypeEnum.HasFlag(ResultTypeEnum.All))
                result.Text = doc.GetText();
            if (resultTypeEnum.HasFlag(ResultTypeEnum.OcrBest) || resultTypeEnum.HasFlag(ResultTypeEnum.All))
                result.OcredBest = doc.PDFBytes;
            if (resultTypeEnum.HasFlag(ResultTypeEnum.OcrCompressed) || resultTypeEnum.HasFlag(ResultTypeEnum.All))
                result.OcredCompressed = System.IO.File.ReadAllBytes(Path.Combine(Path.GetTempPath(), "Clock.hocr", Path.GetFileNameWithoutExtension(pdfPath) + "ocrCompressed" + Path.GetExtension(pdfPath)));

            _fileHelper.DeleteDirectory(Path.Combine(Path.GetTempPath(), "Clock.hocr"));
            return result;

        }


    }
}
