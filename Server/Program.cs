using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;
using Client;
using Server;
using mazeAdapter;

//using Ex1.Interfaces;
namespace Server
{



    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /***********************************************
        main - this is the main function of the program.
        ***********************************************/
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {

            Controller controller = new Controller();
            View view = new View(controller);
            Model model = new Model(controller);
            controller.setModel(model);
            controller.setView(view);
            view.startServer();
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