﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ANARC09ATests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.ANARC09A;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"}{
{}{}{}
{{{}
---"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1. 2
2. 0
3. 1
"
        };

        [TestMethod]
        public void ANARC09A() => TestSolution();
    }
}
