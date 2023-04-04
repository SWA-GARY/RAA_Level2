#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace RAA_Level2
{
    [Transaction(TransactionMode.Manual)]
    public class Com_skills04 : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;


            return Result.Succeeded;
        }
        


            public static String GetMethod()
            {
                var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
                return method;
            }
        }
    }

