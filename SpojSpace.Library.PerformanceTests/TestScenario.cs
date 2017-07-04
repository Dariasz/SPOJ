﻿using System.Collections.Generic;

namespace SpojSpace.Library.PerformanceTests
{
    public class TestScenario
    {
        public TestScenario(string name, IEnumerable<TestCase> testCases)
        {
            Name = name;
            TestCases = testCases;
        }

        public string Name { get; }
        public IEnumerable<TestCase> TestCases { get; }
    }
}
