using ReinforcementColumnarFoundations.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinforcementColumnarFoundations.ViewModels
{
    internal class ReinforcementColumnarFoundationsViewModel : ViewModel
    {
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
    }
}