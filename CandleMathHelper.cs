﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleSW
{
    /// <summary>
    /// Класс математических операций
    /// </summary>
    public static class CandleMathHelper
    {
        /// <summary>
        /// Нахождение радиуса для построения шестиугольника
        /// </summary>
        public static double GetPolygonRadius(double nutSize)
        {
            double radius = nutSize / 2;
            return (radius / Math.Cos(Math.PI / 6));
        }
    }
}
