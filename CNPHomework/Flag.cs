using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPHomework
{
    internal class Flag
    {
        //private int size = 101;
        //public int getSize()
        //{
        //    return this.size;
        //}
        //public void setSize(int newSize)
        //{
        //    if(newSize > 0)
        //    {
        //        this.size = newSize;
        //    }
        //    else
        //    {
        //        throw new IndexOutOfRangeException();
        //    }
        //}

        public bool isCaptured = false;
        private readonly int anchorPointX;
        private readonly int anchorPointY;

        // public Coordinate[,] flagAreaCoordinates;

        public int GetX()
        {
            return anchorPointX;
        }

        public int GetY()
        {
            return anchorPointY;
        }

        public Flag(int anchorPointX, int anchorPointY)
        {
            this.anchorPointX = anchorPointX;
            this.anchorPointY = anchorPointY;

            //Coordinate startPoint = new Coordinate(anchorPointX - 50, anchorPointY - 50);
            
            //flagAreaCoordinates = new Coordinate[size, size];
            //for (int i = 0; i < flagAreaCoordinates.GetLength(0); i++)
            //{
            //    for (int j = 0; j < flagAreaCoordinates.GetLength(1); j++)
            //    {
            //        flagAreaCoordinates[i, j] = new Coordinate( startPoint.getX() + i, startPoint.getY() + j);
            //    }
            //}
        }

        /// <summary>
        /// This function determines whether the area of the object given as a parameter intersects with this flag. Returns true if they intersect, false if they do not.
        /// </summary>
        /// <param name="hitArea"> This object is an object that represents the hit area. Flag center is hit center</param>
        /// <returns></returns>
        public bool IsThisAttackInMyArea(Flag hitArea)
        {
            // this method checks if the flag that has been given by paramater
            if(this.anchorPointX - 50 <= hitArea.anchorPointX + 50 && this.anchorPointX + 50 >= hitArea.anchorPointX - 50)
            {
                if(this.anchorPointY - 50 <= hitArea.anchorPointY + 50 && this.anchorPointY + 50 >= hitArea.anchorPointY - 50)
                {
                    isCaptured = true;
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return anchorPointX.ToString() + ", " + anchorPointY.ToString();
        }
    }

    /*
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
    */
}
