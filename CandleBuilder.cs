﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using System.Diagnostics;


namespace CandleSW
{

    /// <summary>
    /// Класс - отрисовщик детали
    /// </summary>
    class CandleBuilder
    {

        public SldWorks SwApp;
        public ModelDoc2 SwModel;

        public List<string> _detailNames = new List<string>();
        private CandleParametrs _parametr = new CandleParametrs();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CandleBuilder()
        {
            SwApp = new SldWorks();
            SwApp.Visible = true;
        }

        /// <summary>
        /// Метод построения детали
        /// </summary>
        public void BuildCandle(CandleParametrs objParametr)
        {
            _parametr = objParametr;
            double _carvingLength = _parametr.CarvingLength;
            double _nutLength = _parametr.NutLength;
            double _nutSize = _parametr.NutSize;
            double _isolatorLength = _parametr.IsolatorLength;
            double _chamferRadius = _parametr.ChamferRadius;

            /// <summary>
            /// Методы класса CandleCreator
            /// </summary>
            CandleCreator.CreateCarving(_carvingLength, SwApp, SwModel, _detailNames);
            CandleCreator.CreateNut(_nutLength, _nutSize, SwApp, SwModel, _detailNames, _chamferRadius);
            CandleCreator.CreateIsolator(_isolatorLength, SwApp, SwModel, _detailNames);

            /// <summary>
            /// Создание сборки
            /// </summary>
            AssemblyDoc swAssembly = SwApp.NewAssembly();
            SwModel = ((ModelDoc2)(SwApp.ActiveDoc));
            swAssembly.AddComponent2(_detailNames[2], 0, 0, _nutLength);
            swAssembly.AddComponent2(_detailNames[0], 0, 0, _nutLength + _isolatorLength);
            swAssembly.AddComponent2(_detailNames[1], 0, 0, 0);
            SwModel.Extension.SelectByID2("", "FACE", 0, 0, 0, true, 0, null, 0);
            swAssembly.AddMate((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swAlignAGAINST, false, 1, 0);
            SwModel.ShowNamedView("*Изометрия");
            SwModel.ViewZoomToSelection();
            SwModel.ClearSelection();
            SwModel.EditRebuild3();

            /// <summary>
            /// Закрытие созданных документов
            /// </summary>
            SwApp.CloseDoc(_detailNames[0]);
            SwApp.CloseDoc(_detailNames[1]);
            SwApp.CloseDoc(_detailNames[2]);

        }
    }
}
