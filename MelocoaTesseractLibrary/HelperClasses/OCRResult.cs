using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelocoaTesseractLibrary.HelperClasses
{
    public class OcrResult
    {
        public string Text { get; set; }
        public byte[] OcredBest { get; set; }
        public byte[] OcredCompressed { get; set; }
    }
}
