using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class TestSearchable<T> : ISearchable<T>
    {
        /// <summary>
        /// From
        /// </summary>
        private State<T> from,
            to;

        /// <summary>
        /// The adj
        /// </summary>
        Dictionary<State<T>, List<State<T>>> Adj;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestSearchable{T}"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="Adj">The adj.</param>
        public TestSearchable(State<T> from, State<T> to, Dictionary<State<T>, List<State<T>>> Adj)
        {
            this.from = from;
            this.to = to;
            this.Adj = Adj;
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<T> getInitialState()
        {
            return from;
        }

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        public State<T> getGoalState()
        {
            return to;
        }

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public List<State<T>> getAllPossibleStates(State<T> s)
        {
            List<State<T>> states = null;
            // if found the vertex
            if (Adj.ContainsKey(s))
            {
                states = Adj[s];
            }

            // if vertex does not exist or no neighbors found
            if (states == null)
            {
                states = new List<State<T>>();
            }
            return states;
        }
    }






/**


    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SearchAlgorithmsLib;
class Program
{
    static void Main(string[] args)
    {
        ISearcher<int> ser = new BestFirstSearch<int>();

        Dictionary<State<int>, List<State<int>>> Adj = new Dictionary<State<int>, List<State<int>>>();
        State<int> one = new State<int>(1);
        State<int> two = new State<int>(2);
        State<int> three = new State<int>(3);
        State<int> four = new State<int>(4);
        State<int> five = new State<int>(5);
        State<int> six = new State<int>(6);
        State<int> seven = new State<int>(7);

        Adj[one] = new List<State<int>> { two, three };
        Adj[two] = new List<State<int>> { four, five };
        Adj[three] = new List<State<int>> { two, six };
        Adj[four] = new List<State<int>>();
        Adj[five] = new List<State<int>> { six };
        Adj[six] = new List<State<int>> { seven };
        Adj[seven] = new List<State<int>> { three };

        TestSearchable<int> test1 = new TestSearchable<int>(one, six, Adj);
        Solution<int> sol = ser.search(test1);

        printSol(sol);
        Console.ReadLine();
    }


    static void printSol<T>(Solution<T> sn)
    {
        List<State<T>> s = sn.solution;
        for (int i = 0; i < s.Count; i++)
        {
            Console.Write(s[i].state.ToString() + ",");
        }
        Console.WriteLine();
    }
}


**/
}