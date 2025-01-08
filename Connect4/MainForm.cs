using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class MainForm : Form
    {
        private int MCTS_iterations = 100;

        private Board _board;
        private int _selected; // indexul piesei selectate
        private PlayerType _currentPlayer; // om sau calculator
        private Bitmap _boardImage;

        private MCTS mcts;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                if(File.Exists("board.png"))
                    _boardImage = (Bitmap)Image.FromFile("board.png");
                else
                    _boardImage = (Bitmap)Image.FromFile("../../res/board.png");
            }
            catch
            {
                MessageBox.Show("Nu s-a putut incarca imaginea cu tabla de joc!");
                Environment.Exit(1);
            }

            _board = new Board();
            _currentPlayer = PlayerType.None;
            _selected = -1; // nicio piesa selectata

            this.ClientSize = new System.Drawing.Size(850, 600);
            this.pictureBoxBoard.Size = new System.Drawing.Size(500, 500);

            pictureBoxBoard.Refresh();
        }

        private void pictureBoxBoard_Paint(object sender, PaintEventArgs e)
        {
            Bitmap board = new Bitmap(_boardImage);
            e.Graphics.DrawImage(board, 0, 0);

            if (_board == null)
                return;

            SolidBrush red = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
            SolidBrush yellow = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
            SolidBrush transparentGreen = new SolidBrush(Color.FromArgb(75, 0, 255, 0));

            foreach (Piece p in _board.Pieces)
            {
                SolidBrush brush = red;
                if (p.Player == PlayerType.Human)
                {
                    brush = yellow;
                }
                //MessageBox.Show(p.X + " " + p.Y);
                e.Graphics.FillEllipse(brush, 35 + p.X * 64, -14 + (p.Y + 1) * 64, 50, 50);
            }

            if (_currentPlayer == PlayerType.Human)
            {
                List<Move> validMoves = GetPlayerValidMoves();
                foreach (Move validMove in validMoves)
                {
                    e.Graphics.FillEllipse(transparentGreen, 35 + validMove.NewX * 64, -14 + (validMove.NewY + 1) * 64, 50, 50);
                }
            }
        }

        private List<Move> GetPlayerValidMoves()
        {
            List<Move> validMoves = new List<Move>();

            for (int col = 0; col < _board.Columns; col++)
            {
                int availableRow = _board.GetAvailableRow(col);
                if (availableRow != -1)
                {
                    validMoves.Add(new Move(-1, col, availableRow));
                }
            }

            return validMoves;
        }

        private void pictureBoxBoard_MouseUp(object sender, MouseEventArgs e)
        {
            if (_currentPlayer != PlayerType.Human)
                return;

            int mouseX = (e.X - 29) / 64;

            int availableRow = _board.GetAvailableRow(mouseX);

            if (availableRow == -1)
                return;

            int validCellX = 29 + mouseX * 64;
            int validCellY = (availableRow + 1) * 61;
            Rectangle validCellBounds = new Rectangle(validCellX, validCellY, 60, 60);

            if (validCellBounds.Contains(e.Location))
            {
                Piece newPiece = new Piece(mouseX, availableRow, _board.Pieces.Count, PlayerType.Human);
                _board.Pieces.Add(newPiece);
                pictureBoxBoard.Refresh();

                _currentPlayer = PlayerType.Computer;

                mcts.OpponentMove(mouseX);

                CheckFinish();

                if (_currentPlayer == PlayerType.Computer)
                    ComputerMove();
            }
        }

        private void ComputerMove()
        {
            int col = mcts.Run(MCTS_iterations);

            int row = _board.GetAvailableRow(col);

            if (row != -1) 
            {
                Move move = new Move(-1, col, row);
                _board = _board.MakeMove(move);
                pictureBoxBoard.Refresh();

                _currentPlayer = PlayerType.Human;
                pictureBoxBoard.Refresh();
            }

            CheckFinish();
        }

        private void CheckFinish()
        {
            bool end; PlayerType winner;
            _board.CheckFinish(out end, out winner);

            if (end)
            {
                if (winner == PlayerType.Computer)
                {
                    MessageBox.Show("Calculatorul a castigat!");
                    _currentPlayer = PlayerType.None;
                }
                else if (winner == PlayerType.Human)
                {
                    MessageBox.Show("Ai castigat!");
                    _currentPlayer = PlayerType.None;
                } 
                else if (winner == PlayerType.None) 
                {
                    MessageBox.Show("Remiza");
                    _currentPlayer = PlayerType.None;
                }
            }
        }

        private void jocNouToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            _board = new Board();
            _currentPlayer = PlayerType.Computer;

            mcts = new MCTS(_board);

            ComputerMove();
        }

        private void despreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string copyright =
                "Copyright";

            MessageBox.Show(copyright, "Despre jocul Connect4");
        }

        private void iesireToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }

        private void dificultateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DifficultyForm difficultyForm = new DifficultyForm(MCTS_iterations);
            if (difficultyForm.ShowDialog() == DialogResult.OK)
            {
                MCTS_iterations = Convert.ToInt32(difficultyForm.Tag);
            }
        }
    }
}
