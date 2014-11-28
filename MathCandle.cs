using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleSW
{
    static class MathCandle
    {

        /// <summary>
        /// Method find polygon radius for nut
        /// </summary>
        public static double GetPolygonRadius(double nutSize)
        {
            double radius = nutSize / 2;
            return (radius / Math.Cos(Math.PI / 6));
        }
    }
}
