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
        private RebarBarType selectedRebarType;
        public RebarBarType SelectedRebarType
        {
            get
            {
                return selectedRebarType;
            }
            set
            {
                selectedRebarType = value;
                OnPropertyChanged(nameof(SelectedRebarType));
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


        public ReinforcementColumnarFoundationsViewModel(Document _doc)
        {
            rebarModel = new RebarModel();
            RebarBarTypeList = rebarModel.GetRebarTypes(_doc);
            RebarCoverTypeList = rebarModel.GetCoverTypes(_doc);
            RebarShapesList = rebarModel.GetRebarShapes(_doc);
            RebarHookTypeList=rebarModel.GetRebarHookType(_doc);
            CloseWindowCommand = new RelayCommand(CloseWindow, p => true);
        }
    }
}