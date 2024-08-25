using NUnit;


namespace TestProject1
{
    public class TestClass1
    {
        private FiniteAutomaton automaton;

        [SetUp]
        public void Setup()
        {
            automaton = new FiniteAutomaton();
        }

        [Test]
        [Category("GroupA")]
        public void Test_FindUnreachableStates_NoUnreachableStates()
        {
            automaton.SetStartState(0);
            automaton.AddTransition(0, 1);
            automaton.AddTransition(1, 2);
            automaton.AddTransition(2, 0);

            var unreachableStates = automaton.FindUnreachableStates();

            Assert.AreEqual(0, unreachableStates.Count, "All states should be reachable.");
        }

        [Test]
        [Category("GroupA")]
        public void Test_FindUnreachableStates_WithUnreachableStates()
        {
            automaton.SetStartState(0);
            automaton.AddTransition(0, 1);
            automaton.AddTransition(1, 2);
            automaton.AddTransition(3, 4);

            var unreachableStates = automaton.FindUnreachableStates();

            Assert.AreEqual(2, unreachableStates.Count, "Two states should be unreachable.");
            Assert.IsTrue(unreachableStates.Contains(3), "State 3 should be unreachable.");
            Assert.IsTrue(unreachableStates.Contains(4), "State 4 should be unreachable.");
        }

        [Test]
        [Category("GroupA")]
        public void Test_FindDeadEndStates_WithDeadEndStates()
        {
            automaton.SetStartState(0);
            automaton.AddTransition(0, 1);
            automaton.AddTransition(1, 2);

            var deadEndStates = automaton.FindDeadEndStates();

            Assert.AreEqual(1, deadEndStates.Count, "One state should be a dead end.");
            Assert.IsTrue(deadEndStates.Contains(2), "State 2 should be a dead end.");
        }

        [Test]
        [Category("GroupA")]
        public void Test_FindDeadEndStates_NoDeadEndStates()
        {
            automaton.SetStartState(0);
            automaton.AddTransition(0, 1);
            automaton.AddTransition(1, 2);
            automaton.AddTransition(2, 0);

            var deadEndStates = automaton.FindDeadEndStates();

            Assert.AreEqual(0, deadEndStates.Count, "No states should be dead ends.");
        }

        [Test]
        [Category("GroupB")]
        public void Test_AddTransition_ThrowsException_ForNegativeState()
        {
            var ex = Assert.Throws<ArgumentException>(() => automaton.AddTransition(-1, 1),
                                                "Adding a transition from a negative state should throw an exception.");
            Assert.That(ex.Message, Is.EqualTo("State cannot be negative"));
        }

        [TestCase(0, 1, ExpectedResult = 1)]
        [TestCase(1, 2, ExpectedResult = 1)]
        [TestCase(2, 3, ExpectedResult = 1)]
        [Category("GroupB")]
        public int TestParameterized_FindDeadEndStates(int start, int end)
        {
            automaton.SetStartState(start);
            automaton.AddTransition(start, end);

            var deadEndStates = automaton.FindDeadEndStates();
            return deadEndStates.Count;
        }
    }
}