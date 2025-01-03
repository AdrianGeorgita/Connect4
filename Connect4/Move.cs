using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /// <summary>
    /// Reprezinta mutarea unei singure piese
    /// </summary>
    public class Move
    {
        public int PieceId { get; set; } // id-ul piesei mutate
        public int NewX { get; set; } // noua pozitie X
        public int NewY { get; set; } // noua pozitie Y

        public Move(int pieceId, int newX, int newY)
        {
            PieceId = pieceId;
            NewX = newX;
            NewY = newY;
        }
    }
}
