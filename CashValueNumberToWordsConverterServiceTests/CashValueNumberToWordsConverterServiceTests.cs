using System;
using System.Collections;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using CashValueNumberToWordsConverterService;
using System.Diagnostics;
using System.Windows;

namespace CashValueNumberToWordsConverterServiceTests
{
    [TestFixture]
    public class CashValueNumberToWordsConverterServiceTests
    {

        CashValueNumberToWordsConverterService.CashValueNumberToWordsConverter _cashValueNumberToWordsConverter;

        [SetUp]
        public void Init()
        {
            _cashValueNumberToWordsConverter = new CashValueNumberToWordsConverterService.CashValueNumberToWordsConverter();
        }

        [TestCase(0, ExpectedResult = "zero")]
        [TestCase(1, ExpectedResult = "one")]
        [TestCase(2, ExpectedResult = "two")]
        [TestCase(3, ExpectedResult = "three")]
        [TestCase(4, ExpectedResult = "four")]
        [TestCase(5, ExpectedResult = "five")]
        [TestCase(6, ExpectedResult = "six")]
        [TestCase(7, ExpectedResult = "seven")]
        [TestCase(8, ExpectedResult = "eight")]
        [TestCase(9, ExpectedResult = "nine")]
        [TestCase(10, ExpectedResult = "ten")]
        [TestCase(11, ExpectedResult = "eleven")]
        [TestCase(12, ExpectedResult = "twelve")]
        [TestCase(13, ExpectedResult = "thirteen")]
        [TestCase(15, ExpectedResult = "fifteen")]
        [TestCase(16, ExpectedResult = "sixteen")]
        [TestCase(20, ExpectedResult = "twenty")]
        [TestCase(21, ExpectedResult = "twenty-one")]
        [TestCase(30, ExpectedResult = "thirty")]
        [TestCase(32, ExpectedResult = "thirty-two")]
        [TestCase(40, ExpectedResult = "fourty")]
        [TestCase(43, ExpectedResult = "fourty-three")]
        [TestCase(50, ExpectedResult = "fifty")]
        [TestCase(54, ExpectedResult = "fifty-four")]
        [TestCase(60, ExpectedResult = "sixty")]
        [TestCase(65, ExpectedResult = "sixty-five")]
        [TestCase(70, ExpectedResult = "seventy")]
        [TestCase(76, ExpectedResult = "seventy-six")]
        [TestCase(80, ExpectedResult = "eighty")]
        [TestCase(87, ExpectedResult = "eighty-seven")]
        [TestCase(90, ExpectedResult = "ninety")]
        [TestCase(99, ExpectedResult = "ninety-nine")]
        public string ConvertTwoDigitNumberInputNonDerivedNumberShouldReturnAsWord(int nonDerivedNumber)
        {
            var numberAsWord = _cashValueNumberToWordsConverter.GetNumberLowerThanOneHundredAsWord(nonDerivedNumber);
            return numberAsWord;
        }
        [Test]
        public void ConvertTwoDigitNumberInputThreeDigitNumberShouldThrowAnException()
        {
            var inputWithThreeDigits = 100;
            Assert.Throws<Exception>(() => _cashValueNumberToWordsConverter.GetNumberLowerThanOneHundredAsWord(inputWithThreeDigits));
        }

        [Test]
        public void ConvertCashValueFromNumberToWordsInputGreaterThanMaxShouldThrowAnException()
        {
            var inputGreaterThanMax = 1_000_000_000;
            Assert.Throws<Exception>(() => _cashValueNumberToWordsConverter.ConvertCashValueFromNumberToWords(inputGreaterThanMax));
        }

        [Test]
        public void ConvertCashValueFromNumberToWordsInputLowerThanMinShouldThrowAnException()
        {
            var inputLowerThanMin = -1;
            Assert.Throws<Exception>(() => _cashValueNumberToWordsConverter.ConvertCashValueFromNumberToWords(inputLowerThanMin));
        }

        [TestCase(0, ExpectedResult = "zero dollars")]
        [TestCase(0.01, ExpectedResult = "zero dollars and one cent")]
        [TestCase(0.007, ExpectedResult = "zero dollars and one cent")]
        [TestCase(0.03, ExpectedResult = "zero dollars and three cents")]
        [TestCase(1, ExpectedResult = "one dollar")]
        [TestCase(1.01, ExpectedResult = "one dollar and one cent")]
        [TestCase(1.45, ExpectedResult = "one dollar and fourty-five cents")]
        [TestCase(25.1, ExpectedResult = "twenty-five dollars and ten cents")]
        [TestCase(1000, ExpectedResult = "one thousand dollars")]
        [TestCase(45100, ExpectedResult = "fourty-five thousand one hundred dollars")]
        [TestCase(100_000_000, ExpectedResult = "one hundred million dollars")]
        [TestCase(100_000_000.00, ExpectedResult = "one hundred million dollars")]
        [TestCase(000_100_000.00, ExpectedResult = "one hundred thousand dollars")]
        [TestCase(999_999_999.99, ExpectedResult = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public string ConvertCashValueFromNumberToWordsInputShouldReturnAsWord(double number)
        {
            var numberAsWord = _cashValueNumberToWordsConverter.ConvertCashValueFromNumberToWords(number);
            return numberAsWord;
        }


    }
}
