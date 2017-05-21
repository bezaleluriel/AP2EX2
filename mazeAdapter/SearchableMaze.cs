using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace mazeAdapter
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Position" />
    public class SearchableMaze : ISearchable<Position>
    {
        //MEMBERS
        /// <summary>
        /// The maze
        /// </summary>
        Maze maze;

        /// <summary>
        /// Gets or sets all possible states.
        /// </summary>
        /// <value>
        /// All possible states.
        /// </value>
        public List<State<Position>> allPossibleStates { get; set; }

        //CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchableMaze"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public SearchableMaze(Maze maze)
        {
            allPossibleStates = new List<State<Position>>();
            this.maze = maze;
        }

        /**************************************************************************************************
        getAllPossibleStates - returns a list with all of the states that are possible to reach(neighbors).
        **************************************************************************************************/
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            allPossibleStates.Clear();
            int row = s.state.Row;
            int col = s.state.Col;
            //adding left neighbor to list.
            if ((col > 0) && (maze[row, col - 1] == CellType.Free))
            {
                allPossibleStates.Add(State<Position>.StatePool.getInstance(new Position(row, col - 1)));
            }
            //adding upper neighbor to list.
            if ((row > 0) && (maze[row - 1, col ] == CellType.Free))
            {
                allPossibleStates.Add(State<Position>.StatePool.getInstance(new Position(row - 1, col)));
            }
            //adding right neighbor to list.
            if ((col < maze.Cols - 1) && (maze[row, col + 1] == CellType.Free))
            {
                allPossibleStates.Add(State<Position>.StatePool.getInstance(new Position(row, col + 1)));
            }
            //adding lower neighbor to list.
            if ((row < maze.Rows - 1) && (maze[row + 1, col] == CellType.Free)) {
                allPossibleStates.Add(State<Position>.StatePool.getInstance(new Position(row + 1, col)));
            }

            return allPossibleStates;
        }

        /*************************************
        getGoalState - returns the goal state.
        *************************************/
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        public State<Position> getGoalState()
        {
            return State<Position>.StatePool.getInstance(maze.GoalPos);
        }

        /*******************************************
        getInitialState - returns the initial state.
        *******************************************/
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<Position> getInitialState()
        {
            return State<Position>.StatePool.getInstance(maze.InitialPos);
        }
    }
}
