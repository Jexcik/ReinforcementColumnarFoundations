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
        public string SelectedReinforcementTypeButtonName;

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

        public RebarShape Form01;
        public RebarShape Form26;
        public RebarShape Form11;
        public RebarShape Form21;
        public RebarShape Form51;

        public RebarHookType RebarHookTypeForStirrup;


        RainforcementColumnarFoundationsSettings ReinforcementColumnarFoundationsSettingsItem;
        RainforcementColumnarFoundationsSettingsT1 ReinforcementColumnarFoundationsSettingsT1Item;

        public ReinforcementView()
        {
            ReinforcementColumnarFoundationsSettingsItem = new RainforcementColumnarFoundationsSettings().GetSettings();
            ReinforcementColumnarFoundationsSettingsT1Item = new RainforcementColumnarFoundationsSettingsT1().GetSettings();

            InitializeComponent();

            //comboBox_IndirectBarTapes.ItemsSource = RebarBarTypesList;
            //comboBox_IndirectBarTapes.DisplayMemberPath = "Name";

            ////comboBox_FirstBarTapes.ItemsSource = RebarBarTypesList;
            ////comboBox_FirstBarTapes.DisplayMemberPath = "Name";

            //comboBox_LateralBarTapes.ItemsSource = RebarBarTypesList;
            //comboBox_LateralBarTapes.DisplayMemberPath = "Name";

            //comboBox_SecondBarTapes.ItemsSource = RebarBarTypesList;
            //comboBox_SecondBarTapes.DisplayMemberPath = "Name";

            //comboBox_BottomBarTapes.ItemsSource = RebarBarTypesList;
            //comboBox_BottomBarTapes.DisplayMemberPath = "Name";

            //comboBox_FirstStirrupBarTapes.ItemsSource = RebarBarTypesList;
            //comboBox_FirstStirrupBarTapes.DisplayMemberPath = "Name";

            //comboBox_RebarCoverTypes.ItemsSource = RebarCoverTypesList;
            //comboBox_RebarCoverTypes.DisplayMemberPath = "Name";

            //comboBox_RebarCoverBottom.ItemsSource = RebarCoverTypesList;
            //comboBox_RebarCoverBottom.DisplayMemberPath = "Name";

            //comboBox_Form01.ItemsSource = RebarShapeList;
            //comboBox_Form01.DisplayMemberPath = "Name";

            //comboBox_Form26.ItemsSource = RebarShapeList;
            //comboBox_Form26.DisplayMemberPath = "Name";

            //comboBox_Form11.ItemsSource = RebarShapeList;
            //comboBox_Form11.DisplayMemberPath = "Name";

            //comboBox_Form21.ItemsSource = RebarShapeList;
            //comboBox_Form21.DisplayMemberPath = "Name";

            //comboBox_Form51.ItemsSource = RebarShapeList;
            //comboBox_Form51.DisplayMemberPath = "Name";

            //comboBox_RebarHookType.ItemsSource = RebarHookTypeList;
            //comboBox_RebarHookType.DisplayMemberPath = "Name";

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

        //private void buttonType1_Click(object sender, RoutedEventArgs e)
        //{
        //    SelectedReinforcementTypeButtonName = (sender as Button).Name;
        //    SetBorderForSelectedButton(sender);
        //    SetBorderForNonSelectedButtons(sender);


        //    SetSavedSettingsT1();
        //}

        //private void SetSavedSettingsT1()
        //{
        //    if (ReinforcementColumnarFoundationsSettingsT1Item != null)
        //    {
        //        //Задание сохраненных форм
        //        if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form01Name) != null)
        //        {
        //            comboBox_Form01.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form01Name);
        //        }
        //        else
        //        {
        //            if (comboBox_Form01.Items.Count != 0)
        //            {
        //                comboBox_Form01.SelectedItem = comboBox_Form01.Items.GetItemAt(0);
        //            }
        //        }
        //        if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form26Name) != null)
        //        {
        //            comboBox_Form26.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form26Name);
        //        }
        //        else
        //        {
        //            if (comboBox_Form26.Items.Count != 0)
        //            {
        //                comboBox_Form26.SelectedItem = comboBox_Form26.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form11Name) != null)
        //        {
        //            comboBox_Form11.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form11Name);
        //        }
        //        else
        //        {
        //            if (comboBox_Form11.Items.Count != 0)
        //            {
        //                comboBox_Form11.SelectedItem = comboBox_Form11.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form21Name) != null)
        //        {
        //            comboBox_Form21.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form21Name);
        //        }
        //        else
        //        {
        //            if (comboBox_Form21.Items.Count != 0)
        //            {
        //                comboBox_Form21.SelectedItem = comboBox_Form21.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form51Name) != null)
        //        {
        //            comboBox_Form51.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form51Name);
        //        }
        //        else
        //        {
        //            if (comboBox_Form51.Items.Count != 0)
        //            {
        //                comboBox_Form51.SelectedItem = comboBox_Form51.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarHookTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName) != null)
        //        {
        //            comboBox_RebarHookType.SelectedItem = RebarHookTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName);
        //        }
        //        else
        //        {
        //            if (comboBox_RebarHookType.Items.Count != 0)
        //            {
        //                comboBox_RebarHookType.SelectedItem = comboBox_RebarHookType.Items.GetItemAt(0);
        //            }
        //        }

        //        //Заполнение сохраненных параметров сечения
        //        if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName) != null)
        //        {
        //            comboBox_IndirectBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName);
        //        }
        //        else
        //        {
        //            if (comboBox_IndirectBarTapes.Items.Count != 0)
        //            {
        //                comboBox_IndirectBarTapes.SelectedItem = comboBox_IndirectBarTapes.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName) != null)
        //        {
        //            comboBox_FirstBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName);
        //        }
        //        else
        //        {
        //            if (comboBox_FirstBarTapes.Items.Count != 0)
        //            {
        //                comboBox_FirstBarTapes.SelectedItem = comboBox_FirstBarTapes.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SecondBarTapesName) != null)
        //        {
        //            comboBox_SecondBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SecondBarTapesName);
        //        }
        //        else
        //        {
        //            if (comboBox_SecondBarTapes.Items.Count != 0)
        //            {
        //                comboBox_SecondBarTapes.SelectedItem = comboBox_SecondBarTapes.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName) != null)
        //        {
        //            comboBox_LateralBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName);
        //        }
        //        else
        //        {
        //            if (comboBox_LateralBarTapes.Items.Count != 0)
        //            {
        //                comboBox_LateralBarTapes.SelectedItem = comboBox_LateralBarTapes.Items.GetItemAt(0);
        //            }
        //        }


        //        if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName) != null)
        //        {
        //            comboBox_BottomBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName);
        //        }
        //        else
        //        {
        //            if (comboBox_BottomBarTapes.Items.Count != 0)
        //            {
        //                comboBox_BottomBarTapes.SelectedItem = comboBox_BottomBarTapes.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName) != null)
        //        {
        //            comboBox_FirstStirrupBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName);
        //        }
        //        else
        //        {
        //            if (comboBox_FirstStirrupBarTapes.Items.Count != 0)
        //            {
        //                comboBox_FirstStirrupBarTapes.SelectedItem = comboBox_FirstStirrupBarTapes.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName) != null)
        //        {
        //            comboBox_RebarCoverTypes.SelectedItem = RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName);
        //        }
        //        else
        //        {
        //            if (comboBox_RebarCoverTypes.Items.Count != 0)
        //            {
        //                comboBox_RebarCoverTypes.SelectedItem = comboBox_RebarCoverTypes.Items.GetItemAt(0);
        //            }
        //        }

        //        if (RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName) != null)
        //        {
        //            comboBox_RebarCoverBottom.SelectedItem = RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName);
        //        }
        //        else
        //        {
        //            if (comboBox_RebarCoverTypes.Items.Count != 0)
        //            {
        //                comboBox_RebarCoverBottom.SelectedItem = comboBox_RebarCoverBottom.Items.GetItemAt(0);
        //            }
        //        }
        //        textBox_StepIndirectRebar.Text = ReinforcementColumnarFoundationsSettingsT1Item.StepIndirectRebar.ToString();
        //        textBox_StepLateralRebar.Text = ReinforcementColumnarFoundationsSettingsT1Item.StepLateralRebar.ToString();

        //        textBox_CountX.Text = ReinforcementColumnarFoundationsSettingsT1Item.CountX.ToString();
        //        textBox_CountY.Text = ReinforcementColumnarFoundationsSettingsT1Item.CountY.ToString();
        //    }
        //}


        //private void btn_Ok_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveSettings();
        //    DialogResult = true;
        //    Close();
        //}

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
