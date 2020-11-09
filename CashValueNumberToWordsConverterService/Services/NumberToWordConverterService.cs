using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CashValueNumberToWordsConverterService
{
    public class NumberToWordConverterService : NumberToWordConverter.NumberToWordConverterBase
    {
        private readonly ILogger<NumberToWordConverterService> _logger;
        public NumberToWordConverterService(ILogger<NumberToWordConverterService> logger)
        {
            _logger = logger;
        }

        public override Task<NumberToWordConversionReply> Convert(NumberToWordConversionRequest request, ServerCallContext context)
        {
            return Task.FromResult(new NumberToWordConversionReply
            {
                ConvertedNumber = "",
                ErrorMessage = ""
            }) ;
        }
    }
}
