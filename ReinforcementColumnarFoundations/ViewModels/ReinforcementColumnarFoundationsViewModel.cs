using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using ReinforcementColumnarFoundations.Infrastructure.Commands;
using ReinforcementColumnarFoundations.Models;
using ReinforcementColumnarFoundations.ViewModels.Base;
using ReinforcementColumnarFoundations.Views.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ReinforcementColumnarFoundations.ViewModels
{
    public class ReinforcementColumnarFoundationsViewModel : ViewModel
    {
        RainforcementColumnarFoundationsSettings ReinforcementColumnarFoundationsSettingsItem;
        RainforcementColumnarFoundationsSettingsT1 ReinforcementColumnarFoundationsSettingsT1Item;



        public string SelectedReinforcementTypeButtonName;

        private RebarModel rebarModel;

        private ReinforcementView reinforcementView;
        public ReinforcementView _reinforcementView
        {
            get
            {
                if (reinforcementView == null)
                {
                    reinforcementView = new ReinforcementView()
                    {
                        DataContext = this
                    };

                    //if (ReinforcementColumnarFoundationsSettingsItem != null)
                    //{
                    //    switch (ReinforcementColumnarFoundationsSettingsItem.SelectedTypeButtonName)
                    //    {
                    //        case "buttonType1": _reinforcementView.buttonType1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)); break;
                    //    }
                    //}
                    //else
                    //{
                    //    _reinforcementView.buttonType1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    //}
                }

                return reinforcementView;
            }
            set
            {
                reinforcementView = value;
                OnPropertyChanged(nameof(_reinforcementView));
            }
        }

        #region Заголовок окна
        private string _Title = "Армирование столбчатых фундаментов";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            set
            {
                if (Equals(_Title, value))
                {
                    return;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Команды
        public ICommand CloseWindowCommand { get; }
        private void CloseWindow(object parameter)
        {
            _reinforcementView.DialogResult = false;
            _reinforcementView.Close();
        }

        public ICommand OkCommand { get; }
        private void OkWindow(object parameter)
        {
            SaveSettings();
            _reinforcementView.DialogResult = true;
            _reinforcementView.Close();
        }

        private ICommand _buttonType1_Click;
        public ICommand ButtonType1_Click
        {
            get
            {
                return _buttonType1_Click = new RelayCommand(param => buttonType1_Click(param));
            }
        }
        private void buttonType1_Click(object sender)
        {
            SelectedReinforcementTypeButtonName = (sender as Button).Name;
            SetBorderForSelectedButton(sender);
            //SetBorderForNonSelectedButtons(sender);
            SetSavedSettingsT1();
        }

        private static void SetBorderForSelectedButton(object sender)
        {
            BrushConverter bc = new BrushConverter();
            (sender as Button).BorderThickness = new Thickness(4, 4, 4, 4);
        }


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

        #endregion

        #region TextBox
        public double stepIndirectRebar;
        public string StepIndirectRebar
        {
            get => stepIndirectRebar.ToString();
            set
            {
                stepIndirectRebar = double.Parse(value);
                OnPropertyChanged(nameof(StepIndirectRebar));
            }
        }
        public double stepLateralRebar;
        public string StepLateralRebar
        {
            get => stepLateralRebar.ToString();
            set
            {
                stepLateralRebar = double.Parse(value);
                OnPropertyChanged(nameof(StepLateralRebar));
            }

        }

        #endregion

        #region CheckBox
        private bool isCheckedUpperReinforce;
        public bool IsCheckedUpperReinforce
        {
            get
            {
                return isCheckedUpperReinforce;
            }
            set
            {
                isCheckedUpperReinforce = value;
                OnPropertyChanged(nameof(IsCheckedUpperReinforce));
            }
        }
        #endregion

        #region ComboBox
        #region Form
        private RebarShape form01;
        public RebarShape Form01
        {
            get => form01;
            set
            {
                form01 = value;
                OnPropertyChanged(nameof(Form01));
            }
        }
        private RebarShape form26;
        public RebarShape Form26
        {
            get => form26;
            set
            {
                form26 = value;
                OnPropertyChanged(nameof(Form26));
            }
        }
        private RebarShape form11;
        public RebarShape Form11
        {
            get => form11;
            set
            {
                form11 = value;
                OnPropertyChanged(nameof(Form11));
            }
        }
        private RebarShape form21;
        public RebarShape Form21
        {
            get { return form21; }
            set
            {
                form21 = value;
                OnPropertyChanged(nameof(Form21));
            }
        }
        private RebarShape form51;
        public RebarShape Form51
        {
            get => form51;
            set
            {
                form51 = value;
                OnPropertyChanged(nameof(Form51));
            }
        }
        #endregion
        #region RebarBarType
        private RebarBarType selectedFirstRebarType;
        public RebarBarType SelectedFirstRebarType
        {
            get
            {
                return selectedFirstRebarType;
            }
            set
            {
                selectedFirstRebarType = value;
                OnPropertyChanged(nameof(SelectedFirstRebarType));
            }
        }

        private RebarBarType selectedStirrupBarType;
        public RebarBarType SelectedStirrupBarType
        {
            get => selectedStirrupBarType;
            set
            {
                selectedStirrupBarType = value;
                OnPropertyChanged(nameof(SelectedStirrupBarType));
            }
        }

        private RebarBarType selectedIndirectBarType;
        public RebarBarType SelectedIndirectBarType
        {
            get => selectedIndirectBarType;
            set
            {
                selectedIndirectBarType = value;
                OnPropertyChanged(nameof(SelectedIndirectBarType));
            }
        }
        private RebarBarType selectedLateralBarType;
        public RebarBarType SelectedLateralBarType
        {
            get => selectedLateralBarType;
            set
            {
                selectedLateralBarType = value;
                OnPropertyChanged(nameof(SelectedLateralBarType));
            }
        }

        private RebarBarType selectedBottomMainBarType;
        public RebarBarType SelectedBottomMainBarType
        {
            get { return selectedBottomMainBarType; }
            set
            {
                selectedBottomMainBarType = value;
                OnPropertyChanged(nameof(SelectedBottomMainBarType));
            }
        }



        private List<RebarBarType> rebarBarTypeList;
        public List<RebarBarType> RebarBarTypeList
        {
            get { return rebarBarTypeList; }
            set
            {
                rebarBarTypeList = value;
                OnPropertyChanged(nameof(RebarBarTypeList));
            }
        }
        #endregion
        #region Cover
        private RebarCoverType selectedBottomCoverType;
        public RebarCoverType SelectedBottomCoverType
        {
            get => selectedBottomCoverType;
            set
            {
                selectedBottomCoverType = value;
                OnPropertyChanged(nameof(SelectedBottomCoverType));
            }
        }

        private RebarCoverType selectedOtherCoverType;
        public RebarCoverType SelectedOtherCoverType
        {
            get => selectedOtherCoverType;
            set
            {
                selectedOtherCoverType = value;
                OnPropertyChanged(nameof(SelectedOtherCoverType));
            }
        }
        private List<RebarCoverType> rebarCoverTypeList;
        public List<RebarCoverType> RebarCoverTypeList
        {
            get { return rebarCoverTypeList; }
            set
            {
                rebarCoverTypeList = value;
                OnPropertyChanged(nameof(RebarCoverTypeList));
            }
        }
        #endregion
        #region Shape
        private RebarShape selectedRebarShape;
        public RebarShape SelectedRebarShape
        {
            get => selectedRebarShape;
            set
            {
                selectedRebarShape = value;
                OnPropertyChanged(nameof(SelectedRebarShape));
            }
        }




        private List<RebarShape> rebarShapesList;
        public List<RebarShape> RebarShapesList
        {
            get => rebarShapesList;
            set
            {
                rebarShapesList = value;
                OnPropertyChanged(nameof(RebarShapesList));
            }
        }
        #endregion
        #region Hook
        private RebarHookType selectedHookType;
        public RebarHookType SelectedHookType
        {
            get => selectedHookType;
            set
            {
                selectedHookType = value;
                OnPropertyChanged(nameof(SelectedHookType));
            }
        }
        private List<RebarHookType> rebarHookTypeList;
        public List<RebarHookType> RebarHookTypeList
        {
            get => rebarHookTypeList;
            set
            {
                rebarHookTypeList = value;
                OnPropertyChanged(nameof(RebarHookTypeList));
            }
        }
        #endregion
        #endregion

        private void SaveSettings()
        {
            ReinforcementColumnarFoundationsSettingsItem = new RainforcementColumnarFoundationsSettings();
            ReinforcementColumnarFoundationsSettingsItem.SelectedTypeButtonName = SelectedReinforcementTypeButtonName;
            ReinforcementColumnarFoundationsSettingsItem.SaveSettings();

            //Проверка выбора форм стержней
            if (Form01 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для прямых стержней (Форма 01) чтобы продолжить работу!");
                return;
            }

            if (Form26 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для Z-образных стержней (Форма 26), чтобы продолжить работу!");
                return;
            }
            if (Form11 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для Г-образных стержней (Форма 11), чтобы продолжить работу!");
                return;
            }
            if (Form21 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для П-образных стержней (Форма 21), чтобы продолжить работу!");
                return;
            }
            if (Form51 == null)
            {
                TaskDialog.Show("Revit", "Выберите форму арматуры для хомутов (Форма 51, 52 и т.д.), чтобы продолжить работу!");
                return;
            }
            if (SelectedHookType == null)
            {
                TaskDialog.Show("Revit", "Выберите тип отгибов для хомута, что бы продолжить работу!");
                return;
            }

            //Проверка заполнения полей в сечении для всех типов

            if (SelectedIndirectBarType == null)
            {
                TaskDialog.Show("Revit", "Выберите тип стержней косвенного армирования!");
                return;
            }

            if (SelectedFirstRebarType == null)
            {
                TaskDialog.Show("Revit", "Выберите тип основных стержней подколонника!");
                return;
            }

            if (SelectedLateralBarType == null)
            {
                TaskDialog.Show("Revit", "Выберите тип боковых стержней подколонника!");
                return;
            }

            if (SelectedStirrupBarType == null)
            {
                TaskDialog.Show("Revit", "Выберите тип стержня основного хомута, что бы продолжить работу!");
                return;
            }
            if (SelectedBottomMainBarType == null)
            {
                TaskDialog.Show("Revit", "Выберите тип основных стержней подошвы фундамента!");
                return;
            }
            if (SelectedOtherCoverType == null)
            {
                TaskDialog.Show("Revit", "Укажите защитный слой, что бы продолжить работу!");
                return;
            }
            if (SelectedBottomCoverType == null)
            {
                TaskDialog.Show("Revit", "Укажите защитный слой арматуры в подошве фундамента");
                return;
            }


            //    //Сохранение настроек
            //if (SelectedReinforcementTypeButtonName == "buttonType1")
            {
                ReinforcementColumnarFoundationsSettingsT1Item = new RainforcementColumnarFoundationsSettingsT1();

                ReinforcementColumnarFoundationsSettingsT1Item.Form01Name = Form01.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.Form26Name = Form26.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.Form11Name = Form11.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.Form21Name = Form21.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.Form51Name = Form51.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName = SelectedHookType.Name;

                ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName = SelectedIndirectBarType.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName = SelectedFirstRebarType.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName = SelectedLateralBarType.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName = SelectedStirrupBarType.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName = SelectedBottomMainBarType.Name;

                ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName = SelectedOtherCoverType.Name;
                ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName = SelectedBottomCoverType.Name;

                double.TryParse(StepIndirectRebar, out stepIndirectRebar);
                if (string.IsNullOrEmpty(StepIndirectRebar))
                {
                    TaskDialog.Show("Revit", "Укажите шаг раскладки рядов косвенного армирования!");
                    return;
                }

                double.TryParse(StepLateralRebar, out stepLateralRebar);
                if (string.IsNullOrEmpty(StepLateralRebar))
                {
                    TaskDialog.Show("Revit", "Укажите шаг расскладки боковых стержней надколонника!");
                    return;
                }

                //        int.TryParse(textBox_CountX.Text, out CountX);
                //        if (string.IsNullOrEmpty(textBox_CountX.Text))
                //        {
                //            TaskDialog.Show("Revit", "Укажите кол-во стержней по X");
                //            return;
                //        }

                //        int.TryParse(textBox_CountY.Text, out CountY);
                //        if (string.IsNullOrEmpty(textBox_CountY.Text))
                //        {
                //            TaskDialog.Show("Revit", "Укажите кол-во стержней по Y");
                //            return;
                //        }


                ReinforcementColumnarFoundationsSettingsT1Item.StepIndirectRebar = stepIndirectRebar;
                ReinforcementColumnarFoundationsSettingsT1Item.StepLateralRebar = stepLateralRebar;

                //        ReinforcementColumnarFoundationsSettingsT1Item.CountX = CountX;
                //        ReinforcementColumnarFoundationsSettingsT1Item.CountY = CountY;

                ReinforcementColumnarFoundationsSettingsT1Item.SaveSettings();
            }
        }

        private void SetSavedSettingsT1()
        {
            if (ReinforcementColumnarFoundationsSettingsT1Item != null)
            {
                ////Задание сохраненных форм
                if (RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form01Name) != null)
                {
                    Form01 = RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form01Name);
                }
                else
                {
                    if (RebarShapesList.Count != 0)
                    {
                        Form01 = RebarShapesList[0];
                    }
                }

                if (RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form26Name) != null)
                {
                    Form26 = RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form26Name);
                }
                else
                {
                    if (RebarShapesList.Count != 0)
                    {
                        Form26 = RebarShapesList[0];
                    }
                }

                if (RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form11Name) != null)
                {
                    Form11 = RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form11Name);
                }
                else
                {
                    if (RebarShapesList.Count != 0)
                    {
                        Form11 = RebarShapesList[0];
                    }
                }

                if (RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form21Name) != null)
                {
                    Form21 = RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form21Name);
                }
                else
                {
                    if (RebarShapesList.Count != 0)
                    {
                        Form21 = RebarShapesList[0];
                    }
                }

                if (RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form51Name) != null)
                {
                    Form51 = RebarShapesList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.Form51Name);
                }
                else
                {
                    if (RebarShapesList.Count != 0)
                    {
                        Form51 = RebarShapesList[0];
                    }
                }

                if (RebarHookTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName) != null)
                {
                    SelectedHookType = RebarHookTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName);
                }
                else
                {
                    if (RebarHookTypeList.Count != 0)
                    {
                        SelectedHookType = RebarHookTypeList[0];
                    }
                }

                //Заполнение сохраненных параметров сечения
                if (RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName) != null)
                {
                    SelectedIndirectBarType = RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName);
                }
                else
                {
                    if (RebarBarTypeList.Count != 0)
                    {
                        SelectedIndirectBarType = RebarBarTypeList[0];
                    }
                }

                if (RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName) != null)
                {
                    SelectedFirstRebarType = RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName);
                }
                else
                {
                    if (RebarBarTypeList.Count != 0)
                    {
                        SelectedFirstRebarType = RebarBarTypeList[0];
                    }
                }

                //if (RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SecondBarTapesName) != null)
                //{
                //    comboBox_SecondBarTapes = RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SecondBarTapesName);
                //}
                //else
                //{
                //    if (comboBox_SecondBarTapes.Items.Count != 0)
                //    {
                //        comboBox_SecondBarTapes.SelectedItem = comboBox_SecondBarTapes.Items.GetItemAt(0);
                //    }
                //}

                if (RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName) != null)
                {
                    SelectedLateralBarType = RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName);
                }
                else
                {
                    if (RebarBarTypeList.Count != 0)
                    {
                        SelectedLateralBarType = RebarBarTypeList[0];
                    }
                }


                if (RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName) != null)
                {
                    SelectedBottomMainBarType = RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName);
                }
                else
                {
                    if (RebarBarTypeList.Count != 0)
                    {
                        SelectedBottomMainBarType = RebarBarTypeList[0];
                    }
                }

                if (RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName) != null)
                {
                    SelectedStirrupBarType = RebarBarTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName);
                }
                else
                {
                    if (RebarBarTypeList.Count != 0)
                    {
                        SelectedStirrupBarType = RebarBarTypeList[0];
                    }
                }

                if (RebarCoverTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName) != null)
                {
                    SelectedOtherCoverType = RebarCoverTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName);
                }
                else
                {
                    if (RebarCoverTypeList.Count != 0)
                    {
                        SelectedOtherCoverType = RebarCoverTypeList[0];
                    }
                }

                if (RebarCoverTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName) != null)
                {
                    SelectedBottomCoverType = RebarCoverTypeList.FirstOrDefault(rbt => rbt.Name == ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName);
                }
                else
                {
                    if (RebarCoverTypeList.Count != 0)
                    {
                        SelectedBottomCoverType = RebarCoverTypeList[0];
                    }
                }
                StepIndirectRebar = ReinforcementColumnarFoundationsSettingsT1Item.StepIndirectRebar.ToString();
                StepLateralRebar = ReinforcementColumnarFoundationsSettingsT1Item.StepLateralRebar.ToString();

                //        textBox_CountX.Text = ReinforcementColumnarFoundationsSettingsT1Item.CountX.ToString();
                //        textBox_CountY.Text = ReinforcementColumnarFoundationsSettingsT1Item.CountY.ToString();
            }
        }





        public ReinforcementColumnarFoundationsViewModel(Document _doc)
        {
            rebarModel = new RebarModel();
            RebarBarTypeList = rebarModel.GetRebarTypes(_doc);
            RebarCoverTypeList = rebarModel.GetCoverTypes(_doc);
            RebarShapesList = rebarModel.GetRebarShapes(_doc);
            RebarHookTypeList = rebarModel.GetRebarHookType(_doc);

            CloseWindowCommand = new RelayCommand(CloseWindow, p => true);
            OkCommand = new RelayCommand(OkWindow, p => true);

            ReinforcementColumnarFoundationsSettingsItem = new RainforcementColumnarFoundationsSettings().GetSettings();
            ReinforcementColumnarFoundationsSettingsT1Item = new RainforcementColumnarFoundationsSettingsT1().GetSettings();
            _reinforcementView.ShowDialog();
        }
    }
}