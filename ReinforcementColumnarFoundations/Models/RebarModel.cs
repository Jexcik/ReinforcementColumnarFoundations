using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ReinforcementColumnarFoundations.Models
{
    public class RebarModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Возвращает список типов несущей арматуры</returns>
        public List<RebarBarType> GetRebarTypes(Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(RebarBarType))
                .Cast<RebarBarType>()
                .OrderBy(rbt => rbt.Name)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Возвращает список защитных слоев</returns>
        public List<RebarCoverType> GetCoverTypes(Document doc)
        {
            return new FilteredElementCollector(doc)
            .OfClass(typeof(RebarCoverType))
            .Cast<RebarCoverType>()
            .Where(rct => char.IsDigit(rct.Name[0]))
            .OrderBy(rct => rct.Name)
            .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Возвращает список форм арматуры</returns>
        public List<RebarShape> GetRebarShapes(Document doc)
        {
            return new FilteredElementCollector(doc)
            .OfClass(typeof(RebarShape))
            .Cast<RebarShape>()
            .OrderBy(rs => rs.Name)
            .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>Возвращает список типов отгибов</returns>
        public List<RebarHookType> rebarHookTypeList(Document doc)
        {
            return new FilteredElementCollector(doc)
            .OfClass(typeof(RebarHookType))
            .OrderBy(rht => rht.Name)
            .Cast<RebarHookType>()
            .ToList();
        }


    }
}
