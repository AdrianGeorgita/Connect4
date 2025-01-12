using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("MCTSTests")]

namespace Connect4 {

    /* Algoritmul utilizează o cpie a tablei de joc trimisă ca parametru în constructor
     * 
     * Mutările jucătorului sunt transmise prin metoda OpponentMove
     * Mutările jucătorului sunt realizate pe tabla de joc a algoritmului
     * 
     * Murătile calculatorului sunt returnate de metoda run
     * Mutările calculatorului ar trebui realizate pe tabla de joc a jucătorului
     * 
     */

    internal class MCTS {
        private readonly double C = Math.Sqrt(2);
        readonly Random rand = new Random();

        private Node root;

        public MCTS(Board board) {
            if (board == null) {
                throw new ArgumentNullException("board");
            }

            root = new Node(null, board, -1, PlayerType.Human);
        }

        public int Run(int iterations) {
            if (iterations < 1) {
                throw new ArgumentException("iterations < 1", "iterations");
            }

            if (root.player != PlayerType.Human) {
                throw new SystemException("wrong player turn");
            }

            for (int i = 0; i < iterations; i++) {
                Node node = Select();
                Node newNode = Expand(node);
                double result = Simulate(newNode);
                Backpropagate(newNode, result);
            }

            if (root.children.Count == 0) {
                return -1;
            }

            root = root.children.Where(n => n.visits == root.children.Max(a => a.visits)).OrderBy(_ => rand.Next()).First();
            return root.column;
        }

        public void OpponentMove(int column) {
            if (column < 0 || column >= root.board.Columns) {
                throw new ArgumentException($"column must be in betwen 0 and {root.board.Columns - 1}", "column");
            }

            if (root.player != PlayerType.Computer) {
                throw new SystemException("wrong player turn");
            }

            foreach (Node child in root.children) {
                if (child.column == column) {
                    root = child;
                    root.parent = null;
                    return;
                }
            }

            int row = root.board.GetAvailableRow(column);
            if (row == -1) {
                throw new SystemException("invalid move");
            }

            Board newBoard = new Board(root.board);
            newBoard.Pieces.Add(new Piece(column, row, root.board.Pieces.Count, PlayerType.Human));

            root = new Node(null, newBoard, column, PlayerType.Human);
        }

        private double UTC(Node node) {
            if (node.visits == 0) {
                return double.MaxValue;
            }

            return node.wins / node.visits + C * Math.Sqrt(Math.Log(node.parent.visits) / node.visits);
        }

        private Node Select() {
            Node node = root;

            while (node.children.Count > 0) {
                node = node.children.Where(n => UTC(n) == node.children.Max(a => UTC(a))).OrderBy(_ => rand.Next()).First();
            }

            return node;
        }

        private Node Expand(Node node) {
            if (node.finished) {
                return node;
            }

            PlayerType newPlayer = (node.player == PlayerType.Computer) ? PlayerType.Human : PlayerType.Computer;

            for (int a = 0; a < node.board.Columns; a++) {
                int row;
                if ((row = node.board.GetAvailableRow(a)) != -1) {
                    Board newBoard = new Board(node.board);
                    newBoard.Pieces.Add(new Piece(a, row, node.board.Pieces.Count, newPlayer));

                    Node newNode = new Node(node, newBoard, a, newPlayer);
                    node.children.Add(newNode);
                }
            }

            if (node.children.Count == 0) {
                return node;
            }

            return node.children[rand.Next(node.children.Count)];   
        }

        private double Simulate(Node node) {
            Board board = new Board(node.board);
            PlayerType player = node.player;

            while (true) {
                board.CheckFinish(out bool finished, out PlayerType winner);

                if (finished) {
                    switch (winner) {
                        case PlayerType.Computer:
                            return 1.0;
                        case PlayerType.None:
                            return 0.5;
                        case PlayerType.Human:
                            return 0.0;
                    }
                }

                player = (player == PlayerType.Computer) ? PlayerType.Human : PlayerType.Computer;

                List<(int col, int row)> moves = new List<(int col, int row)>();

                for (int c = 0; c < board.Columns; c++) {
                    int r = board.GetAvailableRow(c);
                    if (r != -1) {
                        moves.Add((c, r));
                    }
                }

                (int col, int row) = moves[rand.Next(moves.Count)];

                board.Pieces.Add(new Piece(col, row, board.Pieces.Count, player));
            }
        }

        private void Backpropagate(Node node, double result) {
            while (node != null) {
                node.visits++;
                node.wins += result;
                node = node.parent;
            }
        }
    }
}
