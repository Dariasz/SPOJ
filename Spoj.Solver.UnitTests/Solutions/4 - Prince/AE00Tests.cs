using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class AE00Tests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.AE00;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"8
"
        };

        [TestMethod]
        public void AE00() => TestSolution();
    }
}
