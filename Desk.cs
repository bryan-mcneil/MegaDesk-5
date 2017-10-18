using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk_5
{
    class Desk
    {
        //Create SurfaceMaterials enum
        public enum SurfaceMaterials
        {
            Oak = 200,
            Laminate = 100,
            Pine = 50,
            Rosewood = 300,
            Veneer = 125
        }

        //Declare Variables necessary to create a desk object
        //prop + tab + tab

        public int width { get; set; }
        public int depth { get; set; }
        public int drawerCount { get; set; }
        public SurfaceMaterials surface { get; set; }


        //Desk object constructor
        public Desk(int inWidth, int inDepth, int inDrawerCount, SurfaceMaterials inSurface)
        {
            width = inWidth;
            depth = inDepth;
            drawerCount = inDrawerCount;
            surface = inSurface;
        }


        // Calculator for the cost of the desk
        public double CalculateDeskCost()
        {
            double baseCost = 200;
            double drawerCost = this.drawerCount * 50;
            double additionalSizeCost = this.DeskAreaCostCalc();
            double surfaceValue = (double)this.surface;
            double deskCost = baseCost + drawerCost + additionalSizeCost + surfaceValue;

            return deskCost;
        }


        // Calculator for the desk area
        public double DeskAreaCalc()
        {
            int deskArea = this.width * this.depth;
            return deskArea;
        }


        // Calculator for the cost of extended desk area size
        public double DeskAreaCostCalc()
        {
            double sizeCost = this.DeskAreaCalc();
            if (sizeCost > 1000)
            {
                sizeCost = sizeCost - 1000;
            }
            else
            {
                sizeCost = 0;
            }
            return sizeCost;

        }
    }
}
