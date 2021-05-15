using ChessModel;
using Prism.Mvvm;

namespace Cheers.Models
{
    public class Cell : BindableBase
    {
        public Figure Figure { get; set; }
        public Square Square { get; set; }
        public int X
        {
            get
            {
                return Square.X * 100;
            }
        }
        public int Y
        {
            get
            {
                return Square.Y * 100;
            }
        }
        public string PathToFigureImage
        {
            get
            {
                return GetPathToPicByFigure(this.Figure);
            }
        }

        public Cell(Square square, Figure figure)
        {
            Square = square;
            Figure = figure;
        }

        private static string GetPathToPicByFigure(Figure figure)
        {
            switch (figure)
            {
                case Figure.blackBishop:
                    return BBishopPath;
                case Figure.blackKing:
                    return BKingPath;
                case Figure.blackKnight:
                    return BKnightPath;
                case Figure.blackPawn:
                    return BPawnPath;
                case Figure.blackQueen:
                    return BQueenPath;
                case Figure.blackRook:
                    return BRockPath;
                case Figure.whiteBishop:
                    return WBishopPath;
                case Figure.whiteKing:
                    return WKingPath;
                case Figure.whiteKnight:
                    return WKnightPath;
                case Figure.whitePawn:
                    return WPawnPath;
                case Figure.whiteQueen:
                    return WQueenPath;
                case Figure.whiteRook:
                    return WRockPath;
                case Figure.none:
                    return "";
                default:
                    throw new System.Exception("Cannot fing a picture");
            }
        }

        private static readonly string PathToFolder =
            System.AppDomain.CurrentDomain.BaseDirectory
            + @"..\..\Pictures\";

        private static readonly string BBishopPath = PathToFolder + "chess_piece_black_bishop_T.png";
        private static readonly string BKingPath = PathToFolder + "chess_piece_black_king_T.png";
        private static readonly string BKnightPath = PathToFolder + "chess_piece_black_knight_T.png";
        private static readonly string BPawnPath = PathToFolder + "chess_piece_black_pawn_T.png";
        private static readonly string BQueenPath = PathToFolder + "chess_piece_black_queen_T.png";
        private static readonly string BRockPath = PathToFolder + "chess_piece_black_rook_T.png";

        private static readonly string WBishopPath = PathToFolder + "chess_piece_white_bishop_T.png";
        private static readonly string WKingPath = PathToFolder + "chess_piece_white_king_T.png";
        private static readonly string WKnightPath = PathToFolder + "chess_piece_white_knight_T.png";
        private static readonly string WPawnPath = PathToFolder + "chess_piece_white_pawn_T.png";
        private static readonly string WQueenPath = PathToFolder + "chess_piece_white_queen_T.png";
        private static readonly string WRockPath = PathToFolder + "chess_piece_white_rook_T.png";
    }
}
