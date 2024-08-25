using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class TestClass2
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("GroupC")]
        public void Test_FindUnreachableStates_NoUnreachableStates()
        {
            var automaton = new FiniteAutomaton();
            automaton.SetStartState(0);
            automaton.AddTransition(0, 1);
            automaton.AddTransition(1, 2);
            automaton.AddTransition(2, 0);

            var unreachableStates = automaton.FindUnreachableStates();

            Assert.AreEqual(0, unreachableStates.Count, "All states should be reachable.");
        }

        [Test]
        [Category("GroupC")]
        public void Test_FindUnreachableStates_WithUnreachableStates()
        {
            var automaton = new FiniteAutomaton();
            automaton.SetStartState(0);
            automaton.AddTransition(0, 1);
            automaton.AddTransition(1, 2);
            automaton.AddTransition(3, 4);

            var unreachableStates = automaton.FindUnreachableStates();

            Assert.AreEqual(2, unreachableStates.Count, "Two states should be unreachable.");
            Assert.IsTrue(unreachableStates.Contains(3), "State 3 should be unreachable.");
            Assert.IsTrue(unreachableStates.Contains(4), "State 4 should be unreachable.");
        }
    }
}
