using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Connect4;
using System.Threading;

namespace Connect4Tests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void CheckFinish_PlayerWins()
        {
            Board _board = new Board();
            bool finished;
            PlayerType playerType;

            Piece newPiece = new Piece(6, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 5, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(1, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 4, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(3, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 3, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(4, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 2, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            _board.CheckFinish(out finished, out playerType);

            Assert.IsTrue(finished && playerType == PlayerType.Human);
        }

        [TestMethod]
        public void CheckFinish_ComputerWins()
        {
            Board _board = new Board();
            bool finished;
            PlayerType playerType;

            Piece newPiece = new Piece(6, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 5, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(5, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 4, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(4, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 3, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(3, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            _board.CheckFinish(out finished, out playerType);

            Assert.IsTrue(finished && playerType == PlayerType.Computer);
        }

        [TestMethod]
        public void CheckFinish_Tie()
        {
            Board board = new Board();
            bool finished;
            PlayerType playerType;

            board.Pieces.Add(new Piece(0, 0, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(1, 0, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(0, 1, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(1, 1, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(0, 2, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(1, 2, board.Pieces.Count, PlayerType.Human));

            board.Pieces.Add(new Piece(0, 3, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(1, 3, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(0, 4, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(1, 4, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(0, 5, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(1, 5, board.Pieces.Count, PlayerType.Computer));

            board.Pieces.Add(new Piece(2, 0, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(3, 0, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(2, 1, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(3, 1, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(2, 2, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(3, 2, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(2, 3, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(3, 3, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(2, 4, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(3, 4, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(2, 5, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(3, 5, board.Pieces.Count, PlayerType.Computer));

            board.Pieces.Add(new Piece(4, 0, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(5, 0, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(4, 1, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(5, 1, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(4, 2, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(5, 2, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(4, 3, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(5, 3, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(4, 4, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(5, 4, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(4, 5, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(5, 5, board.Pieces.Count, PlayerType.Computer));

            board.Pieces.Add(new Piece(6, 0, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(6, 1, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(6, 2, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(6, 3, board.Pieces.Count, PlayerType.Computer));
            board.Pieces.Add(new Piece(6, 4, board.Pieces.Count, PlayerType.Human));
            board.Pieces.Add(new Piece(6, 5, board.Pieces.Count, PlayerType.Human));


            board.CheckFinish(out finished, out playerType);

            Assert.IsTrue(finished && playerType == PlayerType.None);
        }

        [TestMethod]
        public void CheckFinish_GameNotFinished()
        {
            Board _board = new Board();
            bool finished;
            PlayerType playerType;

            Piece newPiece = new Piece(6, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 5, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(5, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 4, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(4, 5, _board.Pieces.Count, PlayerType.Computer);
            _board.Pieces.Add(newPiece);

            newPiece = new Piece(0, 3, _board.Pieces.Count, PlayerType.Human);
            _board.Pieces.Add(newPiece);

            _board.CheckFinish(out finished, out playerType);

            Assert.IsFalse(finished);
        }

        [TestMethod]
        public void GetAvailableRow_RowAvailable()
        {
            Board _board = new Board();

            _board.Pieces.Add(new Piece(6, 5, _board.Pieces.Count, PlayerType.Computer));

            _board.Pieces.Add(new Piece(0, 5, _board.Pieces.Count, PlayerType.Human));

            _board.Pieces.Add(new Piece(5, 5, _board.Pieces.Count, PlayerType.Computer));

            _board.Pieces.Add(new Piece(0, 4, _board.Pieces.Count, PlayerType.Human));

            _board.Pieces.Add(new Piece(4, 5, _board.Pieces.Count, PlayerType.Computer));

            _board.Pieces.Add(new Piece(0, 3, _board.Pieces.Count, PlayerType.Human));

            int row = _board.GetAvailableRow(4);
            int expectedRow = 4;

            Assert.AreEqual(row, expectedRow);
        }

        [TestMethod]
        public void GetAvailableRow_ColumnFull()
        {
            Board _board = new Board();

            _board.Pieces.Add(new Piece(6, 5, _board.Pieces.Count, PlayerType.Computer));

            _board.Pieces.Add(new Piece(0, 5, _board.Pieces.Count, PlayerType.Human));

            _board.Pieces.Add(new Piece(5, 5, _board.Pieces.Count, PlayerType.Computer));

            _board.Pieces.Add(new Piece(0, 4, _board.Pieces.Count, PlayerType.Human));

            _board.Pieces.Add(new Piece(4, 5, _board.Pieces.Count, PlayerType.Computer));

            _board.Pieces.Add(new Piece(0, 3, _board.Pieces.Count, PlayerType.Human));

            _board.Pieces.Add(new Piece(0, 2, _board.Pieces.Count, PlayerType.Computer));

            _board.Pieces.Add(new Piece(0, 1, _board.Pieces.Count, PlayerType.Human));

            _board.Pieces.Add(new Piece(0, 0, _board.Pieces.Count, PlayerType.Computer));

            int row = _board.GetAvailableRow(0);
            int expectedRow = -1;

            Assert.AreEqual(row, expectedRow);
        }

        [TestMethod]
        public void GetAvailableRow_InvalidColumn()
        {
            Board _board = new Board();

            int row = _board.GetAvailableRow(-5);
            int expectedRow = -1;

            Assert.AreEqual(row, expectedRow);
        }
    }
}
