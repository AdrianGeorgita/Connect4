using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public enum PlayerType { None, Computer, Human };

    /// <summary>
    /// Reprezinta o piesa de joc
    /// </summary>
    public partial class Piece
    {
        public int Id { get; set; } // identificatorul piesei
        public int X { get; set; } // pozitia X pe tabla de joc
        public int Y { get; set; } // pozitia Y pe tabla de joc
        public PlayerType Player { get; set; } // carui tip de jucator apartine piesa (om sau calculator)

        public Piece(int x, int y, int id, PlayerType player)
        {
            X = x;
            Y = y;
            Id = id;
            Player = player;
        }
    }
}
