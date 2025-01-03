using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /// <summary>
    /// Reprezinta o configuratie a jocului (o tabla de joc) la un moment dat
    /// </summary>
    public partial class Board
    {
        public int Rows { get; set; } // numarul de randuri ale tablei de joc
        public int Columns { get; set; } // numarul de coloane ale tablei de joc
        public List<Piece> Pieces { get; set; } // lista de piese, atat ale omului cat si ale calculatorului

        public Board()
        {
            Rows = 6;
            Columns = 7;
            Pieces = new List<Piece>();
        }

        public int GetAvailableRow(int column)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (!Pieces.Any(p => p.X == column && p.Y == row))
                    return row;
            }
            return -1; // nu se mai pot pune piese pe coloana
        }

        public Board(Board b)
        {
            Rows = b.Rows;
            Columns = b.Columns;
            Pieces = new List<Piece>();

            foreach (Piece p in b.Pieces)
                Pieces.Add(new Piece(p.X, p.Y, p.Id, p.Player));
        }

        /// <summary>
        /// Creeaza o noua configuratie aplicand mutarea primita ca parametru in configuratia curenta
        /// </summary>
        public Board MakeMove(Move move)
        {
            Board nextBoard = new Board(this); // copy

            if (move.PieceId != -1)
            {
                nextBoard.Pieces[move.PieceId].X = move.NewX;
                nextBoard.Pieces[move.PieceId].Y = move.NewY;
            }
            else
            {
                Piece newPiece = new Piece(move.NewX, move.NewY, nextBoard.Pieces.Count, PlayerType.Computer);
                nextBoard.Pieces.Add(newPiece);
            }
            return nextBoard;
        }

        /// <summary>
        /// Verifica daca configuratia curenta este castigatoare
        /// </summary>
        /// <param name="finished">Este true daca cineva a castigat si false altfel</param>
        /// <param name="winner">Cine a castigat: omul sau calculatorul</param>
        public void CheckFinish(out bool finished, out PlayerType winner)
        {
            foreach (Piece piece in Pieces)
            {
                if (CheckWinFrom(piece.X, piece.Y, piece.Player))
                {
                    finished = true;
                    winner = piece.Player;
                    return;
                }
            }

            finished = false;
            winner = PlayerType.None;
        }

        private bool CheckWinFrom(int startX, int startY, PlayerType player)
        {
            // verificam toate directiile (orizontala, verticala si cele 2 diagonale
            return CheckDirection(player, startX, startY, 1, 0) ||
                   CheckDirection(player, startX, startY, 0, 1) ||
                   CheckDirection(player, startX, startY, 1, 1) ||
                   CheckDirection(player, startX, startY, 1, -1);
        }

        private bool CheckDirection(PlayerType player, int startX, int startY, int dx, int dy)
        {
            int count = 0;

            for (int i = 0; i < 4; i++)
            {
                int newX = startX + i * dx;
                int newY = startY + i * dy;

                if (newX < 0 || newX >= Columns || newY < 0 || newY >= Rows)
                    break;

                if (Pieces.Any(p => p.X == newX && p.Y == newY && p.Player == player))
                    count++;
                else
                    break;
            }

            return count == 4;
        }
    }
}
