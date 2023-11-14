using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using ReinforcementColumnarFoundations.Views.Windows;
using System.Windows.Documents;

namespace ReinforcementColumnarFoundations
{
    public class ReinforcementColumnarFoundationsT1 : IExternalCommand
    {

        public Result Execute(UIApplication uiapp, Document doc, List<FamilyInstance> foundationList, ReinforcementColumnarFoundationsWPF reinforcementColumnarFoundationsWPF)
        {
            View view = doc.ActiveView;

            RebarBarType firstMainBarType = reinforcementColumnarFoundationsWPF.FirstMainBarTape;
            double firstMainBarDiam = firstMainBarType.BarDiameter;

            RebarBarType indirectMainBarTapes = reinforcementColumnarFoundationsWPF.IndirectBarTapes;
            double inderectMainBarDiam = indirectMainBarTapes.BarDiameter;

            RebarBarType bottomMainBarType = reinforcementColumnarFoundationsWPF.BottomMainBarTape;
            double bottomMaimBarDiam = bottomMainBarType.BarDiameter;

            RebarBarType firstStirrupBarTape = reinforcementColumnarFoundationsWPF.FirstStirrupBarTape;
            double firstStirrupBarDiam = firstStirrupBarTape.BarDiameter;

            RebarHookType rebarHookTypeForStirrup = reinforcementColumnarFoundationsWPF.RebarHookTypeForStirrup;

            RebarShape form01 = reinforcementColumnarFoundationsWPF.Form01;
            RebarShape form26 = reinforcementColumnarFoundationsWPF.Form26;
            RebarShape form11 = reinforcementColumnarFoundationsWPF.Form11;
            RebarShape form51 = reinforcementColumnarFoundationsWPF.Form51;

            RebarCoverType scRebarBarCoverType = reinforcementColumnarFoundationsWPF.SupracolumnRebarBarCoverType;
            double coverDistance = scRebarBarCoverType.CoverDistance;

            RebarCoverType rebarCoverType = reinforcementColumnarFoundationsWPF.BottomRebarCoverType;
            double bottomCoverDistance = rebarCoverType.CoverDistance;

            double StepIndirectRebar = reinforcementColumnarFoundationsWPF.StepIndirectRebar / 304.8;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Армирование фундаментов - Тип 1");
                foreach (FamilyInstance foundation in foundationList)
                {
                    FoundationPropertyCollector foundationProperty = new FoundationPropertyCollector(doc, foundation);
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
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2 + firstMainBarDiam / 2 + coverDistance, 6), Math.Round(foundationProperty.FoundationBasePoint.Y + foundationProperty.ColumnWidth / 2 - firstMainBarDiam / 2 - coverDistance, 6), Math.Round((foundationProperty.FoundationBasePoint.Z - foundationProperty.CoverTop) + foundationProperty.FoundationLength, 6));
                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6), Math.Round(rebar_p1.Y, 6), Math.Round(rebar_p1.Z - foundationProperty.FoundationLength + foundationProperty.CoverTop + 1.5 * firstMainBarDiam + bottomCoverDistance, 6));
                        XYZ rebar_p3 = new XYZ(Math.Round(rebar_p2.X - 300 / 308.4, 6), Math.Round(rebar_p2.Y, 6), Math.Round(rebar_p2.Z, 6));

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

                    try
                    {
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X, 6), Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 + firstMainBarDiam / 2 + coverDistance, 6), Math.Round((foundationProperty.FoundationBasePoint.Z - foundationProperty.CoverTop) + foundationProperty.FoundationLength, 6));
                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6), Math.Round(rebar_p1.Y, 6), Math.Round(rebar_p1.Z - foundationProperty.FoundationLength + foundationProperty.CoverTop + 1.5 * firstMainBarDiam + bottomCoverDistance, 6));
                        XYZ rebar_p3 = new XYZ(Math.Round(rebar_p2.X, 6), Math.Round(rebar_p2.Y - 300 / 308.4, 6), Math.Round(rebar_p2.Z, 6));

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
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_LAYOUT_RULE).Set(1);
                        MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_QUANTITY_OF_BARS).Set(6);

                        var elementRotate = ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, 0));
                        ElementTransformUtils.RotateElements(doc, elementRotate, rotateLineBase, 180 * (Math.PI / 180));


                        //MainRebar_1.MaxSpacing = 50 / 304.8;
                        //BoundingBoxXYZ boundingBox = MainRebar_1.get_BoundingBox(view);
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалость создать Г-образный стержень");
                        return Result.Cancelled;
                    }

                    //Боковое армирование подколонника
                    try
                    {
                        //Точки для построения кривых стержня
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2, 6)
                            , Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 + firstMainBarDiam / 2 + coverDistance, 6)
                            , Math.Round((foundationProperty.FoundationBasePoint.Z - foundationProperty.CoverTop) + foundationProperty.FoundationLength, 6));
                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X + 300 / 304.8, 6)
                            , Math.Round(rebar_p1.Y, 6)
                            , Math.Round(rebar_p1.Z, 6));

                        //Кривые стержня
                        List<Curve> mainRebarCurves = new List<Curve>();
                        Curve line1 = Line.CreateBound(rebar_p1, rebar_p2) as Curve;
                        mainRebarCurves.Add(line1);

                        //Создание вертикального арматурного стержня
                        MainRebar_1 = Rebar.CreateFromCurvesAndShape(doc
                            , form01
                            , firstMainBarType
                            , null
                            , null
                            , foundation
                            , XYZ.BasisY
                            , mainRebarCurves
                            , RebarHookOrientation.Right
                            , RebarHookOrientation.Right);
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалось создать боковое армирование подколонника!");
                    }

                    //Армирование подошвы
                    try
                    {
                        //Точки для построения арматуры подошвы
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.Ledge1Length / 2 + 25 / 304.8, 6), Math.Round(foundationProperty.FoundationBasePoint.Y, 6), Math.Round(foundationProperty.FoundationBasePoint.Z + 1.5 * bottomMaimBarDiam + bottomCoverDistance, 6));
                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X + foundationProperty.Ledge1Length - 50 / 304.8, 6), Math.Round(rebar_p1.Y, 6), Math.Round(rebar_p1.Z, 6));

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

                        mainRebarCurves.Clear();

                        rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X), Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.Ledge1Width / 2 + 25 / 304.8, 6), Math.Round(foundationProperty.FoundationBasePoint.Z + bottomMaimBarDiam / 2 + bottomCoverDistance, 6));
                        rebar_p2 = new XYZ(Math.Round(rebar_p1.X, 6), Math.Round(rebar_p1.Y + foundationProperty.Ledge1Width - 50 / 304.8, 6), Math.Round(rebar_p1.Z, 6));
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
                    }
                    catch
                    {
                        TaskDialog.Show("Revit", "Не удалось создать арматуру подошвы!");
                        return Result.Cancelled;
                    }
                    //Создание косвенного армирования
                    try
                    {
                        XYZ rebar_p1 = new XYZ(Math.Round(foundationProperty.FoundationBasePoint.X - foundationProperty.ColumnLength / 2 + 25 / 304.8, 6), Math.Round(foundationProperty.FoundationBasePoint.Y - foundationProperty.ColumnWidth / 2 + 50 / 304.8, 6), Math.Round(foundationProperty.FoundationBasePoint.Z + foundationProperty.FoundationLength - 50 / 304.8, 6));
                        XYZ rebar_p2 = new XYZ(Math.Round(rebar_p1.X + foundationProperty.ColumnLength - 50 / 304.8, 6), Math.Round(rebar_p1.Y, 6), Math.Round(rebar_p1.Z, 6));

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
                        //MainRebar_1.get_Parameter(BuiltInParameter.REBAR_ELEM_QUANTITY_OF_BARS).Set(6);

                        var elementRotate = ElementTransformUtils.CopyElement(doc, MainRebar_1.Id, new XYZ(0, 0, -inderectMainBarDiam));
                        ElementTransformUtils.RotateElements(doc, elementRotate, rotateLineBase, 90 * (Math.PI / 180));
                        elementRotate.Add(MainRebar_1.Id);

                        Group newRebarGroup = doc.Create.NewGroup(elementRotate);

                        for (int i = 0; i < 2; i++)
                        {
                            ElementTransformUtils.CopyElement(doc, newRebarGroup.Id, new XYZ(0, 0, -StepIndirectRebar));
                            StepIndirectRebar += StepIndirectRebar;
                        }
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
        private static Rebar CreatRebar(Document doc, RebarShape rebarShape, RebarBarType barType, RebarHookType startHook, RebarHookType endHook, Element host, XYZ norm, IList<Curve> curves, RebarHookOrientation startHookOrient, RebarHookOrientation endHookOrient)
        {
            return Rebar.CreateFromCurvesAndShape(doc, rebarShape, barType, startHook, endHook, host, norm, curves, startHookOrient, endHookOrient);
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            throw new NotImplementedException();
        }
    }
}
