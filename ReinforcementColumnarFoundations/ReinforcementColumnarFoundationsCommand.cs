using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ReinforcementColumnarFoundations.ViewModels;
using ReinforcementColumnarFoundations.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReinforcementColumnarFoundations
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class ReinforcementColumnarFoundationsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            Selection sel = commandData.Application.ActiveUIDocument.Selection;

            List<FamilyInstance> foundationsList = GetFoundationsFromCurrentSelection(doc, sel);

            if (foundationsList.Count == 0)
            {
                FoundationSelectionFilter foundationSelFilter = new FoundationSelectionFilter();
                IList<Reference> selReferenceList = null;
                try
                {
                    selReferenceList = sel.PickObjects(ObjectType.Element, foundationSelFilter, "Выберите столбчатые фундаменты!");
                }
                catch
                {
                    return Result.Cancelled;
                }
                foreach (Reference foundRef in selReferenceList)
                {
                    foundationsList.Add(doc.GetElement(foundRef) as FamilyInstance);
                }
            }

            var vm = new ReinforcementColumnarFoundationsViewModel(doc);

            if (vm._reinforcementView.DialogResult != true)
            {
                return Result.Cancelled;
            }

            switch (vm.SelectedReinforcementTypeButtonName)
            {
                case "buttonType1":
                    ReinforcementColumnarFoundationsT1 reinforcementColumnarFoundationsT1 = new ReinforcementColumnarFoundationsT1();
                    reinforcementColumnarFoundationsT1.Execute(commandData.Application, doc, foundationsList, vm);
                    break;
            }



            return Result.Succeeded;
        }

        private List<FamilyInstance> GetFoundationsFromCurrentSelection(Document doc, Selection sel)
        {
            ICollection<ElementId> selectedIds = sel.GetElementIds();
            List<FamilyInstance> tempFoundationsList = new List<FamilyInstance>();
            foreach (ElementId foundationId in selectedIds)
            {
                if (doc.GetElement(foundationId) is FamilyInstance
                    && null != doc.GetElement(foundationId).Category
                    && doc.GetElement(foundationId).Category.Id.IntegerValue.Equals((int)BuiltInCategory.OST_StructuralFoundation))
                {
                    tempFoundationsList.Add(doc.GetElement(foundationId) as FamilyInstance);
                }
            }
            return tempFoundationsList;
        }
    }
}
