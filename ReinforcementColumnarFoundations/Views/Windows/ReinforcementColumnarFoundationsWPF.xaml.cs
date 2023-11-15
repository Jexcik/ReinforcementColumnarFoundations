using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
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
    public partial class ReinforcementColumnarFoundationsWPF : Window
    {
        List<RebarBarType> RebarBarTypesList;
        List<RebarCoverType> RebarCoverTypesList;
        List<RebarShape> RebarShapeList;
        List<RebarHookType> RebarHookTypeList;

        public string SelectedReinforcementTypeButtonName;

        public double StepIndirectRebar;
        public double StepLateralRebar;

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
        public RebarShape Form51;

        public RebarHookType RebarHookTypeForStirrup;


        RainforcementColumnarFoundationsSettings ReinforcementColumnarFoundationsSettingsItem;
        RainforcementColumnarFoundationsSettingsT1 ReinforcementColumnarFoundationsSettingsT1Item;

        public ReinforcementColumnarFoundationsWPF(List<RebarBarType> rebarBarTypesList, List<RebarShape> rebarShapeList, List<RebarCoverType> rebarCoverTypesList, List<RebarHookType> rebarHookTypeList)
        {
            RebarBarTypesList = rebarBarTypesList;
            RebarShapeList = rebarShapeList;
            RebarCoverTypesList = rebarCoverTypesList;
            RebarHookTypeList = rebarHookTypeList;

            ReinforcementColumnarFoundationsSettingsItem = new RainforcementColumnarFoundationsSettings().GetSettings();
            ReinforcementColumnarFoundationsSettingsT1Item = new RainforcementColumnarFoundationsSettingsT1().GetSettings();

            InitializeComponent();
            comboBox_IndirectBarTapes.ItemsSource = RebarBarTypesList;
            comboBox_IndirectBarTapes.DisplayMemberPath = "Name";

            comboBox_FirstBarTapes.ItemsSource = RebarBarTypesList;
            comboBox_FirstBarTapes.DisplayMemberPath = "Name";

            comboBox_LateralBarTapes.ItemsSource = RebarBarTypesList;
            comboBox_LateralBarTapes.DisplayMemberPath = "Name";

            comboBox_SecondBarTapes.ItemsSource = RebarBarTypesList;
            comboBox_SecondBarTapes.DisplayMemberPath = "Name";

            comboBox_BottomBarTapes.ItemsSource = RebarBarTypesList;
            comboBox_BottomBarTapes.DisplayMemberPath = "Name";

            comboBox_FirstStirrupBarTapes.ItemsSource = RebarBarTypesList;
            comboBox_FirstStirrupBarTapes.DisplayMemberPath = "Name";

            comboBox_RebarCoverTypes.ItemsSource = RebarCoverTypesList;
            comboBox_RebarCoverTypes.DisplayMemberPath = "Name";

            comboBox_RebarCoverBottom.ItemsSource = RebarCoverTypesList;
            comboBox_RebarCoverBottom.DisplayMemberPath = "Name";

            comboBox_Form01.ItemsSource = RebarShapeList;
            comboBox_Form01.DisplayMemberPath = "Name";

            comboBox_Form26.ItemsSource = RebarShapeList;
            comboBox_Form26.DisplayMemberPath = "Name";

            comboBox_Form11.ItemsSource = RebarShapeList;
            comboBox_Form11.DisplayMemberPath = "Name";

            comboBox_Form51.ItemsSource = RebarShapeList;
            comboBox_Form51.DisplayMemberPath = "Name";

            comboBox_RebarHookType.ItemsSource = RebarHookTypeList;
            comboBox_RebarHookType.DisplayMemberPath = "Name";

            if (ReinforcementColumnarFoundationsSettingsItem != null)
            {
                switch (ReinforcementColumnarFoundationsSettingsItem.SelectedTypeButtonName)
                {
                    case "buttonType1": buttonType1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)); break;
                }
            }
            else
            {
                buttonType1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

        }

        private void buttonType1_Click(object sender, RoutedEventArgs e)
        {
            SelectedReinforcementTypeButtonName = (sender as Button).Name;
            SetBorderForSelectedButton(sender);
            SetBorderForNonSelectedButtons(sender);


            SetSavedSettingsT1();
        }

        private void SetSavedSettingsT1()
        {
            if (ReinforcementColumnarFoundationsSettingsT1Item != null)
            {
                //Задание сохраненных форм
                if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form01Name) != null)
                {
                    comboBox_Form01.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form01Name);
                }
                else
                {
                    if (comboBox_Form01.Items.Count != 0)
                    {
                        comboBox_Form01.SelectedItem = comboBox_Form01.Items.GetItemAt(0);
                    }
                }
                if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form26Name) != null)
                {
                    comboBox_Form26.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form26Name);
                }
                else
                {
                    if (comboBox_Form26.Items.Count != 0)
                    {
                        comboBox_Form26.SelectedItem = comboBox_Form26.Items.GetItemAt(0);
                    }
                }

                if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form11Name) != null)
                {
                    comboBox_Form11.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form11Name);
                }
                else
                {
                    if (comboBox_Form11.Items.Count != 0)
                    {
                        comboBox_Form11.SelectedItem = comboBox_Form11.Items.GetItemAt(0);
                    }
                }

                if (RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form51Name) != null)
                {
                    comboBox_Form51.SelectedItem = RebarShapeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form51Name);
                }
                else
                {
                    if (comboBox_Form51.Items.Count != 0)
                    {
                        comboBox_Form51.SelectedItem = comboBox_Form51.Items.GetItemAt(0);
                    }
                }

                if (RebarHookTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName) != null)
                {
                    comboBox_RebarHookType.SelectedItem = RebarHookTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName);
                }
                else
                {
                    if (comboBox_RebarHookType.Items.Count != 0)
                    {
                        comboBox_RebarHookType.SelectedItem = comboBox_RebarHookType.Items.GetItemAt(0);
                    }
                }

                //Заполнение сохраненных параметров сечения
                if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName) != null)
                {
                    comboBox_IndirectBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName);
                }
                else
                {
                    if (comboBox_IndirectBarTapes.Items.Count != 0)
                    {
                        comboBox_IndirectBarTapes.SelectedItem = comboBox_IndirectBarTapes.Items.GetItemAt(0);
                    }
                }

                if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName) != null)
                {
                    comboBox_FirstBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName);
                }
                else
                {
                    if (comboBox_FirstBarTapes.Items.Count != 0)
                    {
                        comboBox_FirstBarTapes.SelectedItem = comboBox_FirstBarTapes.Items.GetItemAt(0);
                    }
                }

                if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SecondBarTapesName) != null)
                {
                    comboBox_SecondBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SecondBarTapesName);
                }
                else
                {
                    if (comboBox_SecondBarTapes.Items.Count != 0)
                    {
                        comboBox_SecondBarTapes.SelectedItem = comboBox_SecondBarTapes.Items.GetItemAt(0);
                    }
                }

                if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName) != null)
                {
                    comboBox_LateralBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName);
                }
                else
                {
                    if (comboBox_LateralBarTapes.Items.Count != 0)
                    {
                        comboBox_LateralBarTapes.SelectedItem = comboBox_LateralBarTapes.Items.GetItemAt(0);
                    }
                }


                if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName) != null)
                {
                    comboBox_BottomBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName);
                }
                else
                {
                    if (comboBox_BottomBarTapes.Items.Count != 0)
                    {
                        comboBox_BottomBarTapes.SelectedItem = comboBox_BottomBarTapes.Items.GetItemAt(0);
                    }
                }

                if (RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName) != null)
                {
                    comboBox_FirstStirrupBarTapes.SelectedItem = RebarBarTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName);
                }
                else
                {
                    if (comboBox_FirstStirrupBarTapes.Items.Count != 0)
                    {
                        comboBox_FirstStirrupBarTapes.SelectedItem = comboBox_FirstStirrupBarTapes.Items.GetItemAt(0);
                    }
                }

                if (RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName) != null)
                {
                    comboBox_RebarCoverTypes.SelectedItem = RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName);
                }
                else
                {
                    if (comboBox_RebarCoverTypes.Items.Count != 0)
                    {
                        comboBox_RebarCoverTypes.SelectedItem = comboBox_RebarCoverTypes.Items.GetItemAt(0);
                    }
                }

                if (RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName) != null)
                {
                    comboBox_RebarCoverBottom.SelectedItem = RebarCoverTypesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName);
                }
                else
                {
                    if (comboBox_RebarCoverTypes.Items.Count != 0)
                    {
                        comboBox_RebarCoverBottom.SelectedItem = comboBox_RebarCoverBottom.Items.GetItemAt(0);
                    }
                }
                textBox_StepIndirectRebar.Text = ReinforcementColumnarFoundationsSettingsT1Item.StepIndirectRebar.ToString();
                textBox_StepLateralRebar.Text= ReinforcementColumnarFoundationsSettingsT1Item.StepLateralRebar.ToString() ;
            }
        }

        private void SaveSettings()
        {
            ReinforcementColumnarFoundationsSettingsItem = new RainforcementColumnarFoundationsSettings();
            ReinforcementColumnarFoundationsSettingsItem.SelectedTypeButtonName = SelectedReinforcementTypeButtonName;
            ReinforcementColumnarFoundationsSettingsItem.SaveSettings();

            //Проверка выбора форм стержней
            Form01 = comboBox_Form01.SelectedItem as RebarShape;
            if (Form01 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для прямых стержней (Форма 01) чтобы продолжить работу!");
                return;
            }
            Form26 = comboBox_Form26.SelectedItem as RebarShape;
            if (Form26 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для Z-образных стержней (Форма 26), чтобы продолжить работу!");
                return;
            }
            Form11 = comboBox_Form11.SelectedItem as RebarShape;
            if (Form11 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для Г-образных стержней (Форма 11), чтобы продолжить работу!");
                return;
            }
            Form51 = comboBox_Form51.SelectedItem as RebarShape;
            if (Form51 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для хомутов (Форма 51, 52 и т.д.), чтобы продолжить работу!");
                return;
            }
            RebarHookTypeForStirrup = comboBox_RebarHookType.SelectedItem as RebarHookType;
            if (RebarHookTypeForStirrup == null)
            {
                TaskDialog.Show("Revit", "Выберите тип отгибов для хомута, что бы продолжить работу!");
                return;
            }

            //Проверка заполнения полей в сечении для всех типов
            IndirectBarTapes = comboBox_IndirectBarTapes.SelectedItem as RebarBarType;
            if (IndirectBarTapes == null)
            {
                TaskDialog.Show("Revit", "Выберите тип стержней косвенного армирования!");
                return;
            }

            FirstMainBarTape = comboBox_FirstBarTapes.SelectedItem as RebarBarType;
            if (FirstMainBarTape == null)
            {
                TaskDialog.Show("Revit", "Выберите тип основных стержней подколонника!");
                return;
            }

            LateralBarTapes = comboBox_LateralBarTapes.SelectedItem as RebarBarType;
            if (LateralBarTapes == null)
            {
                TaskDialog.Show("Revit", "Выберите тип боковых стержней подколонника!");
                return;
            }

            FirstStirrupBarTape = comboBox_FirstStirrupBarTapes.SelectedItem as RebarBarType;
            if (FirstStirrupBarTape == null)
            {
                TaskDialog.Show("Revit", "Выберите тип стержня основного хомута, что бы продолжить работу!");
                return;
            }
            BottomMainBarTape = comboBox_BottomBarTapes.SelectedItem as RebarBarType;
            if (BottomMainBarTape == null)
            {
                TaskDialog.Show("Revit", "Выберите тип основных стержней подошвы фундамента!");
                return;
            }
            SupracolumnRebarBarCoverType = comboBox_RebarCoverTypes.SelectedItem as RebarCoverType;
            if (SupracolumnRebarBarCoverType == null)
            {
                TaskDialog.Show("Revit", "Укажите защитный слой, что бы продолжить работу!");
                return;
            }
            BottomRebarCoverType = comboBox_RebarCoverBottom.SelectedItem as RebarCoverType;
            if (BottomRebarCoverType == null)
            {
                TaskDialog.Show("Revit", "Укажите защитный слой арматуры в подошве фундамента");
                return;
            }


            //Сохранение настроек
            if (SelectedReinforcementTypeButtonName == "buttonType1")
            {
                ReinforcementColumnarFoundationsSettingsT1Item = new RainforcementColumnarFoundationsSettingsT1();

                ReinforcementColumnarFoundationsSettingsT1Item.Form01Name = Form01.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.Form26Name = Form26.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.Form11Name = Form11.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.Form51Name = Form51.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName = RebarHookTypeForStirrup.Name;

                ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName = IndirectBarTapes.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName = FirstMainBarTape.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName = LateralBarTapes.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName = FirstStirrupBarTape.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName = BottomMainBarTape.Name;

                ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName = SupracolumnRebarBarCoverType.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName = BottomRebarCoverType.Name;

                double.TryParse(textBox_StepIndirectRebar.Text, out StepIndirectRebar);
                if (string.IsNullOrEmpty(textBox_StepIndirectRebar.Text))
                {
                    TaskDialog.Show("Revit", "Укажите шаг раскладки рядов косвенного армирования!");
                    return;
                }

                double.TryParse(textBox_StepLateralRebar.Text, out StepLateralRebar);
                if (string.IsNullOrEmpty(textBox_StepLateralRebar.Text))
                {
                    TaskDialog.Show("Revit", "Укажите шаг расскладки боковых стержней надколонника!");
                    return;
                }

                ReinforcementColumnarFoundationsSettingsT1Item.StepIndirectRebar = StepIndirectRebar;
                ReinforcementColumnarFoundationsSettingsT1Item.StepLateralRebar=StepLateralRebar;

                ReinforcementColumnarFoundationsSettingsT1Item.SaveSettings();
            }
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            DialogResult = true;
            Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private static void SetBorderForSelectedButton(object sender)
        {
            BrushConverter bc = new BrushConverter();
            (sender as Button).BorderThickness = new Thickness(4, 4, 4, 4);
        }
        private void SetBorderForNonSelectedButtons(object sender)
        {
            BrushConverter bc = new BrushConverter();
            IEnumerable<Button> buttonsSet = buttonsTypeGrid.Children.OfType<Button>()
                .Where(b => b.Name.StartsWith("button_Type"))
                .Where(b => b.Name != (sender as Button).Name);
            foreach (Button btn in buttonsSet)
            {
                btn.BorderThickness = new Thickness(1, 1, 1, 1);
            }
        }
    }
}
