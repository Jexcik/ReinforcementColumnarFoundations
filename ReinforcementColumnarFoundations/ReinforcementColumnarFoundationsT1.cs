using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using ReinforcementColumnarFoundations.Views.Windows;
using ReinforcementColumnarFoundations.Models;
using ReinforcementColumnarFoundations.ViewModels;
using System.Linq;

namespace ReinforcementColumnarFoundations
{
    public class ReinforcementColumnarFoundationsT1 : IExternalCommand
    {

        public Result Execute(UIApplication uiapp, Document doc, List<FamilyInstance> foundationList, ReinforcementColumnarFoundationsViewModel ViewModel)
        {
            View view = doc.ActiveView;

            RebarBarType firstMainBarType = ViewModel.SelectedFirstRebarType;
            double firstMainBarDiam = firstMainBarType.BarDiameter;

            RebarBarType indirectMainBarTapes = ViewModel.SelectedIndirectBarType;
            double inderectMainBarDiam = indirectMainBarTapes.BarDiameter;

            RebarBarType bottomMainBarType = ViewModel.SelectedBottomMainBarType;
            double bottomMaimBarDiam = bottomMainBarType.BarDiameter;

            RebarBarType firstStirrupBarTape = ViewModel.SelectedStirrupBarType;
            double firstStirrupBarDiam = firstStirrupBarTape.BarDiameter;

            RebarBarType lateralBarTapes = ViewModel.SelectedLateralBarType;
            double lateralBarDiam = lateralBarTapes.BarDiameter;

            RebarHookType rebarHookTypeForStirrup = ViewModel.SelectedHookType;

            RebarShape form01 = ViewModel.Form01;
            RebarShape form26 = ViewModel.Form26;
            RebarShape form11 = ViewModel.Form11;
            RebarShape form21 = ViewModel.Form21;
            RebarShape form51 = ViewModel.Form51;

            RebarCoverType scRebarBarCoverType = ViewModel.SelectedOtherCoverType;
            double coverDistance = scRebarBarCoverType.CoverDistance;

            RebarCoverType rebarCoverType = ViewModel.SelectedBottomCoverType;
            double bottomCoverDistance = rebarCoverType.CoverDistance;

            double StepIndirectRebar = ViewModel.stepIndirectRebar / 304.8;
            double StepLateralRebar = ViewModel.stepLateralRebar / 304.8;

            //    int rebarCountX = reinforcementColumnarFoundationsWPF.CountX;
            //    int rebarCountY = reinforcementColumnarFoundationsWPF.CountY;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Армирование фундаментов - Тип 1");
                foreach (FamilyInstance foundation in foundationList)
                {
                    Foundation foundationProperty = new Foundation(doc, foundation);
                    //Задаем защитный слой арматуры других граней фундамента
                    foundation.get_Parameter(BuiltInParameter.CLEAR_COVER_OTHER).Set(scRebarBarCoverType.Id);
                    //Задаем защитный слой арматуры нижней грани
                    foundation.get_Parameter(BuiltInParameter.CLEAR_COVER_BOTTOM).Set(rebarCoverType.Id);

                    XYZ rotateBase_p1 = foundationProperty.FoundationBasePoint;
                    XYZ rotateBase_p2 = new XYZ(rotateBase_p1.X, rotateBase_p1.Y, rotateBase_p1.Z + 1);
                    Line rotateLineBase = Line.CreateBound(rotateBase_p1, rotateBase_p2);


                    Rebar MainRebar_1 = null;
                    try
                    {
                        //Точки для построения кривых стержня
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2 + firstMainBarDiam / 2 + coverDistance, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Y + foundationProperty.ColumnWidth / 2 - firstMainBarDiam / 2 - coverDistance, 6),
                            Math.Round((foundationProperty.FoundationBasePoint.Z - foundationProperty.CoverTop) + foundationProperty.FoundationLength, 6));

                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6),
                            Math.Round(rebar_p1.Y, 6),
                            Math.Round(rebar_p1.Z - foundationProperty.FoundationLength + foundationProperty.CoverTop + bottomCoverDistance + bottomMaimBarDiam + firstMainBarDiam / 2, 6));

