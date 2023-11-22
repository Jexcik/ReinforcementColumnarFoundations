using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using ReinforcementColumnarFoundations.Infrastructure.Commands;
using ReinforcementColumnarFoundations.Models;
using ReinforcementColumnarFoundations.ViewModels.Base;
using ReinforcementColumnarFoundations.Views.Windows;
using System.Collections.Generic;
using System.Windows.Input;

namespace ReinforcementColumnarFoundations.ViewModels
{
    public class ReinforcementColumnarFoundationsViewModel : ViewModel
    {
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
            _reinforcementView.Close();
        }

        public ICommand OkCommand { get; }
        private void OkWindow(object parameter)
        {
            //    SaveSettings();
            //    DialogResult = true;
            _reinforcementView.Close();
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

        private RebarBarType selectedStirrupBarTapes;
        public RebarBarType SelectedStirrupBarTapes
        {
            get => selectedStirrupBarTapes;
            set
            {
                selectedStirrupBarTapes = value;
                OnPropertyChanged(nameof(SelectedStirrupBarTapes));
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

        //private void SaveSettings()
        //{
        //    ReinforcementColumnarFoundationsSettingsItem = new RainforcementColumnarFoundationsSettings();
        //    ReinforcementColumnarFoundationsSettingsItem.SelectedTypeButtonName = SelectedReinforcementTypeButtonName;
        //    ReinforcementColumnarFoundationsSettingsItem.SaveSettings();

        //    //Проверка выбора форм стержней
        //    Form01 = comboBox_Form01.SelectedItem as RebarShape;
        //    if (Form01 == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите форму арматуры для прямых стержней (Форма 01) чтобы продолжить работу!");
        //        return;
        //    }
        //    Form26 = comboBox_Form26.SelectedItem as RebarShape;
        //    if (Form26 == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите форму арматуры для Z-образных стержней (Форма 26), чтобы продолжить работу!");
        //        return;
        //    }
        //    Form11 = comboBox_Form11.SelectedItem as RebarShape;
        //    if (Form11 == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите форму арматуры для Г-образных стержней (Форма 11), чтобы продолжить работу!");
        //        return;
        //    }
        //    Form21 = comboBox_Form21.SelectedItem as RebarShape;
        //    if (Form21 == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите форму арматуры для П-образных стержней (Форма 21), чтобы продолжить работу!");
        //        return;
        //    }
        //    Form51 = comboBox_Form51.SelectedItem as RebarShape;
        //    if (Form51 == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите форму арматуры для хомутов (Форма 51, 52 и т.д.), чтобы продолжить работу!");
        //        return;
        //    }
        //    RebarHookTypeForStirrup = comboBox_RebarHookType.SelectedItem as RebarHookType;
        //    if (RebarHookTypeForStirrup == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите тип отгибов для хомута, что бы продолжить работу!");
        //        return;
        //    }

        //    //Проверка заполнения полей в сечении для всех типов
        //    IndirectBarTapes = comboBox_IndirectBarTapes.SelectedItem as RebarBarType;
        //    if (IndirectBarTapes == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите тип стержней косвенного армирования!");
        //        return;
        //    }

        //    FirstMainBarTape = comboBox_FirstBarTapes.SelectedItem as RebarBarType;
        //    if (FirstMainBarTape == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите тип основных стержней подколонника!");
        //        return;
        //    }

        //    LateralBarTapes = comboBox_LateralBarTapes.SelectedItem as RebarBarType;
        //    if (LateralBarTapes == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите тип боковых стержней подколонника!");
        //        return;
        //    }

        //    FirstStirrupBarTape = comboBox_FirstStirrupBarTapes.SelectedItem as RebarBarType;
        //    if (FirstStirrupBarTape == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите тип стержня основного хомута, что бы продолжить работу!");
        //        return;
        //    }
        //    BottomMainBarTape = comboBox_BottomBarTapes.SelectedItem as RebarBarType;
        //    if (BottomMainBarTape == null)
        //    {
        //        TaskDialog.Show("Revit", "Выберите тип основных стержней подошвы фундамента!");
        //        return;
        //    }
        //    SupracolumnRebarBarCoverType = comboBox_RebarCoverTypes.SelectedItem as RebarCoverType;
        //    if (SupracolumnRebarBarCoverType == null)
        //    {
        //        TaskDialog.Show("Revit", "Укажите защитный слой, что бы продолжить работу!");
        //        return;
        //    }
        //    BottomRebarCoverType = comboBox_RebarCoverBottom.SelectedItem as RebarCoverType;
        //    if (BottomRebarCoverType == null)
        //    {
        //        TaskDialog.Show("Revit", "Укажите защитный слой арматуры в подошве фундамента");
        //        return;
        //    }


        //    //Сохранение настроек
        //    if (SelectedReinforcementTypeButtonName == "buttonType1")
        //    {
        //        ReinforcementColumnarFoundationsSettingsT1Item = new RainforcementColumnarFoundationsSettingsT1();

        //        ReinforcementColumnarFoundationsSettingsT1Item.Form01Name = Form01.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.Form26Name = Form26.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.Form11Name = Form11.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.Form21Name = Form21.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.Form51Name = Form51.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.RebarHookTypeForStirrupName = RebarHookTypeForStirrup.Name;

        //        ReinforcementColumnarFoundationsSettingsT1Item.IndirectBarTapeName = IndirectBarTapes.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.FirstMainBarTapeName = FirstMainBarTape.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.LateralBarTapeName = LateralBarTapes.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.FirstStirrupBarTapeName = FirstStirrupBarTape.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.BottomMainBarTapeName = BottomMainBarTape.Name;

        //        ReinforcementColumnarFoundationsSettingsT1Item.SupracolumnRebarBarCoverTypeName = SupracolumnRebarBarCoverType.Name;
        //        ReinforcementColumnarFoundationsSettingsT1Item.BottomRebarCoverTypeName = BottomRebarCoverType.Name;

        //        double.TryParse(textBox_StepIndirectRebar.Text, out StepIndirectRebar);
        //        if (string.IsNullOrEmpty(textBox_StepIndirectRebar.Text))
        //        {
        //            TaskDialog.Show("Revit", "Укажите шаг раскладки рядов косвенного армирования!");
        //            return;
        //        }

        //        double.TryParse(textBox_StepLateralRebar.Text, out StepLateralRebar);
        //        if (string.IsNullOrEmpty(textBox_StepLateralRebar.Text))
        //        {
        //            TaskDialog.Show("Revit", "Укажите шаг расскладки боковых стержней надколонника!");
        //            return;
        //        }

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


        //        ReinforcementColumnarFoundationsSettingsT1Item.StepIndirectRebar = StepIndirectRebar;
        //        ReinforcementColumnarFoundationsSettingsT1Item.StepLateralRebar = StepLateralRebar;

        //        ReinforcementColumnarFoundationsSettingsT1Item.CountX = CountX;
        //        ReinforcementColumnarFoundationsSettingsT1Item.CountY = CountY;

        //        ReinforcementColumnarFoundationsSettingsT1Item.SaveSettings();
        //    }
        //}



        public ReinforcementColumnarFoundationsViewModel(Document _doc)
        {
            rebarModel = new RebarModel();
            RebarBarTypeList = rebarModel.GetRebarTypes(_doc);
            RebarCoverTypeList = rebarModel.GetCoverTypes(_doc);
            RebarShapesList = rebarModel.GetRebarShapes(_doc);
            RebarHookTypeList = rebarModel.GetRebarHookType(_doc);
            CloseWindowCommand = new RelayCommand(CloseWindow, p => true);
            OkCommand = new RelayCommand(OkWindow, p => true);
        }
    }
}