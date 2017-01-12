using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class FCTRL2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.FCTRL2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1
2
5
3
55"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
2
120
6
12696403353658275925965100847566516959580321051449436762275840000000000000
"
        };

        [TestMethod]
        public void FCTRL2() => TestSolution();
    }
}
