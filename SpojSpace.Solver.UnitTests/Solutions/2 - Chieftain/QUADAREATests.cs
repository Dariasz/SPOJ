﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._2___Chieftain
{
    [TestClass]
    public sealed class QUADAREATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.QUADAREA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
1 2 1 2
0.5 0.5 0.5 0.5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
0.25
"
        };

        [TestMethod]
        public void QUADAREA() => TestSolution();
    }
}
