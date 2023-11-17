using Autodesk.Revit.DB.Structure;
using ReinforcementColumnarFoundations.Infrastructure.Commands;
using ReinforcementColumnarFoundations.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReinforcementColumnarFoundations.ViewModels
{
    public class ReinforcementColumnarFoundationsViewModel : ViewModel
    {

        public List<string> viewList = new List<string>
        {
            "Pavel"
            ,"Igor"
            ,"Mark"
        };

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
        public ICommand CloseApplicationCommand { get; }
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecuted(object p)
        {
            return true;
        }
        #endregion

        public ReinforcementColumnarFoundationsViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
        }
    }
}