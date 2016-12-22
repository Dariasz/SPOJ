﻿using System;
using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public sealed class StringSortingTestSuite : ITestSuite
    {
        private const int _stringLength = 10000;
        private readonly string[] randomStrings1;
        private readonly string[] randomStrings2;
        private readonly string[] binaryStrings;
        private readonly string[] equalStrings;
        private string randomString;

        public StringSortingTestSuite()
        {
            randomStrings1 = new string[_stringLength];
            randomStrings2 = new string[_stringLength];
            for (int i = 0; i < _stringLength; ++i)
            {
                randomStrings1[i] = randomStrings2[i] = InputGenerator.GenerateRandomString(_stringLength);
            }

            binaryStrings = new string[_stringLength];
            for (int i = 0; i < _stringLength; ++i)
            {
                binaryStrings[i] = InputGenerator.GenerateRandomString(_stringLength, '0', '1');
            }

            equalStrings = new string[_stringLength];
            for (int i = 0; i < _stringLength; ++i)
            {
                equalStrings[i] = InputGenerator.GenerateRandomString(_stringLength, 'a', 'a');
            }

            randomString = InputGenerator.GenerateRandomString(_stringLength);
        }

        public IReadOnlyList<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"Array.Sort, string length {_stringLength}", new TestCase[]
                {
                    new TestCase($"{_stringLength} random strings, ordinal", SortRandomStringsOrdinal),
                    new TestCase($"{_stringLength} random strings, current culture", SortRandomStringsCurrentCulture),
                    new TestCase($"{_stringLength} binary strings", SortBinaryStrings),
                    new TestCase($"{_stringLength} equal strings", SortEqualStrings),
                    new TestCase("Random string suffixes (as indices)", SortBinaryStringSuffixesAsIndices),
                    new TestCase("Random string suffixes (as strings)", SortBinaryStringSuffixesAsStrings),
                })
        };

        public void SortRandomStringsOrdinal()
            => Array.Sort(randomStrings1, StringComparer.Ordinal);

        public void SortRandomStringsCurrentCulture()
            => Array.Sort(randomStrings2, StringComparer.CurrentCulture);

        public void SortBinaryStrings()
            => Array.Sort(binaryStrings, StringComparer.Ordinal);

        public void SortEqualStrings()
            => Array.Sort(equalStrings, StringComparer.Ordinal);

        public void SortBinaryStringSuffixesAsIndices()
        {
            int[] randomStringSuffixIndices = new int[_stringLength];
            for (int i = 0; i < _stringLength; ++i)
            {
                randomStringSuffixIndices[i] = i;
            }
            Array.Sort(randomStringSuffixIndices, (i, j) => string.CompareOrdinal(randomString, i, randomString, j, _stringLength));
        }

        public void SortBinaryStringSuffixesAsStrings()
        {
            string[] randomStringSuffixes = new string[_stringLength];
            for (int i = 0; i < _stringLength; ++i)
            {
                randomStringSuffixes[i] = randomString.Substring(i);
            }
            Array.Sort(randomStringSuffixes, StringComparer.Ordinal);
        }
    }
}
