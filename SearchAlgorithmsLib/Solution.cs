using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T>
    {
        //MEMBERS:
        /// <summary>
        /// Gets or sets the solution.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public List<State<T>> solution { get; set; }
        /// <summary>
        /// Gets or sets the evaluated nodes.
        /// </summary>
        /// <value>
        /// The evaluated nodes.
        /// </value>
        public int evaluatedNodes { get; set; }

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        public Solution()
        {
            solution = new List<State<T>>();
        }

        /***********************************************************
        addToSolutionList - adds a state to the solution states list.
        ***********************************************************/
        /// <summary>
        /// Adds to solution list.
        /// </summary>
        /// <param name="state">The state.</param>
        public void addToSolutionList(State<T> state)
        {
            solution.Add(state);
        }

        /*****************************************
        ToString - overriding the ToString method.
        *****************************************/
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>();
            string check;
            string builder = null;
            string reverseBuilder = null;
            string x = null;
            string y = null;
            foreach (State<T> state in this.solution)
            {
                //bool secondHalf = false;
                check = state.state.ToString();
                int i = 1;
                while(check[i] != ',')
                {
                    x += check[i];
                    i += 1;                    
                }
                i += 1;
                while(i < check.Count()-1)
                {
                    y += check[i];
                    i += 1;
                }
                list.Add(new Tuple<int, int>(Int32.Parse(x), Int32.Parse(y)));
                x = null;
                y = null;
            }
            
            for(int i = 1; i<list.Count; i++)
            {
                if(list[i].Item1 > list[i-1].Item1)
                {
                    builder += "2";
                }
                if (list[i].Item1 < list[i - 1].Item1)
                {
                    builder += "3";
                }
                if (list[i].Item2 > list[i - 1].Item2)
                {
                    builder += "0";
                }
                if (list[i].Item2 < list[i - 1].Item2)
                {
                    builder += "1";
                }
            }
            for (int j = builder.Count() - 1; j >= 0; j--)
            {
                reverseBuilder += builder[j];
            }

                return reverseBuilder;
        }
    }
}