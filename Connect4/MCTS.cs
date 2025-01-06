﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4 {
    internal class MCTS {
        private readonly double C = Math.Sqrt(2);
        Random rand = new Random();

        private Node root;

        public MCTS(Board board) {
            root = new Node(null, board, -1, PlayerType.Human);
        }

        public int Run(int iterations) {
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

        public void OponentMove(int column) {
            foreach (Node child in root.children) {
                if (child.column == column) {
                    root = child;
                    root.parent = null;
                    break;
                }
            }
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

            bool finished = false;
            PlayerType winner = PlayerType.None;
            PlayerType player = node.player;

            do {
                player = (player == PlayerType.Computer) ? PlayerType.Human : PlayerType.Computer;

                int a;
                for (a = 0; a < board.Columns; a++) {
                    if (board.GetAvailableRow(a) != -1) {
                        break;
                    }
                }

                if (a == board.Columns) {
                    return 0.5;
                }

                int col = -1;
                int row = -1;

                while (row == -1) {
                    col = rand.Next(board.Columns);
                    row = board.GetAvailableRow(col);
                }

                board.Pieces.Add(new Piece(col, row, board.Pieces.Count, player));

                board.CheckFinish(out finished, out winner);
            } while (!finished);

            if (winner == PlayerType.Computer) {
                return 1;
            }

            return 0;
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
