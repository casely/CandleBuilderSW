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
    /// </summary>2
    /// 
    public static class CandleCreator
    {
        private const int _firstConstraint = 8;
        private const double _draftAngle = 1.745;
        private const double _distanceExtrusion = 0.002;
        private const double _dAngle = 1.1;

        #region CreateCarving
        /// <summary>
        /// Создание резьбовой части
        /// </summary>
        /// 
        
        public static void CreateCarving(double carvingLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, double pitchSize, double carvingRadius, double electrodeLength, string pathName)
        {
            var helixRevolution = 12;
            var sidesOfPolygon = 3;

            string modelName = pathName + "Резьба.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, carvingLength, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            
            // Создание спирали
            swModel.Extension.SelectByID2("", "FACE", 0, 0, carvingLength, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius);
            swModel.InsertHelix(true, true, false, false, 0, carvingLength, pitchSize, helixRevolution, 0, 0);
            
            // Создание треугольника для выреза резьбы
            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(carvingRadius, -carvingLength - 0.001, 0, carvingRadius - 0.0008, -carvingLength - 0.001, 0, sidesOfPolygon, false);
            swModel.ClearSelection2(true);

            // Выполнение операции "вырезать по направлению"
            swModel.Extension.SelectByID2("Эскиз3", "SKETCH", 0, 0, 0, true, 0, null, 0);
            swModel.Extension.SelectByID2("Спираль1", "REFERENCECURVES", 0, 0, 0, true, 0, null, 0);
            swModel.FeatureManager.InsertCutSwept4(false, true, 0, false, false, 0, 0, false, 0, 0, 0, 0, true, true, 0, true, true, true, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, carvingLength, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, _distanceExtrusion, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            
            // Вырез под электрод
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, carvingRadius - _distanceExtrusion);
            swModel.FeatureManager.FeatureCut3(true, false, true, 0, 0, 0.06, 0.06, false, false, false, false, 0.02, 0.02, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.002);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, carvingLength + 0.003, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
           
            swModel.Extension.SelectByID2("", "FACE", 0, 0, carvingLength + 0.003, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.001);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, electrodeLength, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);

            // Создание второго электрода
            swModel.Extension.SelectByID2("Справа", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, 0.001, 0, 0, 0, 0);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, 0, 0, 0, 0, 0);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCornerRectangle(-carvingLength - 0.008, 0.001, 0, -carvingLength - 0.007, -0.001, 0);
            swModel.SketchManager.InsertSketch(true);

            swModel.Extension.SelectByID2("Плоскость6", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.Create3PointArc(-0.0038, -carvingLength - _distanceExtrusion, 0, 0.001, -carvingLength - 0.007, 0, -0.004, -carvingLength - 0.003, 0);
            swModel.SketchManager.InsertSketch(true);

            swModel.Extension.SelectByID2("Эскиз8", "SKETCH", 0, 0, 0, true, 0, null, 0);
            swModel.Extension.SelectByID2("Эскиз9", "SKETCH", 0, 0, 0, true, 0, null, 0);

            swModel.FeatureManager.InsertProtrusionSwept3(false, false, 0, false, false, 0, 0, false, 0, 0, 0, 0, true, true, true, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        #endregion

        #region CreateNut
        /// <summary>
        /// Создание гайки
        /// </summary>
        public static void CreateNut(double nutLength, double nutSize, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, double chamferRadius, string pathName)
        {
            var dAng2 = 0.45;

            string modelName = pathName + "Болт.sldprt";
            Array vSkLines = null;
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            // Шайба с фаской 1
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, _distanceExtrusion, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);
            
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, _distanceExtrusion, 0, 0, 0, 0);
            swModel.ClearSelection2(true);
            
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.006);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, _distanceExtrusion, _distanceExtrusion, true, false, false, false, 0.30, 0.30, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
            // Гайка
            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            vSkLines = swModel.SketchManager.CreatePolygon(0, 0, 0, 0, CandleMathHelper.GetPolygonRadius(nutSize), 0, 6, true);
            double a = CandleMathHelper.GetPolygonRadius(nutSize);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, nutLength - 0.003, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            
            // Фаска гайки 1
            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, 0.001, 0.001, true, false, false, false, _dAngle, _dAngle, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
            // Фаска гайки 2
            swModel.Extension.SelectByID2("", "FACE", 0, 0, nutLength, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, 0.001, 0.001, true, false, false, false, _dAngle, _dAngle, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            // Шайба с фаской 2
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, nutLength - 0.001, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, chamferRadius);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, 0.001, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, nutLength, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость6", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.006);
            swModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, 0.001, 0.001, true, false, false, false, dAng2, dAng2, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            
            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        #endregion

        #region CreateIsolator
        /// <summary>
        /// Создание изолятора
        /// </summary>
        public static void CreateIsolator(double isolatorLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, string pathName)
        {
            var isolatorDist = 0.01;

            string modelName = pathName + "Изолятор.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.008);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, isolatorLength, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);

            // Фаска 
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.006);
            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, isolatorDist, isolatorDist, true, false, false, false, _dAngle, _dAngle, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.Extension.SelectByID2("", "FACE", 0, 0, 0, false, 0, null, 0);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        #endregion

        #region CreatePlinth
        /// <summary>
        /// Создание гофрированного цоколя
        /// </summary>
        public static void CreatePlinth(double plinthLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, string textEtching, string pathName)
        {
            var angleExtrusion = 2 * Math.PI;
            var polX = 0.005;
            var textWidth = 40;
            var textHeight = 100;

            string modelName = pathName  + "Гофра.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            // Создание изолятора
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, polX);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, plinthLength, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCenterLine(0, 0, 0, 0, -plinthLength, 0);
            swModel.SketchManager.InsertSketch(true);

            // Вырез под гофру
            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(polX + 0.002, 0, 0, polX - 0.001, 0, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, angleExtrusion, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(polX + 0.002, -0.002, 0, polX - 0.001, -_distanceExtrusion, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, angleExtrusion, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(polX + 0.002, -0.004, 0, polX - 0.001, -0.004, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, angleExtrusion, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(polX + 0.002, -0.006, 0, polX - 0.001, -0.006, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, angleExtrusion, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(polX + 0.002, -0.008, 0, polX - 0.001, -0.008, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, angleExtrusion, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreatePolygon(polX + 0.002, -0.01, 0, polX - 0.001, -0.01, 0, 3, false);
            swModel.Extension.SelectByID2("Line1@Эскиз2", "EXTSKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            swModel.FeatureManager.FeatureRevolve2(true, true, false, true, false, false, 0, 0, angleExtrusion, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);
            swModel.ClearSelection2(true);

            // Создание текста

            swModel.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, 0.006, 0, 0, 0, 0);

            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.InsertSketchText(-0.003, -plinthLength + 0.002, 0, textEtching, 0, 0, 0, textWidth, textHeight);
            
            swModel.SketchManager.InsertSketch(true);
            swModel.Extension.SelectByID2("Эскиз9", "SKETCH", 0, 0, 0, false, 4, null, 0);
            swModel.Extension.SelectByID2("", "FACE", 0.005, 0.0007, 0.02, true, 1, null, 0);
            swModel.FeatureManager.InsertWrapFeature(1, 0.001, false);
            //swModel.FeatureManager.FeatureCut3(true, false, true, 0, 0, 0.05, 0.05, false, false, false, false, 0.017, 0.017, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        #endregion

        #region CreateHead
        /// <summary>
        /// Создание головки
        /// </summary>
        public static void CreateHead(double headLength, SldWorks swApp, ModelDoc2 swModel, List<string> detailNames, string pathName)
        {
            var cutDistance = 0.003;
            var dAng = 0.001;
            var dAng2 = 0.35;
            var dAng3 = 0.02;

            string modelName = pathName + "Головка.sldprt";
            swApp.NewPart();
            swModel = swApp.IActiveDoc2;

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, cutDistance);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, headLength / 3, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, headLength / 3, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, _distanceExtrusion);

            swModel.FeatureManager.FeatureCut3(true, true, true, 0, 0, cutDistance, cutDistance, true, false, false, false, dAng2, dAng2, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            swModel.Extension.SelectByID2("Плоскость4", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, cutDistance);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, headLength / 6, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, headLength / 2, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, _distanceExtrusion);
            swModel.FeatureManager.FeatureCut3(true, true, false, 0, 0, cutDistance, cutDistance, true, false, false, false, dAng2, dAng2, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость5", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, _distanceExtrusion);
            swModel.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0, headLength / 2, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);

            // Вырез по центру
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.001);
            swModel.FeatureManager.FeatureCut3(true, false, true, 0, 0, 0.06, 0.06, false, false, false, false, dAng3, dAng3, false, false, false, false, false, true, true, true, true, false, 0, 0, false);

            // Создание гайки для головки
            swModel.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.InsertRefPlane(_firstConstraint, headLength, 0, 0, 0, 0);
            swModel.ClearSelection2(true);

            swModel.Extension.SelectByID2("Плоскость6", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.SketchManager.CreateCircleByRadius(0, 0, 0, 0.004);
            swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.FeatureManager.FeatureExtrusion2(true, false, true, 0, 0, dAng, 0, false, false, false, false, _draftAngle, _draftAngle, false, false, false, false, true, true, true, 0, 0, false);
            swModel.ClearSelection();

            swModel.SaveAs(modelName);
            detailNames.Add(modelName);
        }

        #endregion

    }
}
