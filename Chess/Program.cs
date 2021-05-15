using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess("rnbqkbnr/pppppppp/8/8/8/8/11P1PP1P/RNBQKBNR w KQkq - 0 1");
            while (true)
            {
                Console.WriteLine(chess.fen);
                //Print(ChessToAscii(chess));
                Console.WriteLine(chess.IsCheck() ? "Check" : null);
                foreach (var item in chess.GetAllMoves())
                {
                    Console.WriteLine(item + "\t");
                }
                Console.Write("> ");
                string move = Console.ReadLine();
                if (move == "") break;
                chess = chess.Move(move);
            }
        }

        //static string ChessToAscii(Chess chess)
        //{
        //    string text= "  +-----------------+\n";
        //    for (int y = 7; y >= 0; y--)
        //    {
        //        text += y + 1;
        //        text += " | ";
        //        for (int x = 0; x < 8; x++)
        //        {
        //            text += chess.GetFigureAt(x, y) + " ";
        //        }
        //        text += "|\n";
        //    }
        //    text += "  +-----------------+\n";
        //    text += "    a b c d e f g h\n";
        //    return text;
        //}

        //static void Print(string text)
        //{
        //    ConsoleColor oldForeColor = Console.ForegroundColor;
        //    foreach (var x in text)
        //    {
        //        if (x >= 'a' && x <= 'z')
        //            Console.ForegroundColor = ConsoleColor.Red;
        //        else if (x >= 'A' && x <= 'Z')
        //            Console.ForegroundColor = ConsoleColor.White;
        //        else
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //        Console.Write(x);
        //    }
        //    Console.ForegroundColor = oldForeColor;
        //}
    }
}
