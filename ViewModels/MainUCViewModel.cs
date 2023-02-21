using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;

namespace StoreHouse.ViewModels
{
    internal class MainUCViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public MainUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        //Properties

        //Commands

        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        private RelayCommand _ShowMessageCommand;
        public RelayCommand ShowMessageCommand
        {
            get
            {
                return _ShowMessageCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.ShowMessage("Привет от MainUC");
                });
            }
        }
    }
}
