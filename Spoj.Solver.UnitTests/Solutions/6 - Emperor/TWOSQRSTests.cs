﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class TWOSQRSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TWOSQRS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"11
1
2
7
14
49
9
17
76
2888
27
999999999989"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Yes
Yes
No
No
Yes
Yes
Yes
No
Yes
No
Yes
"
        };

        [TestMethod]
        public void TWOSQRS() => TestSolution();
    }
}
