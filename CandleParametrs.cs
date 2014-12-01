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
            set
            {
                if (value < 10 || value > 15)
                    throw new Exception("Длина резьбовой части не должна быть меньше 10 мм и превышать 15 мм"); 
                else
                    _carvingLength = value; 
            }
        }

        private double _nutLength;
        public double NutLength
        {
            get { return _nutLength / 1000; }
            set
            {
                if (value < 4 || value > 10)
                    throw new Exception("Высота гайки не должна быть меньше 4 мм и превышать 10 мм");
                else
                    _nutLength = value;
            }
        }

        private double _nutSize;
        public double NutSize
        {
            get { return _nutSize / 1000; }
            set { _nutSize = value; }
        }

        private double _isolatorLength;
        public double IsolatorLength
        {
            get { return _isolatorLength / 1000; }
            set
            {
                if (value < 7 || value > 15)
                    throw new Exception("Длина изолятора не должна быть меньше 7 мм и превышать 15 мм");
                else
                    _isolatorLength = value;
            }
        }
    }
}
