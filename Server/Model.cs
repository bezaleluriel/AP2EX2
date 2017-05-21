using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Client;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using Server;
using Newtonsoft.Json.Linq;
//using Client;
using mazeAdapter;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    class Model
    {
        //MEMBERS:
        /// <summary>
        /// The controller
        /// </summary>
        private Controller controller;
        /// <summary>
        /// The generator
        /// </summary>
        private DFSMazeGenerator generator;
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;
        /// <summary>
        /// The BFS solver
        /// </summary>
        private BestFirstSearch<Position> BFSSolver;// Position is from Maze is the position in the maze
        /// <summary>
        /// The DFS solver
        /// </summary>
        private Dfs<Position> DFSSolver;
        /// <summary>
        /// The maze dictionary
        /// </summary>
        private Dictionary<string, Maze> mazeDictionary;
        /// <summary>
        /// The solution dictionary
        /// </summary>
        private Dictionary<string, Solution<Position>> BFSSolutionDictionary;
        /// <summary>
        /// The solution dictionary
        /// </summary>
        private Dictionary<string, Solution<Position>> DFSSolutionDictionary;
        /// <summary>
        /// The waiting games dictionary
        /// </summary>
        private Dictionary<string, Maze> waitingGamesDictionary;
        /// <summary>
        /// The players list
        /// </summary>
        private Dictionary<MultyPlayer, Maze> playersList;
        /// <summary>
        /// The clients list
        /// </summary>
        private Dictionary<TcpClient, TcpClient> connectionsList;
        /// <summary>
        /// The active games
        /// </summary>
        private Dictionary<string, Maze> activeGames;


        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public Model(Controller controller)
        {
            this.controller = controller;
            mazeDictionary = new Dictionary<string, Maze>();
            BFSSolutionDictionary = new Dictionary<string, Solution<Position>>();
            DFSSolutionDictionary = new Dictionary<string, Solution<Position>>();
            waitingGamesDictionary = new Dictionary<string, Maze>();
            playersList = new Dictionary<MultyPlayer, Maze>();
            activeGames = new Dictionary<string, Maze>();
            connectionsList = new Dictionary<TcpClient, TcpClient>();
        }

        /*********************************
        GenerateMaze - generates a maze.
        *********************************/
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns></returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            generator = new DFSMazeGenerator();
            maze = generator.Generate(rows, cols);
            maze.Name = name;
            mazeDictionary[name] = maze;// intilize the name key with the maze value - to save the mazes or somthing like this//
            return maze;
        }

        /*****************************************************
        solveMaze - looks for a maze with a specific name and 
        and solves it with a specific algorithm.       
        *****************************************************/
        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        public Solution<Position> solveMaze(string name, int algorithm)
        {
            //if the solution dictionary holds the solution, we return it.
            if ((algorithm == 0)&&(this.BFSSolutionDictionary.ContainsKey(name)))
            {
                return BFSSolutionDictionary[name];
            }
            //if the solution dictionary holds the solution, we return it.
            if ((algorithm == 1) && (this.DFSSolutionDictionary.ContainsKey(name)))
            {
                return DFSSolutionDictionary[name];
            }
            //if this maze was not created at all - print error message.
            if (!(mazeDictionary.ContainsKey(name)))
            {
                Console.WriteLine("this maze does not exist");
            }
            //if the number representing the algorithm is 0 - we use BFS.
            if (algorithm == 0)
            {
                BFSSolver = new BestFirstSearch<Position>();
                Maze maze = mazeDictionary[name];
                SearchableMaze searchableMaze = new SearchableMaze(maze);
                Solution<Position> solution = BFSSolver.search(searchableMaze);
                BFSSolutionDictionary.Add(name, solution);
                return solution;
            }
            //if the number representing the algorithm is 0 - we use DFS.
            if (algorithm == 1)
            {
                DFSSolver = new Dfs<Position>();
                Maze maze = mazeDictionary[name];
                SearchableMaze searchableMaze = new SearchableMaze(maze);
                Solution<Position> solution = DFSSolver.search(searchableMaze);
                DFSSolutionDictionary.Add(name, solution);
                return solution;
            }
            return null;
        }

        /// <summary>
        /// Gets the lists.
        /// </summary>
        /// <returns></returns>
        public JArray getLists()
        {

            JArray listArr = new JArray();
            // List<string> avaList = new List<string>();
            foreach (string name in waitingGamesDictionary.Keys)
            {
                if (waitingGamesDictionary[name] != null)
                {
                    listArr.Add(name);
                }
            }
            return listArr;
        }
        
        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public Maze start(string name, int rows, int cols, TcpClient c)
        {
            Maze maze;

            if (mazeDictionary.ContainsKey(name))
            {
                maze = mazeDictionary[name];
            }
            else
            {
                maze = this.GenerateMaze(name, rows, cols);
            }

            if (waitingGamesDictionary.ContainsKey(name))
            {
                waitingGamesDictionary[name] = maze;
            }
            else
            {
                waitingGamesDictionary.Add(name, maze);
            }
            
            MultyPlayer player = new MultyPlayer(c);

            if (playersList.ContainsKey(player))
            {
                playersList[player] = maze;
            }
            else
            {
                playersList.Add(player, maze);
            }
            player.Wait();
            return maze;
        }

        /// <summary>
        /// Joins the specified name of game.
        /// </summary>
        /// <param name="nameOfGame">The name of game.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public Maze join(string nameOfGame, TcpClient client)
        {
            Maze m;
            if (waitingGamesDictionary.ContainsKey(nameOfGame))
            {
                m = waitingGamesDictionary[nameOfGame];
                // lamda expression is func that i declare and use it immidetly 
                MultyPlayer player1 = playersList.First(blabla => { return blabla.Value.Equals(m); }).Key;
                activeGames.Add(nameOfGame, m);
                waitingGamesDictionary[nameOfGame] = null;
                MultyPlayer player2 = new MultyPlayer(client);
                playersList.Add(player2, m);
                player1.StopWait();
                //adding the the player's tcp's to TcpList, we will use it in play command.
                if (connectionsList.ContainsKey(player1.tcpClient))
                {
                    connectionsList[player1.tcpClient] = player2.tcpClient;
                }
                else
                {
                    connectionsList.Add(player1.tcpClient, player2.tcpClient);
                }
                if (connectionsList.ContainsKey(player2.tcpClient))
                {
                    connectionsList[player2.tcpClient] = player1.tcpClient;
                }
                else
                {
                    connectionsList.Add(player2.tcpClient, player1.tcpClient);
                }
                return maze;
            }
            return null;
        }

        /// <summary>
        /// Gets the other player.
        /// </summary>
        /// <param name="playingClient">The playing client.</param>
        /// <returns></returns>
        public Tuple<TcpClient, string> getOtherPlayer(TcpClient playingClient)
        {
            Tuple<TcpClient, string> tup;
            TcpClient otherClient;
            string mazeName = null;
            if (connectionsList.ContainsKey(playingClient))
            {
                otherClient = connectionsList[playingClient];
            }
            else
            {
                Console.WriteLine("1-play command error - client is not in an active game");
                return null;
            }
            foreach (MultyPlayer player in playersList.Keys)
            {
                if (player.tcpClient.Equals(otherClient))
                {
                    mazeName = playersList[player].Name;
                    break;
                }
            }
            tup = new Tuple<TcpClient, string>(otherClient, mazeName);
            return tup;
        }


        //// <summary>
        /// returns the other player removes everything about this game from all lists.
        /// </summary>
        /// <param name="playingClient"></param>
        /// <returns></returns>
        public TcpClient Close(TcpClient playingClient)
        {
            TcpClient otherClient;
            string mazeName = null;
            int counter = 0;
            MultyPlayer thisPlayer = null;
            MultyPlayer otherPlayer = null;
            if (connectionsList.ContainsKey(playingClient))
            {
                otherClient = connectionsList[playingClient];
                connectionsList.Remove(otherClient);
                connectionsList.Remove(playingClient);
            }
            else
            {
                return null;
            }
            foreach (MultyPlayer player in playersList.Keys)
            {
                if ((player.tcpClient.Equals(playingClient)))
                {
                    counter += 1;
                    thisPlayer = player;
                }


                if ((player.tcpClient.Equals(otherClient)))
                {
                    mazeName = playersList[player].Name;
                    counter += 1;
                    otherPlayer = player;
                }

                if (counter == 2)
                {
                    playersList.Remove(thisPlayer);
                    playersList.Remove(otherPlayer);
                    activeGames.Remove(mazeName);
                    break;
                }
            }
            return otherClient;
        }
    }
}
