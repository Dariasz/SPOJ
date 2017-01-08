﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class EIGHTSTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.EIGHTS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"192
"
        };

        [TestMethod]
        public void EIGHTS() => TestSolution();
    }
}
