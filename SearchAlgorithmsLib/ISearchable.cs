using SearchAlgorithmsLib;
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
    public interface ISearchable<T>
    {
        /*******************************************
        getInitialState - returns the initial state.
        *******************************************/
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        State<T> getInitialState();

        /*************************************
        getGoalState - returns the goal state.
        *************************************/
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        State<T> getGoalState();

        /******************************************
        getInitialState - returns the initial state.
        ******************************************/
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        List<State<T>> getAllPossibleStates(State<T> s);
    }
}
