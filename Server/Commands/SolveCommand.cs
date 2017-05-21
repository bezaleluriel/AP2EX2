using MazeLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICommand" />
    internal class SolveCommand : ICommand
    {
        //MEMBERS:
        /// <summary>
        /// The model
        /// </summary>
        private Model model;

        //CONSTRUCTOR:
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveCommand(Model model)
        {
            this.model = model;
        }

        /******************************
        Execute - executes the command.
        ******************************/
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            Solution<Position> solution = model.solveMaze(name, algorithm);
            string s = toJason(name, solution, solution.evaluatedNodes);
            return s;
        }

        /******************************
        toJason - creates a json obj.
        ******************************/
        /// <summary>
        /// To the jason.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="solution">The solution.</param>
        /// <param name="nodesevaluated">The nodesevaluated.</param>
        /// <returns></returns>
        public string toJason(string name, Solution<Position> solution, int nodesevaluated)
        {
            JObject solutionObj = new JObject();
            solutionObj["Name"] = name;
            solutionObj["Solution"] = solution.ToString();
            solutionObj["NodesEvaluated"] = nodesevaluated;
            return solutionObj.ToString() + '\n';
        }

    }
}