using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using System.Diagnostics;
using System.Windows.Forms;

namespace CandleSW
{
    /// <summary>
    /// Класс - отрисовщик детали
    /// </summary>
    class CandleBuilder
    {
        #region Fields
        /// <summary>
        /// Ссылка на интерфейс SldWorks
        /// </summary>
        public SldWorks SwApp;

        /// <summary>
        /// Ссылка на модель
        /// </summary>
        public ModelDoc2 SwModel;

        /// <summary>
        /// Список названий деталей
        /// </summary>
        private List<string> _detailNames = new List<string>();

        /// <summary>
        /// Создание экземпляра класса параметров
        /// </summary>
        private CandleParametrs _parametr = new CandleParametrs();
       
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CandleBuilder()
        {
            /// <summary>
            /// Запуск SolidWorks
            /// </summary>
            SwApp = new SldWorks();
            SwApp.Visible = true;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Метод построения детали
        /// </summary>
        public void BuildCandle(CandleParametrs objParametr)
        {
            /// <summary>
            /// Экземпляру присваиваем значение объекта класса
            /// </summary>
            _parametr = objParametr;

            /// <summary>
            /// Проверка построена ли деталь
            /// </summary>
            _parametr.ExistDetail = true;

            /// <summary>
            /// Присваивание параметров
            /// </summary>
            var carvingLength = _parametr.CarvingLength;
            var nutLength = _parametr.NutLength;
            var nutSize = _parametr.NutSize;
            var isolatorLength = _parametr.IsolatorLength;
            var chamferRadius = _parametr.ChamferRadius;
            var plinthLength = _parametr.PlinthLength;
            var headLength = _parametr.HeadLength;
            var pitchSize = _parametr.PitchSize;
            var carvingRadius = _parametr.CarvingRadius;
            var textEtching = _parametr.TextEtching;
            var electrodeLength = _parametr.ElectrodeLength;

            /// <summary>
            /// Методы класса CandleCreator
            /// </summary>
            if (_parametr.ExistHead == true)
            {
                CandleCreator.CreateHead(headLength, SwApp, SwModel, _detailNames);
            }
            CandleCreator.CreatePlinth(plinthLength, SwApp, SwModel, _detailNames, textEtching);
            CandleCreator.CreateNut(nutLength, nutSize, SwApp, SwModel, _detailNames, chamferRadius);
            CandleCreator.CreateIsolator(isolatorLength, SwApp, SwModel, _detailNames);
            CandleCreator.CreateCarving(carvingLength, SwApp, SwModel, _detailNames, pitchSize, carvingRadius, electrodeLength);

            /// <summary>
            /// Создание сборки
            /// </summary>
            AssemblyDoc swAssembly = SwApp.NewAssembly();
            SwModel = ((ModelDoc2)(SwApp.ActiveDoc));
            if (_parametr.ExistHead == true)
            {
                swAssembly.AddComponent2(_detailNames[0], 0, 0, headLength / 2);
            }
            swAssembly.AddComponent2(_detailNames[1], 0, 0, plinthLength / 2 + headLength);
            swAssembly.AddComponent2(_detailNames[2], 0, 0, nutLength / 2 + headLength + plinthLength);
            swAssembly.AddComponent(_detailNames[3], 0, 0, isolatorLength / 2 + headLength + plinthLength + nutLength);
            swAssembly.AddComponent(_detailNames[4], 0, 0, carvingLength / 2 + headLength + plinthLength + isolatorLength + nutLength);

            /// <summary>
            /// Выбор вида "Изометрия"
            /// </summary>
            SwModel.Extension.SelectByID2("", "FACE", 0, 0, 0, true, 0, null, 0);
            swAssembly.AddMate((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swAlignAGAINST, false, 1, 0);
            SwModel.ShowNamedView("*Изометрия");
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

            /// <summary>
            /// Сохранение сборки
            /// </summary>
            string modelName = "C:\\Users\\dafunk\\Desktop\\Свеча.SLDASM";
            SwModel.SaveAs(modelName);
        }

        #endregion

    }
}
