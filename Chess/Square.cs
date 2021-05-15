using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public struct Square
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public static IEnumerable<Square> YieldSquares()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    yield return new Square(x, y);
                }
            }
        }

        public static Square none = new Square(-1, -1);

        public Square(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Square(string e2)
        {
            if(e2.Length==2  && 
                e2[0] >= 'a' && e2[0] <= 'h' &&
                e2[1] >= '1' && e2[1]<= '8')
            {
                X = e2[0] - 'a';
                Y = e2[1] - '1';
            }
            else
            {
                this = none;
            }
        }

        public bool OnBoard()
        {
            return X >= 0 && X < 8
                && Y >= 0 && Y < 8;
        }

        public string Name { get { return ((char)('a' + X)).ToString() + (Y + 1).ToString(); } }

        public static bool operator == (Square a, Square b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator != (Square a, Square b)
        {
            return !(a == b);
        }
    }
}
