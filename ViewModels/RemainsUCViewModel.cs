using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model;
using StoreHouse.Model.Models;
using System.Runtime.CompilerServices;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.ViewModels
{
    internal class RemainsUCViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public RemainsUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        // ChosenRemainsItem Field
        private static OutputIngredient _ChosenRemainsItem;
        public OutputIngredient ChosenRemainsItem
        {
            get => _ChosenRemainsItem;
            set
            {
                _ChosenRemainsItem = value;
                OnPropertyChanged();
            }
        }

        public static string GetChosenRemainsItemName()
        {
            return _ChosenRemainsItem.Name;
        }
        //Commands

        private RelayCommand _LoadIngredientSuppliesCommand;
        public RelayCommand LoadIngredientSuppliesCommand
        {
            get
            {
                return _LoadIngredientSuppliesCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.IngredientSupply);
                });
            }
        }

        private RelayCommand _ShowMessageCommand;
        public RelayCommand ShowMessageCommand
        {
            get
            {
                return _ShowMessageCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.ShowMessage("RemainsUC");
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
