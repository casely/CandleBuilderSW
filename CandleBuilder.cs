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
        public PartDoc SwPart;

        private List<string> _detailNames = new List<string>();
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

            double carvingLength = _parametr.CarvingLength;
            double nutLength = _parametr.NutLength;
            double nutSize = _parametr.NutSize;
            double isolatorLength = _parametr.IsolatorLength;
            double chamferRadius = _parametr.ChamferRadius;
            double plinthLength = _parametr.PlinthLength;
            double headLength = _parametr.HeadLength;
            double pitchSize = _parametr.PitchSize;
            double carvingRadius = _parametr.CarvingRadius;
            string textEtching = _parametr.TextEtching;
            double electrodeLength = _parametr.ElectrodeLength;

            /// <summary>
            /// Методы класса CandleCreator
            /// </summary>
            CandleCreator.CreateCarving(carvingLength, SwApp, SwModel, _detailNames, pitchSize, carvingRadius, electrodeLength);
            CandleCreator.CreateNut(nutLength, nutSize, SwApp, SwModel, _detailNames, chamferRadius);
            CandleCreator.CreateIsolator(isolatorLength, SwApp, SwModel, _detailNames);
            CandleCreator.CreatePlinth(plinthLength, SwApp, SwModel, _detailNames, textEtching);
            
            if (_parametr.ExistHead == true)
            {
                CandleCreator.CreateHead(headLength, SwApp, SwModel, _detailNames);
            }
            
            /// <summary>
            /// Создание сборки
            /// </summary>
            AssemblyDoc swAssembly = SwApp.NewAssembly();
            SwModel = ((ModelDoc2)(SwApp.ActiveDoc));

            swAssembly.AddComponent2(_detailNames[3], 0, 0, plinthLength / 2 + headLength);
            swAssembly.AddComponent(_detailNames[0], 0, 0, carvingLength / 2 + headLength + plinthLength + isolatorLength + nutLength);
            swAssembly.AddComponent(_detailNames[2], 0, 0, isolatorLength / 2 + headLength + plinthLength + nutLength);
            swAssembly.AddComponent2(_detailNames[1], 0, 0, nutLength / 2 + headLength + plinthLength);
            if (_parametr.ExistHead == true)
            {
                swAssembly.AddComponent2(_detailNames[4], 0, 0, headLength / 2);
            }

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
            SwApp.CloseDoc(_detailNames[3]);
            if (_parametr.ExistHead == true)
            {
                SwApp.CloseDoc(_detailNames[4]);
            }
        }
    }
}
