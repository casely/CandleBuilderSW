using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleSW
{
    public class CandleParametrs
    {

        /// <summary>
        /// Class with candle parametrs
        /// </summary>
        private double _carvingLength;
        public double CarvingLength
        {
            get { return _carvingLength / 1000; }
            set { _carvingLength = value; }
        }

        private static double _nutLength;
        public double NutLength
        {
            get { return _nutLength / 1000; }
            set { _nutLength = value; }
        }

        private static double _nutSize;
        public double NutSize
        {
            get { return _nutSize / 1000; }
            set { _nutSize = value; }
        }
    }
}
