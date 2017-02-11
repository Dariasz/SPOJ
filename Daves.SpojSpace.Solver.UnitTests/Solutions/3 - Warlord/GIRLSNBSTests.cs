﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class GIRLSNBSTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.GIRLSNBS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10 10
5 1
0 1000
-1 -1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
3
1000
"
        };

        [TestMethod]
        public void GIRLSNBS() => TestSolution();
    }
}
