using System;
using System.Collections.Generic;

public class FiniteAutomaton
{
    public Dictionary<int, List<int>> transitions = new Dictionary<int, List<int>>();
    public HashSet<int> states = new HashSet<int>();
    public int startState;

    public void AddTransition(int fromState, int toState)
    {
        if(fromState < 0 || toState < 0)
        {
            throw new ArgumentException("State cannot be negative");
        }

        if (!transitions.ContainsKey(fromState))
        {
            transitions[fromState] = new List<int>();
        }

        transitions[fromState].Add(toState);
        states.Add(fromState);
        states.Add(toState);
    }

    public void SetStartState(int state)
    {
        startState = state;
        states.Add(state);
    }

    public HashSet<int> FindUnreachableStates()
    {
        var reachable = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(startState);

        while (queue.Count > 0)
        {
            int state = queue.Dequeue();
            reachable.Add(state);

            if (transitions.ContainsKey(state))
            {
                foreach (int nextState in transitions[state])
                {
                    if (!reachable.Contains(nextState))
                    {
                        queue.Enqueue(nextState);
                    }
                }
            }
        }

        var unreachable = new HashSet<int>(states);
        unreachable.ExceptWith(reachable);
        return unreachable;
    }

    public HashSet<int> FindDeadEndStates()
    {
        var deadEnds = new HashSet<int>();
        foreach (var state in states)
        {
            if (!transitions.ContainsKey(state) || transitions[state].Count == 0)
            {
                deadEnds.Add(state);
            }
        }
        return deadEnds;
    }
}

class Program
{
    static void Main()
    {
        var automaton = new FiniteAutomaton();
        automaton.SetStartState(0);
        automaton.AddTransition(0, 1);
        automaton.AddTransition(1, 2);
        automaton.AddTransition(2, 0);
        automaton.AddTransition(3, 4);

        // Add more transitions

        var unreachableStates = automaton.FindUnreachableStates();
        var deadEndStates = automaton.FindDeadEndStates();

        Console.WriteLine("Unreachable States: " + string.Join(", ", unreachableStates));
        Console.WriteLine("Dead End States: " + string.Join(", ", deadEndStates));
    }
}