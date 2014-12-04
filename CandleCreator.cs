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
        public static void CreateCarving(double carvingLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, double pitchSize, double carvingRadius, double electrodeLength)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Резьба.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, carvingLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            
            // Создание спирали
            swModel.Extension.SelectByID2("", "FACE", 0, 0, carvingLength, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius);
            swModel.InsertHelix(true, true, false, false, 0, carvingLength, pitchSize, 12, 0, 0);
            
            // Создание треугольника для выреза резьбы
            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(carvingRadius, -carvingLength - 0.001, 0, carvingRadius - 0.0008, -carvingLength - 0.001, 0, 3, false);
            swModel.ClearSelection2(true);

            // Выполнение операции "вырезать по направлению"
            swModel.Extension.SelectByID2("Эскиз3", "SKETCH", 0, 0, 0, true, 0, null, 0);
            swModel.Extension.SelectByID2("Спираль1", "REFERENCECURVES", 0, 0, 0, true, 0, null, 0);
            swModel.FeatureManager.InsertCutSwept4(false, true, 0, false, false, 0, 0, false, 0, 0, 0, 0, true, true, 0, true, true, true, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, carvingLength, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, 0.002, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            
            // Вырез под электрод
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius - 0.002);
            swModel.FeatureManager.FeatureCut3(true, false, true, 0, 0, 0.06, 0.06, false, false, false, false, 0.02, 0.02, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.002);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, carvingLength + 0.003, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
           
            swModel.Extension.SelectByID2("", "FACE", 0, 0, carvingLength + 0.003, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.001);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, electrodeLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            // Создание второго электрода
            swModel.Extension.SelectByID2("Справа", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, 0.001, 0, 0, 0, 0);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, 0, 0, 0, 0, 0);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCornerRectangle(-carvingLength - 0.008, 0.001, 0, -carvingLength - 0.007, -0.001, 0);
            swModel.SketchManager.InsertSketch(true);

            swModel.Extension.SelectByID2("Плоскость6", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.Create3PointArc(-0.0038, -carvingLength - 0.002, 0, 0.001, -carvingLength - 0.007, 0, -0.004, -carvingLength - 0.003, 0);
            swModel.SketchManager.InsertSketch(true);

            swModel.Extension.SelectByID2("Эскиз8", "SKETCH", 0, 0, 0, true, 0, null, 0);
            swModel.Extension.SelectByID2("Эскиз9", "SKETCH", 0, 0, 0, true, 0, null, 0);

            swModel.FeatureManager.InsertProtrusionSwept3(false, false, 0, false, false, 0, 0, false, 0, 0, 0, 0, true, true, true, 0, false);

            swModel.Extension.SelectByID2("Деталь3.SLDPRT", "COMPONENT", 0, 0, 0, false, 0, null, 0);
            (swModel as IPartDoc).SetMaterialPropertyName2("AddHoles", "solidworks materials.sldmat", "Alloy Steel");

            swModel.Extension.SelectByID2("Деталь3.SLDPRT", "COMPONENT", 0, 0, 0, false, 0, null, 0);
            (swModel as IPartDoc).SetMaterialPropertyName2("AddHoles", "solidworks materials.sldmat", "Alloy Steel");
            swModel.SaveAs(modelName);
            detailNames.Add(modelName);

            

            //var myPart = (PartDoc)swModel;
            
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

            // Шайба с фаской 1
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, 0.002, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);
            
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, 0.002, 0, 0, 0, 0);
            swModel.ClearSelection2(true);
            
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.006);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.002, 0.002, true, false, false, false, 0.30, 0.30, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
            // Гайка
            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            vSkLines = swModel.SketchManager.CreatePolygon(0, 0, 0, 0, MathCandle.GetPolygonRadius(nutSize), 0, 6, true);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, nutLength - 0.003, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            
            // Фаска гайки 1
            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
            // Фаска гайки 2
            swModel.Extension.SelectByID2("", "FACE", 0, 0, nutLength, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, 0.001, 0.001, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            // Шайба с фаской 2
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, nutLength - 0.001, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, 0.001, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, nutLength, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость6", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.006);
            swModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, 0.001, 0.001, true, false, false, false, 0.45, 0.45, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
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
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.008);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, isolatorLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            // Фаска 
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.006);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.01, 0.01, true, false, false, false, 1.1, 1.1, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        /// <summary>
        /// Создание гофрированного цоколя
        /// </summary>
        public static void CreatePlinth(double plinthLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, string textEtching)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Гофра.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            // Создание изолятора
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.005);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, plinthLength, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCenterLine(0, 0, 0, 0, -plinthLength, 0);
            swModel.SketchManager.InsertSketch(true);

            // Вырез под гофру
            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(0.005 + 0.002,      0, 0, 0.005 - 0.001,      0, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, 6.2831853071796004, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(0.005 + 0.002, -0.002, 0, 0.005 - 0.001, -0.002, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, 6.2831853071796004, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(0.005 + 0.002, -0.004, 0, 0.005 - 0.001, -0.004, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, 6.2831853071796004, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(0.005 + 0.002, -0.006, 0, 0.005 - 0.001, -0.006, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, 6.2831853071796004, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(0.005 + 0.002, -0.008, 0, 0.005 - 0.001, -0.008, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, 6.2831853071796004, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(0.005 + 0.002, -0.01, 0, 0.005 - 0.001, -0.01, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, 6.2831853071796004, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            // Создание текста
            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.InsertSketchText(-0.003, -plinthLength + 0.002, 0, textEtching, 0, 0, 0, 40, 100);
            swModel.Extension.SelectByID2("Эскиз9", "SKETCH", 0, 0, 0, true, 0, null, 0);
            swModel.FeatureManager.FeatureCut3(true, false, true, 0, 0, 0.05, 0.05, false, false, false, false, 0.017, 0.017, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        /// <summary>
        /// Создание головки
        /// </summary>
        public static void CreateHead(double headLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames)
        {
            string modelName = "C:\\Users\\dafunk\\Desktop\\Головка.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.003);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, headLength / 3, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, headLength / 3, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.002);

            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.003, 0.003, true, false, false, false, 0.35, 0.35, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.003);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, headLength / 6, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, headLength / 2, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.002);
            swModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, 0.003, 0.003, true, false, false, false, 0.35, 0.35, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.002);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, headLength / 2, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);

            // Вырез по центру
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.001);
            swModel.FeatureManager.FeatureCut3(true, false, true, 0, 0, 0.06, 0.06, false, false, false, false, 0.02, 0.02, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            // Создание гайки для головки
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(8, headLength, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость6", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.004);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, true, 0, 0, 0.001, 0, false, false, false, false, 1.745, 1.745, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection();

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }
    }
}
