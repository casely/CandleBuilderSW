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
        /// 
        public static void CreateCarving(double carvingLength)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Изолятор.sldprt";
            CandleBuilder.SwApp.NewPart();
            CandleBuilder.SwModel = CandleBuilder.SwApp.IActiveDoc2;
            CandleBuilder.SwModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            CandleBuilder.SwModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.004);
            CandleBuilder.SwModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            CandleBuilder.SwModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, carvingLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            CandleBuilder.SwModel.SaveAs(modelName);
            CandleBuilder._detailNames.Add(modelName);
        }

        /// <summary>
        /// Create nut static method
        /// </summary>
        public static void CreateNut(double nutLength, double nutSize)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Болт.sldprt";
            Array vSkLines = null;
            CandleBuilder.SwApp.NewPart();
            CandleBuilder.SwModel = CandleBuilder.SwApp.IActiveDoc2;
            CandleBuilder.SwModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            vSkLines = CandleBuilder.SwModel.SketchManager.CreatePolygon(0, 0, 0, 0, MathCandle.GetPolygonRadius(nutSize), 0, 6, true);
            CandleBuilder.SwModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, nutLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            // Фаска 1
            CandleBuilder.SwModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            CandleBuilder.SkSegment = CandleBuilder.SwModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.008);
            CandleBuilder.SwModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            // Фаска 2
            CandleBuilder.SwModel.Extension.SelectByID2("", "FACE", 0, 0, nutLength, false, 0, null, 0);
            CandleBuilder.SkSegment = CandleBuilder.SwModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.008);
            CandleBuilder.SwModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
            CandleBuilder.SwModel.SaveAs(modelName);
            CandleBuilder._detailNames.Add(modelName);
        }
    }
}
