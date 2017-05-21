using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.PrioritySearcher{T}" />
    public class BestFirstSearch<T> : PrioritySearcher<T>
    {

        /***************************************************
        search - activates the BFS algorithm on the problem.
        ***************************************************/
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = popOpenList();
                closed.Add(n);
                if (n.Equals(searchable.getGoalState()))
                {
                    return backTrace(n);
                }
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    // todo update the cost
                    if (!(closed.Contains(s)) && !(IsInTheOpenList(s)))
                    {
                        addToOpenList(s);
                        s.cameFrom = n;
                        // s.cost = theNewCost;

                    }
                    // the cost of the new way is better
                    else if (!IsInTheOpenList(s))
                    {
                        //  s.cost = theNewCost;


                        if ((n.cost + 1) < (s.cost))
                        {
                            s.cameFrom = n;
                            //s.SetCost(n.GetCost() + 1);
                            openList.UpdatePriority(s, (float)s.cost);
                        }


                        //                        addToOpenList(s);
                        //
                        //                        s.cameFrom = n;
                        //
                        //                        updatePriority(s, (float)s.cost);
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
            while (state != null)
            {
                solution.addToSolutionList(state);
                state = state.cameFrom;
            }
            solution.evaluatedNodes = this.evaluatedNodes;
            return solution;
        }
    }
}