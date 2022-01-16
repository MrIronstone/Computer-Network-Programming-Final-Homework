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

        private bool isPrepartionPhase = true;
        private bool isGuessingPhase = false;

        Game()
        {
            Player player1 = new Player();
            Player player2 = new Player();
        }

    }
    class Player
    {
        private int id { get; set; }
        private static int maxFlagNumberPerPlayer = 5;
        public int currentFlagNumber = 0;
        private Flag[] flags = new Flag[maxFlagNumberPerPlayer];

        public void SewFlag(int x, int y)
        {
            if(currentFlagNumber < maxFlagNumberPerPlayer)
            {
                Flag newFlag = new Flag(x, y);
                currentFlagNumber += 1;
                flags[currentFlagNumber - 1] = newFlag;
            }
        }

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

            Coordinate startPoint = new Coordinate(anchorPointX - 7, anchorPointY +7);

            flagAreaCoordinates = new Coordinate[15,15];
            for (int i = 0; i < flagAreaCoordinates.GetLength(0); i++)
            {
                for (int j = 0; j < flagAreaCoordinates.GetLength(1); j++)
                {
                    flagAreaCoordinates[i, j] = new Coordinate( startPoint.getX() + i, startPoint.getY() + j);
                }
            }
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
