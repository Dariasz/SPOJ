﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class FENCE1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.FENCE1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"0.16
"
        };

        [TestMethod]
        public void FENCE1() => TestSolution();
    }
}
