using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace RAA_Level2
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    
    public partial class MyForm2 : Window
    {
        public Document MyDoc;
        public List<Element> listref;
        public MyForm2(Document doc,List<Reference> selectionList)
        {
            InitializeComponent();
            MyDoc = doc;
            for (int i = 1; i <= 20; i++)
            {
                StartNumber.Items.Add(i.ToString());
            }
            StartNumber.SelectedIndex = 0;
            CustomNum.Text="0";

            if(selectionList != null)
            {
                ListBox.Items.Clear();
                listref = new List<Element>();

                foreach (Reference SelectRef in selectionList)
                {
                    Element curElem = doc.GetElement(SelectRef);
                    
                    if (curElem is Viewport)
                    {
                        listref.Add(curElem);
                        Parameter NameParam = curElem.get_Parameter(BuiltInParameter.VIEWPORT_VIEW_NAME);
                        Parameter NumParam = curElem.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
                        ListBox.Items.Add(NameParam.AsString() + ":" + NumParam.AsString());
                       //ListBox.Items.Add(curElem.Id.ToString());
                    }
                }



                


            }
            
            
            
        }
        
        

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            





        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();   
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }




        internal List<Element> GetSelectedView()
        { 
            if (listref != null)
            
                return listref;
               else
                    return null;
           
        }

        internal int GetStartnumber()
        {
            if (CustomNum.Text =="0")
            {
                string selectednumber = StartNumber.SelectedItem.ToString();
                int returnValue = Convert.ToInt32(selectednumber);
                return returnValue;
           }
            else
            {
                string selectednumber2 = CustomNum.Text;
                int returnValue2 = Convert.ToInt32(selectednumber2);
                return returnValue2;
            }
            
        }

        //internal renumberviews(List<Element> ShowViews, int CurNum)
        //{
           
        //     foreach (Element el in ShowViews)
        //    {
        //        Parameter viewnumber = el.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
        //        viewnumber.Set(CurNum.ToString() + "z");
        //        CurNum++;
               
        //    }
        //    return CurNum++;
        //}
    }
}
