﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class PT07ZTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.PT07Z;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1 2
2 3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
"
        };

        [TestMethod]
        public void PT07Z() => TestSolution();
    }
}
