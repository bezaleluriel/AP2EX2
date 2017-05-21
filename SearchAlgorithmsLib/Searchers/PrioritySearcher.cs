using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        //MEMBERS
        /// <summary>
        /// The open list
        /// </summary>
        protected SimplePriorityQueue<State<T>> openList;

        //CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="PrioritySearcher{T}"/> class.
        /// </summary>
        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
        }

        /***************************************
        OpenListSize - returns size of openList.
        ***************************************/
        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize
        { // it is a read-only property 
            get { return openList.Count; }
        }

        /*******************************************************
        IsInTheOpenList - checks if a state is in the open list.
        *******************************************************/
        /// <summary>
        /// Determines whether [is in the open list] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is in the open list] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsInTheOpenList(State<T> s)
        {
            return openList.Contains(s);
        }

        /**************************************************************
        popOpenList - returns the top prioritorized member of the list.
        **************************************************************/
        /// <summary>
        /// Pops the open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        /*********************************************
        addToOpenList - adds a state to the open list.
        *********************************************/
        /// <summary>
        /// Adds to open list.
        /// </summary>
        /// <param name="s">The s.</param>
        protected void addToOpenList(State<T> s)
        {
            float floatedCost = (float)s.cost;
            // this is how we add an element to priority queue
            openList.Enqueue(s, floatedCost);
        }

        /************************************************
        updatePriority - changes the priority of a state.
        ************************************************/
        /// <summary>
        /// Updates the priority.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="p">The p.</param>
        protected void updatePriority(State<T> s, float p)
        {
            openList.UpdatePriority(s, p);
        }
    }
}
