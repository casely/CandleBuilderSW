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
        public static SketchManager SwSkM;
        private List<string> _detailNames = new List<string>();
        private CandleParametrs _parametr = new CandleParametrs();

        /// <summary>
        /// Class constructor
        /// </summary>
        public CandleBuilder()
        {
            SwApp = new SldWorks();
            SwApp.Visible = true;
            _detailNames.Add("Изолятор.sldprt");
            _detailNames.Add("Болт.sldprt");
        }

        /// <summary>
        /// Candle builder method
        /// </summary>
        public void BuildCandle(CandleParametrs objParametr)
        {
            _parametr = objParametr;
            double _carvingLength = _parametr.CarvingLength;

            /// <summary>
            /// Call static carving method
            /// </summary>
            CandleCreator.CreateCarving(_carvingLength);


        }
    }
}
