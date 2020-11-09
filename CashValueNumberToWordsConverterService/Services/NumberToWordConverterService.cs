using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CashValueNumberToWordsConverterService
{
    public class NumberToWordConverterService : NumberToWordConverter.NumberToWordConverterBase
    {
        private readonly ILogger<NumberToWordConverterService> _logger;
        private readonly ICashValueNumberToWordsConverter _converter;
        public NumberToWordConverterService(ILogger<NumberToWordConverterService> logger, ICashValueNumberToWordsConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        public override Task<NumberToWordConversionReply> Convert(NumberToWordConversionRequest request, ServerCallContext context)
        {
            try
            {
               var convertedNumber = _converter.ConvertCashValueFromNumberToWords(request.NumberToConvert);
                var result = new NumberToWordConversionReply()
                {
                    ConvertedNumber = convertedNumber,
                    ErrorMessage = ""
                };
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                var result = new NumberToWordConversionReply()
                {
                    ConvertedNumber = "",
                    ErrorMessage = ex.Message
                };
                return Task.FromResult(result);
            }
        }
    }
}