                        XYZ rebar_p3 = new XYZ(Math.Round(rebar_p2.X - 300 / 308.4, 6),
                            Math.Round(rebar_p2.Y, 6),
                            Math.Round(rebar_p2.Z, 6));

                        //Кривые стержня
                        List<Curve> mainRebarCurves = new List<Curve>();
                        Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);
                        Curve line2 = Line.CreateBound(rebar_p2, rebar_p3) as Curve;
                        mainRebarCurves.Add(line2);

                        //Создание вертикального арматурного стержня
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form11
                            , firstMainBarType
                            , null
                            , null
                            , foundation
                            , new XYZ(0, 1, 0)
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);


                        ElementTransformUtils.MoveElement(doc, MainRebar_1.Id, new XYZ(0, 2 * (coverDistance + firstMainBarDiam / 2) - foundationProperty.ColumnWidth, 0));
                        ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(1);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_QUANTITY_OF_BARS).Set(5);

                        var elementRotate = ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, 0));
                        ElementTransformUtils.RotateElements(doc, elementRotate, rotateLineBase, 180 * (Math.PI / 180));

                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалость создать Г-образный стержень");
                        return Result.Cancelled;
                    }
                    //Создание вертикальных стержней
                    try
                    {
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - 50 / 304.8, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 + firstMainBarDiam / 2 + coverDistance, 6),
                            Math.Round((foundationProperty.FoundationBasePoint.Z - foundationProperty.CoverTop) + foundationProperty.FoundationLength, 6));

                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6),
                            Math.Round(rebar_p1.Y, 6),
                            Math.Round(rebar_p1.Z - foundationProperty.FoundationLength + foundationProperty.CoverTop + bottomCoverDistance + 2 * bottomMaimBarDiam + firstMainBarDiam / 2, 6));

                        XYZ rebar_p3 = new XYZ(Math.Round(rebar_p2.X, 6),
                            Math.Round(rebar_p2.Y - 300 / 308.4, 6),
                            Math.Round(rebar_p2.Z, 6));

                        //Кривые стержня
                        List<Curve> mainRebarCurves = new List<Curve>();
                        Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);
                        Curve line2 = Line.CreateBound(rebar_p2, rebar_p3) as Curve;
                        mainRebarCurves.Add(line2);

                        //Создание вертикального арматурного стержня
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form11
                            , firstMainBarType
                            , null
                            , null
                            , foundation
                            , new XYZ(1, 0, 0)
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);

                        ElementTransformUtils.MoveElement(doc, MainRebar_1.Id, new XYZ(foundationProperty.ColumnLength / 2 - coverDistance - firstMainBarDiam / 2, 0, 0));
                        ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(3);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_QUANTITY_OF_BARS).Set(3);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set((foundationProperty.ColumnLength - 2 * coverDistance - 100 / 304.8) / 2);

                        var elementRotate = ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, 0));
                        ElementTransformUtils.RotateElements(doc, elementRotate, rotateLineBase, 180 * (Math.PI / 180));

                        //double step = (foundationProperty.ColumnLength - coverDistance * 2 - 100 / 304.8) / 2;
                        //for (int i = 1; i < 3; i++)
                        //{
                        //    step = step * i;
                        //    ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(-step, 0, 0));
                        //}

                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалость создать Г-образный стержень");
                        return Result.Cancelled;
                    }

                    //Боковое армирование подколонника по оси Х
                    try
                    {
                        //Точки для построения кривых стержня
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2 + 25 / 304.8, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 - lateralBarDiam / 2 + coverDistance / 1.000000000000001, 6)
                            , Math.Round((foundationProperty.FoundationBasePoint.Z - foundationProperty.CoverTop) + foundationProperty.FoundationLength, 6));
                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X + foundationProperty.ColumnLength - 50 / 304.8, 6)
                            , Math.Round(rebar_p1.Y, 6)
                            , Math.Round(rebar_p1.Z, 6));

                        //Кривые стержня
                        List<Curve> mainRebarCurves = new List<Curve>();
                        Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);

                        //Создание вертикального арматурного стержня
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form01
                            , lateralBarTapes
                            , null
                            , null
                            , foundation
                            , XYZ.BasisZ
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);

                        ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(3);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_QUANTITY_OF_BARS).Set((int)(foundationProperty.ColumnHeight / StepLateralRebar) + 1);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set(StepLateralRebar);

                        var elementRotate = ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, 0));
                        ElementTransformUtils.RotateElements(doc, elementRotate, rotateLineBase, 180 * (Math.PI / 180));
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалось создать боковое армирование подколонника!");
                        return Result.Cancelled;
                    }

                    //Боковое армирование подколонника по оси Y
                    try
                    {
                        //Точки для построения кривых стержня
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2 + coverDistance - lateralBarDiam / 2, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 + 25 / 304.8, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Z - foundationProperty.CoverTop + foundationProperty.FoundationLength - lateralBarDiam, 6));
                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6)
                            , Math.Round(rebar_p1.Y + foundationProperty.ColumnWidth - 50 / 308.8, 6)
                            , Math.Round(rebar_p1.Z, 6));

                        //Кривые стержня
                        List<Curve> mainRebarCurves = new List<Curve>();
                        Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);

                        //Создание вертикального арматурного стержня
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form01
                            , lateralBarTapes
                            , null
                            , null
                            , foundation
                            , XYZ.BasisZ
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);

                        ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(3);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_QUANTITY_OF_BARS).Set((int)(foundationProperty.ColumnHeight / StepLateralRebar) + 1);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set(StepLateralRebar);

                        var elementRotate = ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, 0));
                        ElementTransformUtils.RotateElements(doc, elementRotate, rotateLineBase, 180 * (Math.PI / 180));
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалось создать боковое армирование подколонника!");
                        return Result.Cancelled;
                    }


                    //Армирование подошвы
                    try
                    {
                        //Точки для построения арматуры подошвы
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.Ledge1Length / 2 + 25 / 304.8, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.Ledge1Width / 2 + coverDistance + bottomMaimBarDiam / 2, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Z + 1.5 * bottomMaimBarDiam + bottomCoverDistance, 6));

                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X + foundationProperty.Ledge1Length - 50 / 304.8, 6),
                            Math.Round(rebar_p1.Y, 6),
                            Math.Round(rebar_p1.Z, 6));

                        //Кривые стержня
                        List<Curve> mainRebarCurves = new List<Curve>();
                        Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);

                        //Армирование подошвы фундамента по X
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form01
                            , bottomMainBarType
                            , null
                            , null
                            , foundation
                            , XYZ.BasisY
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);

                        ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(2);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set(200 / 304.8);

                        //Если необходимо верхнее армирование по X
                        if (ViewModel.IsCheckedUpperReinforce == true)
                        {
                            ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, foundationProperty.Ledge1Height - (2 * bottomMaimBarDiam + 2 * bottomCoverDistance)));
                        }


                        mainRebarCurves.Clear();

                        rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.Ledge1Length / 2 + coverDistance + bottomMaimBarDiam / 2, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.Ledge1Width / 2 + 25 / 304.8, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Z + bottomMaimBarDiam / 2 + bottomCoverDistance, 6));

                        rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6),
                            Math.Round(rebar_p1.Y + foundationProperty.Ledge1Width - 50 / 304.8, 6),
                            Math.Round(rebar_p1.Z, 6));

                        line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);

                        //Армирование подошвы фундамента по Y
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form01
                            , bottomMainBarType
                            , null
                            , null
                            , foundation
                            , XYZ.BasisX
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);

                        ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(2);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set(200 / 304.8);

                        //Если необходимо верхнее армирование по Y
                        if (ViewModel.IsCheckedUpperReinforce == true)
                        {
                            ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, foundationProperty.Ledge1Height - (2 * bottomMaimBarDiam + 2 * bottomCoverDistance)));
                        }
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалось создать арматуру подошвы!");
                        return Result.Cancelled;
                    }

                    //Создание П-шк по торцам подошвы
                    if (ViewModel.IsCheckedUpperReinforce == true)
                    {
                        try
                        {
                            //Точки для построение П стержней
                            XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.Ledge1Length / 2 + 500 / 304.8, 6)
                                , Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.Ledge1Width / 2 + bottomMaimBarDiam / 2, 6)
                                , Math.Round(foundationProperty.FoundationBasePoint.Z + 1.5 * bottomMaimBarDiam + bottomCoverDistance, 6));

                            XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X + coverDistance - 500 / 304.8, 6)
                                , Math.Round(rebar_p1.Y, 6)
                                , Math.Round(rebar_p1.Z, 6));

                            XYZ rebar_p3 = new XYZ(Math.Round(rebar_p2.X, 6)
                                , Math.Round(rebar_p1.Y, 6)
                                , Math.Round(rebar_p2.Z + foundationProperty.Ledge1Height - (2 * bottomMaimBarDiam + 2 * bottomCoverDistance), 6));

                            XYZ rebar_p4 = new XYZ(Math.Round(rebar_p1.X, 6)
                                , Math.Round(rebar_p1.Y, 6)
                                , Math.Round(rebar_p3.Z, 6));


                            //Кривые стержня
                            List<Curve> mainRebarCurves = new List<Curve>();
                            Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                            mainRebarCurves.Add(line1);
                            Curve line2 = Line.CreateBound(rebar_p2, rebar_p3) as Curve;
                            mainRebarCurves.Add(line2);
                            Curve line3 = Line.CreateBound(rebar_p3, rebar_p4) as Curve;
                            mainRebarCurves.Add(line3);

                            //Армирование подошвы фундамента по X
                            MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                                , form21
                                , bottomMainBarType
                                , null
                                , null
                                , foundation
                                , XYZ.BasisY
                                , mainRebarCurves
                                , RebarHookOrientation.Right
                                , RebarHookOrientation.Right);

                            ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                            MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(2);
                            MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set(200 / 304.8);
                            ElementTransformUtils.MoveElement(doc, MainRebar_1.Id, new XYZ(0, bottomMaimBarDiam, 0));
                        }
                        catch
                        {
                            TaskDialog.Show("Revit", "Не удалось создать обрамляющую арматуру!");
                            return Result.Cancelled;
                        }
                    }

                    //Создание косвенного армирования по Y
                    try
                    {
                        List<ElementId> elementsId = new List<ElementId>();

                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2 + 25 / 304.8, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 + 50 / 304.8, 6),
                            Math.Round(foundationProperty.FoundationBasePoint.Z + foundationProperty.FoundationLength - 50 / 304.8, 6));

                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X + foundationProperty.ColumnLength - 50 / 304.8, 6),
                            Math.Round(rebar_p1.Y, 6),
                            Math.Round(rebar_p1.Z, 6));

                        //Кривые стержня
                        List<Curve> mainRebarCurves = new List<Curve>();
                        Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);


                        //Создание косвенного армирования по X
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form01
                            , indirectMainBarTapes
                            , null
                            , null
                            , foundation
                            , XYZ.BasisY
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);

                        ElementTransformUtils.RotateElement(doc, MainRebar_1.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(2);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set(100 / 304.8);

                        rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2 + 25 / 304.8, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 + 25 / 304.8, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Z + foundationProperty.FoundationLength - 50 / 304.8 - inderectMainBarDiam, 6));

                        rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6),
                            Math.Round(rebar_p1.Y + foundationProperty.ColumnWidth - 50 / 304.8, 6),
                            Math.Round(rebar_p1.Z, 6));

                        mainRebarCurves = new List<Curve>();
                        line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);

                        //Создание косвенного армирования по Y
                        Rebar MainRebar_2 = Rebar.CreateFromCurvesAndShape(doc
                            , form01
                            , indirectMainBarTapes
                            , null
                            , null
                            , foundation
                            , XYZ.BasisX
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);

                        ElementTransformUtils.RotateElement(doc, MainRebar_2.Id, rotateLineBase, (foundation.Location as LocationPoint).Rotation);
                        MainRebar_2.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(2);
                        MainRebar_2.get_Parameter(BuiltInParameter.REBAR_ELEM_BAR_SPACING).Set(100 / 304.8);

                        elementsId.Add(MainRebar_1.Id);
                        elementsId.Add(MainRebar_2.Id);

                        for (int i = 0; i < 2; i++)
                        {
                            ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, -StepIndirectRebar));
                            ElementTransformUtils.CopyElement(doc, MainRebar_2.Id, new XYZ(0, 0, -StepIndirectRebar));
                            StepIndirectRebar += StepIndirectRebar;
                        }

                        //Group newRebarGroup = doc.Create.NewGroup(new List<ElementId> { MainRebar_1.Id, MainRebar_2.Id });

                        //for (int i = 0; i < 2; i++)
                        //{
                        //    ElementTransformUtils.CopyElement(doc, newRebarGroup.Id, new XYZ(0, 0, -StepIndirectRebar));
                        //    StepIndirectRebar += StepIndirectRebar;
                        //}
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалось создать косвенное армирование!");
                        return Result.Cancelled;
                    }
                    //Создание хомутов
                    try
                    {
                        //Точки для построения кривых хомута
                        XYZ firstStirrup_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - 200 / 304.8, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Y - 200 / 304.8, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Z + 150 / 304.8, 6));

                        XYZ firstStirrup_p2 = new XYZ(Math.Round(firstStirrup_p1.X + 400 / 304.8, 6)
                            , Math.Round(firstStirrup_p1.Y, 6)
                            , Math.Round(firstStirrup_p1.Z, 6));

                        XYZ firstStirrup_p3 = new XYZ(Math.Round(firstStirrup_p2.X, 6)
                            , Math.Round(firstStirrup_p2.Y + 200 / 304.8, 6)
                            , Math.Round(firstStirrup_p2.Z, 6));

                        XYZ firstStirrup_p4 = new XYZ(Math.Round(firstStirrup_p3.X - 400 / 304.8, 6)
                            , Math.Round(firstStirrup_p3.Y, 6)
                            , Math.Round(firstStirrup_p3.Z, 6));

                        //Кривые хомута
                        List<Curve> firstStirrupCurves = new List<Curve>();

                        Curve firstStirrup_line1 = Line.CreateBound(firstStirrup_p1, firstStirrup_p2) as Curve;
                        firstStirrupCurves.Add(firstStirrup_line1);
                        Curve firstStirrup_line2 = Line.CreateBound(firstStirrup_p2, firstStirrup_p3) as Curve;
                        firstStirrupCurves.Add(firstStirrup_line2);
                        Curve firstStirrup_line3 = Line.CreateBound(firstStirrup_p3, firstStirrup_p4) as Curve;
                        firstStirrupCurves.Add(firstStirrup_line3);
                        Curve firstStirrup_line4 = Line.CreateBound(firstStirrup_p4, firstStirrup_p1) as Curve;
                        firstStirrupCurves.Add(firstStirrup_line4);


                        Rebar buttonStirrup = Rebar.CreateFromCurvesAndShape(doc
                            , form51
                            , firstStirrupBarTape
                            , rebarHookTypeForStirrup
                            , rebarHookTypeForStirrup
                            , foundation
                            , new XYZ(0, 0, 1)
                            , firstStirrupCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалось создать хомут! Возможно выбран некорректный тип формы 51 или отгиб арматуру не соответствует хомуту!");
                        return Result.Cancelled;
                    }
                }
                t.Commit();
            }

            return Result.Succeeded;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            throw new NotImplementedException();
        }
    }
}
