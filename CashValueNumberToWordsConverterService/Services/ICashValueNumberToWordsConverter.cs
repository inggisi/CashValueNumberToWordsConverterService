using System;
using System.Collections.Generic;
using System.Text;

namespace CashValueNumberToWordsConverterService
{
    public interface ICashValueNumberToWordsConverter
    {
        string ConvertCashValueFromNumberToWords(double cashValueAsNumber);
    }
}
