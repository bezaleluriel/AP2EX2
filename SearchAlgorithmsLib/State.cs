using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T>
    {
        //MEMBERS:
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public T state { get; set; }                 // the state represented by T.
        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public double cost { get; set; }             // cost to reach this state (set by a setter)
        /// <summary>
        /// Gets or sets the came from.
        /// </summary>
        /// <value>
        /// The came from.
        /// </value>
        public State<T> cameFrom { get; set; }       // the state we came from to this state (setter)

        /***************************************************************
        CONSTRUCTOR - this is a private constructor. we use a state pool.
        ***************************************************************/
        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        private State(T state)                                    
        {
            this.state = state;
        }

        /******************************************
        Equals - we overload Object's Equals method.
        ******************************************/
        /// <summary>
        /// Equalses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public bool Equals(State<T> s)                           
        {
            return s != null && state.Equals((s as State<T>).state);
        }

        /****************************************************************
        overriding the getHashCode method for contains func to work right.
        ****************************************************************/
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.state.ToString().GetHashCode();
        }

        /**********************************************************************
        StatePool - inner static class, makes sure there is only one instance of
        each state we create. 
        **********************************************************************/
        /// <summary>
        /// 
        /// </summary>
        public static class StatePool
        {
            //MEMBERS:
            /// <summary>
            /// The hash set
            /// </summary>
            public static Dictionary<T, State<T>> hashSet = new Dictionary<T, State<T>>();

            /**********************************************************************
            getInstance - if there is already an istance of this state it returns
            it and if not creates it and adds it to the pool.
            **********************************************************************/
            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <param name="position">The position.</param>
            /// <returns></returns>
            public static State<T> getInstance(T position)
            {
                if (hashSet.ContainsKey(position))
                {
                    return hashSet[position];
                }
                else
                {
                    State<T> state = new State<T>(position);
                    hashSet.Add(position, state);
                    return state;
                }
            }
        }
    }
}
