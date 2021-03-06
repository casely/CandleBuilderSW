﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleSW
{
    /// <summary>
    /// Класс хранящий параметры детали
    /// </summary>
    public class CandleParametrs
    {
        /// <summary>
        /// Перечисление параметров
        /// </summary>
        public enum Params
        {
            HeadLength,
            PlinthLength,
            NutLength,
            IsolatorLength,
            CarvingLength,
            ElectrodeLength,
            NutSize
        };

        /// <summary>
        /// Наличие головки
        /// </summary>
        public bool ExistHead = true;

        /// <summary>
        /// Наличие детали
        /// </summary>
        public bool ExistDetail = false;

        /// <summary>
        /// Длина резьбовой части
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

        /// <summary>
        /// Размер шага резьбы
        /// </summary>
        private double _pitchSize;
        public double PitchSize
        {
            get { return _pitchSize / 1000; }
            set { _pitchSize = value; }
        }

        /// <summary>
        /// Размер шага резьбы
        /// </summary>
        private double _carvingRadius;
        public double CarvingRadius
        {
            get { return _carvingRadius / 1000; }
            set { _carvingRadius = value; }
        }

        /// <summary>
        /// Длина гайки части
        /// </summary>
        private double _nutLength;
        public double NutLength
        {
            get { return _nutLength / 1000; }
            set
            {
                if (value < 8 || value > 13)
                    throw new Exception("Высота гайки не должна быть меньше 8 мм и превышать 13 мм");
                else
                    _nutLength = value;
            }
        }

        /// <summary>
        /// Радиус окружности под фаску
        /// </summary>
        private double _chamferRadius;
        public double ChamferRadius
        {
            get { return _chamferRadius; }
            set { _chamferRadius = value; }
        }

        /// <summary>
        /// Размер гайки
        /// </summary>
        private double _nutSize;
        public double NutSize
        {
            get { return _nutSize / 1000; }
            set { _nutSize = value; }
        }

        /// <summary>
        /// Длина изолятора
        /// </summary>
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

        /// <summary>
        /// Длина гофрированного цоколя
        /// </summary>
        private double _plinthLength;
        public double PlinthLength
        {
            get { return _plinthLength / 1000; }
            set
            {
                if (value < 25 || value > 35)
                    throw new Exception("Длина гофрированного цоколя не должна быть меньше 25 мм и превышать 35 мм");
                else
                    _plinthLength = value;
            }
        }

        /// <summary>
        /// Длина головки
        /// </summary>
        private double _headLength;
        public double HeadLength
        {
            get
            {
                return _headLength / 1000;
            }
            set
            {
                if (value < 8 || value > 11)
                    throw new Exception("Длина контактной головки не должна быть меньше 8 мм и превышать 11 мм");
                else
                    _headLength = value;
            }
        }

        /// <summary>
        /// Текст гравировки
        /// </summary>
        private string _textEtching;
        public string TextEtching
        {
            get
            {
                return _textEtching;
            }
            set
            {

                _textEtching = value;
            }
        }

        /// <summary>
        /// Длина электрода
        /// </summary>
        private double _electrodeLength;
        public double ElectrodeLength
        {
            get
            {
                return _electrodeLength / 1000;
            }
            set
            {
                if (value < 1 || value > 3)
                    throw new Exception("Длина электрода не должна быть меньше 1 мм и превышать 3 мм");
                else
                    _electrodeLength = value;
            }
        }

    }
}
