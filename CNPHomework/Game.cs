using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPHomework
{
    internal class Game
    {
        private static int maxPlayer = 2;
        private static int maxFlagNumber = 10;
        private Flag[] flags = new Flag[maxFlagNumber];


    }
    class Flag
    {
        private int id { get; set; }

        private int anchorPointX;
        private int anchorPointY;

        private Coordinate[,] flagAreaCoordinates;

        public Flag(int anchorPointX, int anchorPointY)
        {
            this.anchorPointX = anchorPointX;
            this.anchorPointY = anchorPointY;

            Coordinate startPoint = new Coordinate(anchorPointX - 50, anchorPointY + 50);
            
            flagAreaCoordinates = new Coordinate[101,101];
            for (int i = 0; i < flagAreaCoordinates.GetLength(0); i++)
            {
                for (int j = 0; j < flagAreaCoordinates.GetLength(1); j++)
                {
                    flagAreaCoordinates[i, j] = new Coordinate( startPoint.getX() + i, startPoint.getY() + j);
                }
            }
        }

        public override string ToString()
        {
            return anchorPointX.ToString() + ", " + anchorPointY.ToString();
        }
    }

    class Coordinate
    {
        private int x;
        private int y;
        public int getX() { return x; }
        public void setX(int x)
        {
            this.x = x;
        }
        public int getY() { return y; } 
        public void setY(int y)
        {
            this.x = y;
        }


        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
