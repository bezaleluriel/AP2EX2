using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Ex1;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using mazeAdapter;

namespace Mission1
{
    class Program
    {
        static void Main(string[] args)
        {
            compareSolvers();
        }

        /***********************************************************************
       compareSolvers - static method, creates a maze, prints it out, solves it
       with BFS, DFS, prints number of nodes evaluated for each algorithm.
       ***********************************************************************/
        /// <summary>
        /// Compares the solvers.
        /// </summary>
        static void compareSolvers()
        {
            DFSMazeGenerator generator = new DFSMazeGenerator();
            Maze maze = generator.Generate(400, 400);
            Console.WriteLine(maze.ToString());
            SearchableMaze searchableMaze = new SearchableMaze(maze);
            BestFirstSearch<Position> bfs = new BestFirstSearch<Position>();
            bfs.search(searchableMaze);
            Console.Write("The number of nodes evaluated by bfs is: ");
            Console.WriteLine(bfs.getNumberOfNodesEvaluated());
            Dfs<Position> dfs = new Dfs<Position>();
            dfs.search(searchableMaze);
            Console.Write("The number of nodes evaluated by dfs is: ");
            Console.WriteLine(dfs.getNumberOfNodesEvaluated());
            Console.ReadLine();
        }
    }
}
