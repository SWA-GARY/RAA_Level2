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
    public class Command : IExternalCommand
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
            MyForm currentForm = new MyForm()
            {
                Width = 800,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm.ShowDialog();
           
            //VARIABLES
            int FLOORNUMBER = 0;
            int plannumber = 100;
            int RCPnumber = 200;
            XYZ insertPoint = new XYZ(2, 1, 0);
            XYZ secondInsertPoint = new XYZ(0, 1, 0);
            //VARIABLES - plan types 
            ViewFamilyType planVFT = GetViewFamilyTypeByName(doc, "Floor Plan", ViewFamily.FloorPlan);
            ViewFamilyType ceilingPlanVFT = GetViewFamilyTypeByName(doc, "Ceiling Plan", ViewFamily.CeilingPlan);

            //VARIABLES titleblock
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_TitleBlocks);

            ElementId tblockId = collector.FirstElementId();

            



            // get form data and do something
            if (currentForm.DialogResult == false)
            {
                return Result.Cancelled;
            }
            //load csv
            List<string[]> Datalist = new List<string[]>();
            string TextBoxResult = currentForm.GetTextBoxValue();
            string[] DataArray = System.IO.File.ReadAllLines(TextBoxResult);

            foreach (string Data in DataArray)
            {
                string[] CellString = Data.Split(',');
                Datalist.Add(CellString);
            }
            //remove header row
            Datalist.RemoveAt(0);
            
            

            

            Transaction t = new Transaction(doc);
            t.Start("Create level and sheet");

            foreach (string[] CellString in Datalist)
            {
                //sort scv data
                string LevelName = CellString[0];
                string Elevation = CellString[1];
                string MElevation = CellString[2];

                double TrueNumber=0;

                //create levels
                
                

                
                // CREATE FLOOR PLANS
                if (currentForm.GetLevelBox() == true)
                {
                    bool ConvertNumber = double.TryParse(Elevation, out TrueNumber);
                    if (ConvertNumber == false)
                    {
                        TaskDialog.Show("error", "cant read some elevation check file formating");
                        continue;
                    }

                    double LevelHeight = TrueNumber;
                    Level mylevel = Level.Create(doc, LevelHeight);
                    mylevel.Name = LevelName;

                    ViewPlan planview = ViewPlan.Create(doc, planVFT.Id, mylevel.Id);
                    FLOORNUMBER++;
                    planview.Name = (FLOORNUMBER.ToString() + " FLOOR PLAN");

                    ViewSheet newSheet = ViewSheet.Create(doc, tblockId);
                    newSheet.Name = planview.Name;
                    //plannumber++;
                    newSheet.SheetNumber = ("A" + plannumber++.ToString());

                    Viewport newViewPort = Viewport.Create(doc, newSheet.Id, planview.Id, insertPoint);
                }
            }
            t.Commit();
            t.Dispose();




            return Result.Succeeded;


        }
        //filter view catagory 
        private ViewFamilyType GetViewFamilyTypeByName(Document doc, string typeName, ViewFamily viewFamily)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(ViewFamilyType));

            foreach (ViewFamilyType currentVFT in collector)
            {
                if (currentVFT.Name == typeName && currentVFT.ViewFamily == viewFamily)
                {
                    return currentVFT;
                }
            }

            return null;
        }


        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
