using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelocoaTesseractLibrary.HelperClasses
{
    [Flags]
    public enum ResultTypeEnum
    {
        Text = 1,
        OcrBest = 2,
        OcrCompressed =4,
        All = 8
        
    }
}
