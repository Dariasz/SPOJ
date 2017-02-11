﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class MAJOR_v2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.MAJOR_v2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
4
2 1 2 2
6
1 1 1 2 2 2
5
1 2 4 5 1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"YES 2
NO
NO
"
        };

        [TestMethod]
        public void MAJOR_v2() => TestSolution();
    }
}
