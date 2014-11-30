using System;
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
    class CandleBuilder
    {

        public static SldWorks SwApp;
        public static ModelDoc2 SwModel;
        public static SketchSegment SkSegment;

        public static List<string> _detailNames = new List<string>();
        private CandleParametrs _parametr = new CandleParametrs();


        /// <summary>
        /// Class constructor
        /// </summary>
        public CandleBuilder()
        {
            SwApp = new SldWorks();
            SwApp.Visible = true;
        }

        /// <summary>
        /// Candle builder method
        /// </summary>
        public void BuildCandle(CandleParametrs objParametr)
        {
            _parametr = objParametr;
            double _carvingLength = _parametr.CarvingLength;
            double _nutLength = _parametr.NutLength;
            double _nutSize = _parametr.NutSize;

            /// <summary>
            /// Call static carving method
            /// </summary>
            CandleCreator.CreateCarving(_carvingLength);
            CandleCreator.CreateNut(_nutLength, _nutSize);


            AssemblyDoc swAssembly = SwApp.NewAssembly();
            SwModel = ((ModelDoc2)(SwApp.ActiveDoc));
            Component2 CompBolt = swAssembly.AddComponent4(_detailNames[0], "Default", 0, 0, 0);
            Component2 CompShayba = swAssembly.AddComponent4(_detailNames[1], "Default", 0, 0, 0);
            SwModel.Extension.SelectByID2("", "FACE", 0, 0, 0, true, 0, null, 0);
            swAssembly.AddMate((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swAlignAGAINST, false, 1, 0);
            SwModel.ShowNamedView("*Изометрия");
            SwModel.ViewZoomToSelection();
            SwModel.ClearSelection();
            SwModel.EditRebuild3();

            SwApp.CloseDoc(_detailNames[0]);
            SwApp.CloseDoc(_detailNames[1]);

        }
    }
}
