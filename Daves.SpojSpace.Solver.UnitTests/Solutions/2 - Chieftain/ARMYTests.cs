﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._2___Chieftain
{
    [TestClass]
    public sealed class ARMYTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.ARMY;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2

1 1
1
1

3 2
1 3 2
5 5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Godzilla
MechaGodzilla
"
        };

        [TestMethod]
        public void ARMY() => TestSolution();
    }
}
