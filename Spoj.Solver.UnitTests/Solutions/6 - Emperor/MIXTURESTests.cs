﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class MIXTURESTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.MIXTURES;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
18 19
3
40 60 20"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"342
2400
"
        };

        [TestMethod]
        public void MIXTURES() => TestSolution();
    }
}
