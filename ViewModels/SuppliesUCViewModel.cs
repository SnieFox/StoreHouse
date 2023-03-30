using StoreHouse.Model.Commands;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.ViewModels
{
    internal class SuppliesUCViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public SuppliesUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }


        // ChosenRemainsItem Field
        private static OutputSupplies _ChoosenSupplyItem;
        public OutputSupplies ChoosenSupplyItem
        {
            get => _ChoosenSupplyItem;
            set
            {
                _ChoosenSupplyItem = value;
                OnPropertyChanged();
            }
        }

        public static OutputSupplies GetChoosenSupplyItem()
        {
            return _ChoosenSupplyItem;
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
        private RelayCommand _LoadEditSupplyCommand;
        public RelayCommand LoadEditSupplyCommand
        {
            get
            {
                return _LoadEditSupplyCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditSupply);
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


        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
