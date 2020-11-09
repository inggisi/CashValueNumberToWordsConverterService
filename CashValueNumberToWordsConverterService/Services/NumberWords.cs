using System;
using System.Collections.Generic;
using System.Text;

namespace CashValueNumberToWordsConverterService
{


    public static class NumberWords
    {
        public static Dictionary<int, string> NonDerivedNumbers => new Dictionary<int, string>() {
            { 0,"zero" },
            { 1,"one" },
            { 2,"two" },
            { 3,"three" },
            { 4,"four" },
            { 5,"five" },
            { 6,"six" },
            { 7,"seven" },
            { 8,"eight" },
            { 9,"nine" },
            { 10,"ten" },
            { 11,"eleven" },
            { 12,"twelve" },
            { 13,"thirteen" },
            { 15,"fifteen" },
            };

        public static Dictionary<int, string> TennerNumbers => new Dictionary<int, string>() {
            {2, "twenty"},
            {3, "thirty"},
            {4, "fourty"},
            {5, "fifty"},
            {6, "sixty"},
            {7, "seventy"},
            {8, "eighty"},
            {9, "ninety"},
        };


    }
}
