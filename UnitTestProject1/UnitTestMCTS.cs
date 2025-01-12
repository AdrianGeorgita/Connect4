using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Connect4 {
    [TestClass]
    public class UnitTestMCTS {

        /* Verificare parametri pentru contructorul clasei MCTS */

        [TestMethod]
        public void CheckMCTSParameter() {
            try {
                new MCTS(null);
            } catch (ArgumentNullException ex) {
                if (ex.ParamName != "board") {
                    throw ex;
                }
            }
        }

        /* Verificare parameteri pentru metoda Run */

        [TestMethod]
        public void CheckRunParameter() {
            MCTS mcts;

            try {
                mcts = new MCTS(new Board());
                mcts.Run(-1);

                mcts = new MCTS(new Board());
                mcts.Run(0);
            } catch (ArgumentException ex) {
                if (ex.ParamName != "iterations") {
                    throw ex;
                }

            }

            mcts = new MCTS(new Board());
            mcts.Run(1);
        }

        /* Verificare parametri pentru metoda OpponentMove */

        [TestMethod]
        public void CheckOpponentMoveParameter() {
            Board board = new Board();
            MCTS mcts = new MCTS(board);
            mcts.Run(1);

            try {
                mcts.OpponentMove(-1);
                Assert.Fail();
            } catch (ArgumentException ex) {
                if (ex.ParamName != "column") {
                    throw ex;
                }
            }

            try {
                mcts.OpponentMove(board.Columns);
                Assert.Fail();
            } catch (ArgumentException ex) {
                if (ex.ParamName != "column") {
                    throw ex;
                }
            }

            mcts.OpponentMove(3);
        }

        /* Verificarea metodei OpponentMove prin apelarea cu mutări corecte și invalide */

        [TestMethod]
        public void CheckValidOpponentMove() {
            Board board = new Board();

            PlayerType player = (board.Rows % 2 == 0) ? PlayerType.Human : PlayerType.Computer;
            for (int i = board.Rows - 1; i >= 0; i--) {
                player = (player == PlayerType.Computer) ? PlayerType.Human : PlayerType.Computer;
                board.Pieces.Add(new Piece(0, i, board.Pieces.Count, player));
            }

            MCTS mcts = new MCTS(board);
            mcts.Run(1);

            try {
                mcts.OpponentMove(0);
                Assert.Fail();
            } catch (SystemException ex) {
                if (ex.Message != "invalid move") {
                    throw ex;
                }
            }

            mcts.OpponentMove(1);
        }

        /* Verificarea respectării ordinii execuției mutărilor 
         * 
         * Metoda Run poate fi apelată doar după ce jucătorul a plasat o piesă
         * Metoda OpponentMove poate fi apelată doar după ce calculatorul a efectuat o mutare
         */

        [TestMethod]
        public void CheckPlayerTurn() {
            MCTS mcts;

            try {
                mcts = new MCTS(new Board());
                mcts.Run(1);
                mcts.Run(1);
                Assert.Fail();
            } catch (SystemException ex) {
                if (ex.Message != "wrong player turn") {
                    throw ex;
                }
            }

            mcts = new MCTS(new Board());
            mcts.Run(1);
            mcts.OpponentMove(0);

            try {
                mcts.OpponentMove(1);
                Assert.Fail();
            } catch (SystemException ex) {
                if (ex.Message != "wrong player turn") {
                    throw ex;
                }
            }
        }

        /* Verificarea dacă tabal de joc și copia tablei de joc folosite pentru rularea simulărilor sunt identice după realizarea unor mutări 
         *
         * algoritmul utilizează o copie a tablei de joc, realizân mutările jucătorului trimise ca parametru
         */

        [TestMethod]
        public void CheckMCTSBoard() {
            Board board = new Board();
            MCTS mcts = new MCTS(board);

            for (int i = 0; i < 3; i++) {
                // Computer move
                int col = mcts.Run(1);
                int row = board.GetAvailableRow(col);
                board.Pieces.Add(new Piece(col, row, board.Pieces.Count, PlayerType.Computer));

                // Player move
                mcts.OpponentMove(i);
                row = board.GetAvailableRow(i);
                board.Pieces.Add(new Piece(i, row, board.Pieces.Count, PlayerType.Human));
            }

            // Get private property from mcts
            Type type = typeof(MCTS);
            FieldInfo fieldInfo = type.GetField("root", BindingFlags.NonPublic | BindingFlags.Instance);
            Board mctsBoard = ((Node)fieldInfo.GetValue(mcts)).board;

            if (board.Pieces.Count != mctsBoard.Pieces.Count) {
                Assert.Fail("missing pieces from board");
            }

            for (int i = 0; i < board.Pieces.Count; i++) {
                Piece simPiece = mctsBoard.Pieces[i];
                Piece boardPiece = board.Pieces[i];

                if (
                    simPiece.Player != boardPiece.Player ||
                    simPiece.Id != boardPiece.Id ||
                    simPiece.X != boardPiece.X ||
                    simPiece.Y != boardPiece.Y
                 ) {
                    Assert.Fail("pieces do not match");
                }
            }
        }

        [TestMethod]
        public void CheckForFullBoard() {
            Board board = new Board();

            PlayerType swapPlayer(PlayerType p) {
                return (p == PlayerType.Computer) ? PlayerType.Human : PlayerType.Computer;
            }

            // Creating a board with mac pieces
            PlayerType player = PlayerType.Computer;
            for (int i = 0; i < board.Rows; i++) {
                for (int j = 0; j < board.Columns; j++) {
                    if (j % 3 == 0) {
                        player = swapPlayer(player);
                    }

                    board.Pieces.Add(new Piece(j, i, board.Pieces.Count, player));
                }
            }

            MCTS mcts = new MCTS(board);
            if (mcts.Run(1) != -1) {
                Assert.Fail();
            }
        }
    }
}
