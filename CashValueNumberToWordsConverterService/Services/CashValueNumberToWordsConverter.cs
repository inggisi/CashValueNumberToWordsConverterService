using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("CashValueNumberToWordsConverterServiceTests")]
namespace CashValueNumberToWordsConverterService
{
    public class CashValueNumberToWordsConverter : ICashValueNumberToWordsConverter
    {
        const double MAX_ALLOWED_NUMBER = 999_999_999.99d;
        const double MIN_ALLOWED_NUMBER = 0d;

        public string ConvertCashValueFromNumberToWords(double cashValueAsNumber)
        {
            string centsAsWord = "";
            string cashValueAsWord = "";

            LimitCheck(cashValueAsNumber);

            var dollarAndCentsSplitted = cashValueAsNumber.ToString().Split(",");
            var numberSplittedCount = dollarAndCentsSplitted.Count();
            cashValueAsWord = GetDollarsAsWord(dollarAndCentsSplitted[0]);
            if (numberSplittedCount == 2)
            {
                centsAsWord = GetCentsAsWord(dollarAndCentsSplitted[1]);
                cashValueAsWord = $"{cashValueAsWord} and {centsAsWord}";
            }
            return cashValueAsWord;
        }

        private void LimitCheck(double cashValueAsNumber)
        {
            if (cashValueAsNumber > MAX_ALLOWED_NUMBER)
            {
                throw new Exception($"The input number {cashValueAsNumber} is greater than the allowed max number {MAX_ALLOWED_NUMBER}!");
            }
            if (cashValueAsNumber < MIN_ALLOWED_NUMBER)
            {
                throw new Exception($"The input number {cashValueAsNumber} is lower than the allowed min number {MIN_ALLOWED_NUMBER}!");
            }
        }

        private string GetDollarsAsWord(string dollarBlock)
        {
            var dollarsAsWord = "";
            var dollars = int.Parse(dollarBlock);
            var dollarsSplittedIntoBlocks = SplitNumberIntoBlocks(dollars, 3).Reverse();

            int blockCounter = 0;
            foreach (var threeDigitBlock in dollarsSplittedIntoBlocks)
            {
                var blockAsWord = GetNumberLowerThanThousandAsWord(threeDigitBlock);

                if (blockCounter == 0)
                {
                    dollarsAsWord = $"{blockAsWord} {(blockAsWord == "one" ? "dollar" : "dollars")}";
                }

                if (blockCounter == 1)
                {
                    dollarsAsWord = $"{blockAsWord} thousand {dollarsAsWord}";
                }

                if (blockCounter == 2)
                {
                    dollarsAsWord = $"{blockAsWord} million {dollarsAsWord}";
                }

                blockCounter++;
            }
            return dollarsAsWord;
        }

        private string GetCentsAsWord(string centBlock)
        {
            if (centBlock.Length == 1)
            {
                centBlock = $"{centBlock}0";
            }
            if (centBlock.Length > 2)
            {
                var detailedCentString = centBlock.Insert(2, ",");
                var detailedCentAsDouble = double.Parse(detailedCentString);
                centBlock = Math.Round(detailedCentAsDouble, 0).ToString();
            }
            var centsAsNumber = int.Parse(centBlock);
            var centsAsWord = GetNumberLowerThanOneHundredAsWord(centsAsNumber);

            if (centsAsWord == "one")
            {
                centsAsWord = "one cent";
            }
            else
            {
                centsAsWord = $"{centsAsWord} cents";
            }

            return centsAsWord;
        }


        internal string GetNumberLowerThanOneHundredAsWord(int number)
        {
            string nonDerivedNumber;
            string tennerNumber;

            if (number >= 100)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().ReflectedType}:{MethodBase.GetCurrentMethod().Name}:Input parameter {number} has 3 digits, but only 2 are allowed!");
            }

            if (IsNonDerivedNumber(number))
            {
                nonDerivedNumber = GetNonDerivedNumberAsWord(number);
                return nonDerivedNumber;
            }

            var splittedNumber = SplitNumberIntoBlocks(number, 1);
            nonDerivedNumber = GetNonDerivedNumberAsWord(splittedNumber[1]);
            if (number < 20)
            {
                return $"{nonDerivedNumber}teen";
            }

            tennerNumber = GetTennerNumberAsWord(splittedNumber[0]);

            if (splittedNumber[1] == 0)
            {
                return tennerNumber;
            }

            return $"{tennerNumber}-{nonDerivedNumber}";
        }

        private bool IsNonDerivedNumber(int number) => (number <= 13 || number == 15);

        private string GetNonDerivedNumberAsWord(int number)
        {
            var gotNumberAsWord = NumberWords.NonDerivedNumbers.TryGetValue(number, out string nonDerivedNumberAsWord);
            if (!gotNumberAsWord)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().ReflectedType}:{MethodBase.GetCurrentMethod().Name}:Could not get a non derived number as word for key {number}!");
            }
            return nonDerivedNumberAsWord;
        }

        private string GetTennerNumberAsWord(int number)
        {
            var gotTennerNumberAsWord = NumberWords.TennerNumbers.TryGetValue(number, out string tennerNumber);
            if (!gotTennerNumberAsWord)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().ReflectedType}:{MethodBase.GetCurrentMethod().Name}:Could not get a the tenner number as word for key {number}!");
            }
            return tennerNumber;
        }

        private string GetNumberLowerThanThousandAsWord(int number)
        {
            string numberAsWord;
            string twoLowerSignificantDigitsAsWord;
            string mostSignificantDigitAsWord;
            int twoLowerSignificantDigitsAsNumber;

            if (number >= 1000)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().ReflectedType}:{MethodBase.GetCurrentMethod().Name}:Input parameter {number} has 4 digits, but only 3 are allowed!");
            }

            if (number < 10)
            {
                numberAsWord = GetNumberLowerThanOneHundredAsWord(number);
                return numberAsWord;
            }

            var splittedNumber = SplitNumberIntoBlocks(number, 1);
            if (number < 100)
            {
                twoLowerSignificantDigitsAsNumber = splittedNumber[0] + splittedNumber[0] * 9 + splittedNumber[1];
                twoLowerSignificantDigitsAsWord = GetNumberLowerThanOneHundredAsWord(twoLowerSignificantDigitsAsNumber);
                return twoLowerSignificantDigitsAsWord;
            }

            twoLowerSignificantDigitsAsNumber = splittedNumber[1] + splittedNumber[1] * 9 + splittedNumber[2];
            twoLowerSignificantDigitsAsWord = GetNumberLowerThanOneHundredAsWord(twoLowerSignificantDigitsAsNumber);
            mostSignificantDigitAsWord = GetNumberLowerThanOneHundredAsWord(splittedNumber[0]);
            numberAsWord = $"{mostSignificantDigitAsWord} hundred {(twoLowerSignificantDigitsAsNumber == 0 ? "" : twoLowerSignificantDigitsAsWord)}";
            return numberAsWord.Trim();
        }

        private int[] SplitNumberIntoBlocks(int number, int blockSize)
        {
            var blockSizeFactor = (int)Math.Pow(10, blockSize);
            List<int> numberBlocks = new List<int>();
            if (number == 0)
            {
                numberBlocks.Add(number);
                return numberBlocks.ToArray();
            }
            while (number > 0)
            {
                numberBlocks.Add(number % blockSizeFactor);
                number /= blockSizeFactor;
            }
            numberBlocks.Reverse();
            return numberBlocks.ToArray();
        }
    }
}
