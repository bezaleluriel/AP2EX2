using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public class Dfs<T> : Searcher<T>
    {

        //MEMBERS
        /// <summary>
        /// Gets or sets the discoverd.
        /// </summary>
        /// <value>
        /// The discoverd.
        /// </value>
        List<State<T>> discoverd { get; set; }
        /// <summary>
        /// Gets or sets the open stack.
        /// </summary>
        /// <value>
        /// The open stack.
        /// </value>
        Stack<State<T>> openStack { get; set; }

        //CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="Dfs{T}"/> class.
        /// </summary>
        public Dfs()
        {
            openStack = new Stack<State<T>>();
            discoverd = new List<State<T>>();
        }

        /****************************************************************
        search - runs the dfs algorithm on a problem given as a parameter.
        ****************************************************************/
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> search(ISearchable<T> searchable)
        {
            //placing initial state in stack.
            openStack.Push(searchable.getInitialState());
            while (openStack.Count > 0)
            {
                State<T> temp = openStack.Pop();
                //if we found the state we were looking for:
                if (temp.Equals(searchable.getGoalState()))
                {
                    return backTrace(temp);
                }
                //if the current state has not been discovered yet:
                if (!discoverd.Contains(temp))
                {
                    evaluatedNodes++;
                    List<State<T>> succerssors = searchable.getAllPossibleStates(temp);
                    discoverd.Add(temp);
                    foreach (State<T> s in succerssors)
                    {
                        if (!discoverd.Contains(s))
                        {
                            s.cameFrom = temp;
                            openStack.Push(s);
                        }
                    }
                }
            }
            return null;
        }

        /*************************************************************************************
        backTrace - returns the solution, follows the parents from goal state to initial state.
        *************************************************************************************/
        /// <summary>
        /// Backs the trace.
        /// </summary>
        /// <param name="goal">The goal.</param>
        /// <returns></returns>
        public Solution<T> backTrace(State<T> goal)
        {
            Solution<T> solution = new Solution<T>();
            State<T> state = goal;
            while (state.cameFrom != null)
            {
                solution.addToSolutionList(state);
                state = state.cameFrom;
            }
            solution.addToSolutionList(state);
            solution.evaluatedNodes = this.evaluatedNodes;
            return solution;
        }
    }
}
