using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandleSW
{
    public static class CandleCreator
    {

        /// <summary>
        /// Create carving static method
        /// </summary>
        public static void CreateCarving(double _carvingLength)
        {
            CandleBuilder.SwApp.NewPart();
            CandleBuilder.SwModel = CandleBuilder.SwApp.IActiveDoc2;
            CandleBuilder.SwModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            CandleBuilder.SwModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.004);
            CandleBuilder.SwModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            CandleBuilder.SwModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, _carvingLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
        }
    }
}
