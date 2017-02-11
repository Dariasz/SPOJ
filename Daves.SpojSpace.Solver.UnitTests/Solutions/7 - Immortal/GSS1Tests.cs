﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class GSS1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.GSS1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3 
-1 2 3
1
1 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
"
        };

        [TestMethod]
        public void GSS1() => TestSolution();
    }
}
