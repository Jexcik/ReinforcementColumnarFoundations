using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using ReinforcementColumnarFoundations.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ReinforcementColumnarFoundations.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReinforcementColumnarFoundationsWPF.xaml
    /// </summary>
    public partial class ReinforcementView : Window
    {

        public double StepIndirectRebar;
        public double StepLateralRebar;

        public int CountX;
        public int CountY;

        public RebarBarType IndirectBarTapes;
        public RebarBarType FirstMainBarTape;
        public RebarBarType SecondMainBarTape;
        public RebarBarType LateralBarTapes;
        public RebarBarType BottomMainBarTape;
        public RebarBarType FirstStirrupBarTape;
        public RebarBarType SecondStirrupBarTape;

        public RebarCoverType SupracolumnRebarBarCoverType;
        public RebarCoverType BottomRebarCoverType;

        public ReinforcementView()
        {

            InitializeComponent();
            //if (ReinforcementColumnarFoundationsSettingsItem != null)
            //{
            //    switch (ReinforcementColumnarFoundationsSettingsItem.SelectedTypeButtonName)
            //    {
            //        case "buttonType1": buttonType1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)); break;
            //    }
            //}
            //else
            //{
            //    buttonType1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            //}

        }

        //private static void SetBorderForSelectedButton(object sender)
        //{
        //    BrushConverter bc = new BrushConverter();
        //    (sender as Button).BorderThickness = new Thickness(4, 4, 4, 4);
        //}
        //private void SetBorderForNonSelectedButtons(object sender)
        //{
        //    BrushConverter bc = new BrushConverter();
        //    IEnumerable<Button> buttonsSet = buttonsTypeGrid.Children.OfType<Button>()
        //        .Where(b => b.Name.StartsWith("button_Type"))
        //        .Where(b => b.Name != (sender as Button).Name);
        //    foreach (Button btn in buttonsSet)
        //    {
        //        btn.BorderThickness = new Thickness(1, 1, 1, 1);
        //    }
        //}
    }
}
