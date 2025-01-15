using System.Collections.Generic;

namespace Connect4 {
    internal class Node {
        public Node parent = null;
        public List<Node> children = new List<Node>();

        public int visits = 0;
        public double wins = 0;

        public PlayerType winner;
        public bool finished;

        public Board board;
        public PlayerType player;

        public int column;

        /// <summary>
        /// Constructor pentru un nod din arbore
        /// </summary>
        /// <param name="parent">Nodul părinte</param>
        /// <param name="board">Tabla de joc a nodului</param>
        /// <param name="column">Mutarea prin care se ajunge din părinte în nod</param>
        /// <param name="player">Jucătorul care a realizat mutarea</param>
        public Node(Node parent, Board board, int column, PlayerType player) {
            this.column = column;
            this.parent = parent;
            this.board = board;
            this.board.CheckFinish(out finished, out winner);
            this.player = player;
        }
    }
}
