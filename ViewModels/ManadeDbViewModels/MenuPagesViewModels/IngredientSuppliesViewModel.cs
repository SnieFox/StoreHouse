using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.Model.Commands;

namespace StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels
{
    internal class IngredientSuppliesViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public IngredientSuppliesViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }


        //Fields
        private static string _SupplyIngredientName = $"Постачання для інгредієнту '{RemainsUCViewModel.GetChosenRemainsItemName()}'";
        public string SupplyIngredientName
        {
            get => _SupplyIngredientName;
            set
            {
                _SupplyIngredientName = value;
                OnPropertyChanged();
            }
        }


        //Commands 
        private RelayCommand _LoadRemainsUCCommand;
        public RelayCommand LoadRemainsUCCommand
        {
            get
            {
                return _LoadRemainsUCCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.Remains);
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
