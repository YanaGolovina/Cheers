using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Board
    {
        public string fen { get; private set; }
        Figure[,] figures;
        public Color moveColor { get; private set; }
        public int moveNumber { get; private set; }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach(Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == moveColor)
                    yield return new FigureOnSquare(GetFigureAt(square), square);
            }
        }

        public Board(string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        void Init()
        {
            string[] parts = fen.Split();
            if (parts.Length != 6) return;
            InitFigures(parts[0]);
            moveColor = (parts[1] == "w") ? Color.white : Color.black; ;
            moveNumber = int.Parse(parts[5]);
        }

        void InitFigures(string data)
        {
            for(int j=8; j>=2; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }
            data = data.Replace("1", ".");
            string[] lines = data.Split('/');
            for (int y = 7; y >= 0; y--)
            {
                for (int x= 0;  x < 8; x++)
                {
                    figures[x, y] = lines[7-y][x] == '.' 
                        ? Figure.none
                        : (Figure)lines[7 - y][x];
                }
            }
        }

        public Figure GetFigureAt(Square square)
        {
            if(square.OnBoard())
            {
                return figures[square.X, square.Y];
            }
            return Figure.none;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if(square.OnBoard())
            {
                figures[square.X, square.Y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(fen);
            next.SetFigureAt(fm.from, Figure.none);
            next.SetFigureAt(fm.to, fm.promotion==Figure.none ? fm.figure : fm.promotion);
            if(moveColor==Color.black)
            {
                next.moveNumber++;
            }
            next.moveColor = moveColor.FlipColor();
            next.GenerateFEN();
            return next;
        }

        void GenerateFEN()
        {
            fen = FenFigure() + " " +
                   (moveColor==Color.white ? "w" : "b") + 
                   " - - 0 " + moveNumber.ToString();
        }

        string FenFigure()
        {
            var sb = new StringBuilder();
            for (int y=7; y>=0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(figures[x, y]==Figure.none ? '1' : (char)figures[x,y]);          
                }
                if (y > 0)
                    sb.Append('/');
            }
            string eight = "11111111";
            for (int j = 8; j >= 2; j--)
            {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }
            return sb.ToString();
        }

        public bool IsCheck()
        {
            Board after = new Board(fen);
            after.moveColor = moveColor.FlipColor();
            return after.CanEatKing();
        }

        private bool CanEatKing()
        {
            var badKing = FindBadKing();
            var moves = new Moves(this);
            foreach (var item in YieldFigures())
            {
                var fm = new FigureMoving(item, badKing);
                if (moves.CanMove(fm))
                    return true;
            }
            return false;
        }

        private Square FindBadKing()
        {
            var badKnig = moveColor == Color.black ? Figure.whiteKing : Figure.blackKing;
            foreach (var item in Square.YieldSquares())
            {
                if (GetFigureAt(item) == badKnig)
                    return item;
            }
            return Square.none;
        }

        public bool IsCheckAfterMove(FigureMoving fm) //невозможный ход
        {
            Board after = Move(fm);
            return after.CanEatKing();
        }
    }
}
