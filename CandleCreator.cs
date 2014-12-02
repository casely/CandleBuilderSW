using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;

namespace CandleSW
{
    
    /// <summary>
    /// Статический класс для создания частей детали
    /// </summary>
    /// 
    public static class CandleCreator
    {
        
        /// <summary>
        /// Создание резьбовой части
        /// </summary>
        public static void CreateCarving(double carvingLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Резьба.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.004);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, carvingLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            // Фаска 1
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.003);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        /// <summary>
        /// Создание гайки
        /// </summary>
        public static void CreateNut(double nutLength, double nutSize, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, double chamferRadius)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Болт.sldprt";
            Array vSkLines = null;
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            vSkLines = swModel.SketchManager.CreatePolygon(0, 0, 0, 0, MathCandle.GetPolygonRadius(nutSize), 0, 6, true);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, nutLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            // Фаска 1
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            // Фаска 2
            swModel.Extension.SelectByID2("", "FACE", 0, 0, nutLength, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        /// <summary>
        /// Создание изолятора
        /// </summary>
        public static void CreateIsolator(double isolatorLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Изолятор.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.005);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, isolatorLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            // Фаска 
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.004);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        /// <summary>
        /// Создание гофрированного цоколя
        /// </summary>
        public static void CreatePlinth(double plinthLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Гофра.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.004);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, plinthLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        public static void CreateHead(double headLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Головка.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.003);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, 0.001, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            swModel.Extension.SelectByID2("", "FACE", 0, 0, 0.001, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.002);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, headLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }
    }
}
