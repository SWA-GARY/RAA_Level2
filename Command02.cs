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
    public class Command02 : IExternalCommand
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

            // put any code needed for the form here

            // open form
            MyForm2 currentForm = new MyForm2(doc, null)
            {
                Width = 800,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm.ShowDialog();

            
            

            if (currentForm.DialogResult == false)
            {
                return Result.Failed;

            }
            List<Reference> selection = new List<Reference>();
            bool flag = true;
            while (flag == true)
            {
                try
                {
                    Reference curfref = uidoc.Selection.PickObject(ObjectType.Element, "pick items and press esc key when done");
                    selection.Add(curfref);
                }
                catch (Exception)
                {
                    flag = false;
                }
            }


            MyForm2 currentForm2 = new MyForm2(doc, selection)
            {
                Width = 800,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm2.ShowDialog();
            
            if (currentForm2.DialogResult == false)
            {
                return Result.Failed;

            }
            List<Element> ShowViews = currentForm2.GetSelectedView();
            int startNum= currentForm2.GetStartnumber();
            int CurNum= startNum;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("renumber views");
                    foreach (Element el in ShowViews)
                {
                    Parameter viewnumber = el.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
                    viewnumber.Set(CurNum.ToString() + "z");
                    CurNum++;
                    
                }
                CurNum = startNum;
                foreach (Element el2 in ShowViews)
                {
                    Parameter viewnumber = el2.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
                    viewnumber.Set(CurNum.ToString());
                    CurNum++;
                }
                TaskDialog.Show("complete","renumberd " + CurNum.ToString());
                t.Commit();    
            }
                
            


           






               
                return Result.Succeeded;


            }



            public static String GetMethod()
            {
                var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
                return method;
            }
        }
    } 
