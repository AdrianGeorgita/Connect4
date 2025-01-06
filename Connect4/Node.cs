using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Node(Node parent, Board board, int column, PlayerType player) {
            this.column = column;
            this.parent = parent;
            this.board = board;
            this.board.CheckFinish(out bool finished, out PlayerType winner);
            this.player = player;
        }
    }
}
