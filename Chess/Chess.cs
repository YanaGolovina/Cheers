using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Chess
    {
        public string fen { get; private set; }
        public Board Board 
        { 
            get 
            { 
                return board; 
            } 
        }
        Board board;
        Moves moves;
        public List<FigureMoving> Moves
        {
            get
            {
                return allMoves;
            }
        }
        List<FigureMoving> allMoves;

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
            FindAllMoves();
        }

        Chess(Board board) //конструктор
        {
            this.board = board;
            this.fen= board.fen;
            moves = new Moves(board);
        }

        public Chess Move(string move) //Pe2e4
        {
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
                return this;
            if (board.IsCheckAfterMove(fm))
                return this;
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public Chess Move(FigureMoving move)
        {
            if (!moves.CanMove(move))
                return this;
            if (board.IsCheckAfterMove(move))
                return this;
            Board nextBoard = board.Move(move);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x,y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.' : (char)f;
        }

        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach (var fs in board.YieldFigures())
            {
                foreach (var to in Square.YieldSquares())
                {
                    var fm = new FigureMoving(fs, to);
                    if (moves.CanMove(fm))
                        if(!board.IsCheckAfterMove(fm))
                            allMoves.Add(fm);
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            var list = new List<string>();
            foreach (var item in allMoves)
            {
                list.Add(item.ToString());
            }
            return list;
        }

        public bool IsCheck()
        {
            return board.IsCheck();
        }
    }
}
