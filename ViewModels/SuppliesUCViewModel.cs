using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.ViewModels
{
    internal class SuppliesUCViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public SuppliesUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        //Properties

        //Commands
        private RelayCommand _LoadAddSupplyCommand;
        public RelayCommand LoadAddSupplyCommand
        {
            get
            {
                return _LoadAddSupplyCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.AddSupply);
                });
            }
        }
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
                    _MainCodeBehind.ShowMessage("SuppliesUC");
                });
            }
        }
    }
}
