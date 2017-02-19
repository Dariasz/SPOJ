﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class OLOLOTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.OLOLO;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1
8
1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"8
"
        };

        [TestMethod]
        public void OLOLO() => TestSolution();
    }
}
