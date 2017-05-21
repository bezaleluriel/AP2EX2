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
    public interface ISearcher<T>
    {
        /****************
        the search method
        ****************/
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        Solution<T> search(ISearchable<T> searchable);

        /*************************************************
        get how many nodes were evaluated by the algorithm
        *************************************************/
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        int getNumberOfNodesEvaluated();                    
    }
}
