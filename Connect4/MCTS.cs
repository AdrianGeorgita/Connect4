using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MCTSTests")]

namespace Connect4 {
    /// <summary>
    /// Implementare a algoritmului Monte Carlo Tree Search pentru jocul Connect4
    /// </summary>
    /// <remarks><para>
    /// Algoritmul utilizează o copie a tablei de joc trimisă ca parametru în constructor
    /// </para><para>
    /// Mutările jucătorului sunt transmise prin metoda <see cref="OpponentMove"/> <br/>
    /// Mutările jucătorului sunt realizate pe tabla de joc a algoritmului
    /// </para><para>
    /// Murătile calculatorului sunt returnate de metoda <see cref="Run"/> <br/>
    /// Mutările calculatorului ar trebui realizate pe tabla de joc a jucătorului
    /// </para></remarks>
    internal class MCTS {
        /// <summary>
        /// Constantă utilizată pentru calcului UTC
        /// </summary>
        private readonly double C = Math.Sqrt(2);
        readonly Random rand = new Random();

        /// <summary>
        /// Nodul rădăcină al arborelui
        /// </summary>
        /// <seealso cref="Node"/>
        private Node root;

        /// <summary>
        /// Constructor pentru clasa MCTS
        /// </summary>
        /// <param name="board">Tabla de joc</param>
        /// <exception cref="ArgumentNullException">Parametrul <paramref name="board"/> este null</exception>
        /// <remarks>
        /// Tabla de joc trimisă prin parametrul <paramref name="board"/> poate avea orice configurație chiar dacă aceasta nu reprezintă un joc valid.
        /// Următorul jucător care trebuie să realizeze o mutare va fi calculatorul.
        /// </remarks>
        /// <seealso cref="Board"/>
        public MCTS(Board board) {
            if (board == null) {
                throw new ArgumentNullException("board");
            }

            root = new Node(null, board, -1, PlayerType.Human);
        }

        /// <summary>
        /// Rulează algoritmul MCTS pentru un număr de iterații dat
        /// </summary>
        /// <param name="iterations">Numărul de iterații</param>
        /// <returns>Numărul coloanei pe care mută calculatorul o piesă sau <c>-1</c> dacă nu există mutări valide pe tabla de joc</returns>
        /// <exception cref="ArgumentException">Valoarea parametrului <paramref name="iterations"/> este mai mică decâ <c>1</c></exception>
        /// <exception cref="SystemException">Metoda a fost apelată înainte să realizeze jucătorul o mișcare</exception>
        /// <remarks><para>
        /// Poate fi apelată doar o singură dată după care este necesară o mutare a jucătorului
        /// </para><para>
        /// La final se alege mutarea noduli cu cele mai multe vizite. Dacă numărul de vizite este egal pentru mai multe noduri se alege un nod aleatoriu.
        /// </para></remarks>
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

        /// <summary>
        /// Metodă utilizată pentru a transmite mutarea jucătorului
        /// </summary>
        /// <param name="column">Coloana în care se adaugă o piesă a jucătorului</param>
        /// <exception cref="ArgumentException">Parametrul <paramref name="column"/> nu reprezintă o coloană vlaidă</exception>
        /// <exception cref="SystemException">Metoda a fost apelată înainte să realizeze calculatorul o mișcare</exception>
        /// <exception cref="SystemException">Parametrul <paramref name="column"/> reprezintă o coloană validă dar nu se poate adăuga o piesă pe coloana respectivă</exception>
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

        /// <summary>
        /// Calculează valoarea <c>UCT</c> (Upper Confidence Bound for Trees) pentru un nod dat utilizată în alegerea nodurilor
        /// </summary>
        /// <param name="node">Nodul pentru care se calculează valoarea</param>
        /// <returns>Valoarea calculată pentru nod</returns>
        /// <remarks>Dacă numărul de vizite ale nodului este <c>0</c> valoare <c>UCT</c> a nodului va fi <c>double.MaxValue</c></remarks>
        /// <seealso cref="Node"/>
        private double UCT(Node node) {
            if (node.visits == 0) {
                return double.MaxValue;
            }

            return node.wins / node.visits + C * Math.Sqrt(Math.Log(node.parent.visits) / node.visits);
        }

        /// <summary>
        /// Primul pas al algoritmului MCTS: Selectarea nosului cu valoare <c>UCT</c> maximă
        /// </summary>
        /// <returns>Nodul selectat</returns>
        /// <remarks>
        /// Dacă valoarea <c>UCT</c> este egală pentru mai multe noduri se alege un nod aleatoriu.
        /// </remarks>
        /// <seealso cref="Node"/>
        private Node Select() {
            Node node = root;

            while (node.children.Count > 0) {
                node = node.children.Where(n => UCT(n) == node.children.Max(a => UCT(a))).OrderBy(_ => rand.Next()).First();
            }

            return node;
        }

        /// <summary>
        /// Al doilea pas al algoritmului MCTS: Expansiunea unui nod cu următorul set de mutări posibile
        /// </summary>
        /// <param name="node">Nodul petnru care se realizează expansiunea</param>
        /// <returns>Nodul pentru care se realizează următorul pas</returns>
        /// <remarks>
        /// Dacă nu există mutări sau jocul s-a terminal se returnează nocul primit ca parametru
        /// </remarks>
        /// <seealso cref="Node"/>"/>
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

        /// <summary>
        /// Al treilea pas al algoritmului MCTS: Simularea unui joc până la final utilizînd mutări aleatorii
        /// </summary>
        /// <param name="node">Nocul pentru care se realizează simularea</param>
        /// <returns>
        /// Scorul obținut în urma simulării: <br/>
        ///  - <c>1.0</c> Victorie <br/>
        ///  - <c>0.5</c> Remiză <br/>
        ///  - <c>0.0</c> Înfrângere
        /// </returns>
        /// <seealso cref="Node"/>
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

        /// <summary>
        /// Al patrulea pas al algoritmului MCTS: Backpropagarea rezultatului simulării și actualizarea numărului de vizite al nodurilor din arbore
        /// </summary>
        /// <param name="node">Nodul în care s-a realizat simularea</param>
        /// <param name="result">Rezultatul simulării</param>
        /// <seealso cref="Node"/>
        private void Backpropagate(Node node, double result) {
            while (node != null) {
                node.visits++;
                node.wins += result;
                node = node.parent;
            }
        }
    }
}
