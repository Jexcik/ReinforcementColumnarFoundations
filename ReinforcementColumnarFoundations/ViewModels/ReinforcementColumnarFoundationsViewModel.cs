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


        private RebarCoverType selectedCoverType;
        public RebarCoverType SelectedCoverType
        {
            get
            {
                return selectedCoverType;
            }
            set
            {
                selectedCoverType = value;
                OnPropertyChanged(nameof(SelectedCoverType));
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

        private List<RebarShape> rebarShapesList;
        public List <RebarShape> RebarShapesList
        {
            get => rebarShapesList;
            set
            {
                rebarShapesList = value;
                OnPropertyChanged(nameof(RebarShapesList));
            }
        }


        #endregion


        public ReinforcementColumnarFoundationsViewModel(Document _doc)
        {
            rebarModel = new RebarModel();
            RebarBarTypeList = rebarModel.GetRebarTypes(_doc);
            RebarCoverTypeList = rebarModel.GetCoverTypes(_doc);
            RebarShapesList = rebarModel.GetRebarShapes(_doc);
            CloseWindowCommand = new RelayCommand(CloseWindow, p => true);
        }
    }
}