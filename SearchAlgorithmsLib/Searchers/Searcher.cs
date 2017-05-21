using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.ISearcher{T}" />
    public abstract class Searcher<T> : ISearcher<T>
    {
        //MEMBERS
        /// <summary>
        /// The evaluated nodes
        /// </summary>
        protected int evaluatedNodes;

        //CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }

        /************************************************************
        getNumberOfNodesEvaluated - returns number of nodes evaluated.
        ************************************************************/
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /******************************************************************
        search - returns a solution to the problem the algorithm is solving.
        ******************************************************************/
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public abstract Solution<T> search(ISearchable<T> searchable);

    }
}
