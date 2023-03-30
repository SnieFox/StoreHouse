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
    internal class IngredientsUCViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public IngredientsUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        //Properties
        
        //Commands

        private RelayCommand _LoadAddIngredientCommand;
        public RelayCommand LoadAddIngredientCommand
        {
            get
            {
                return _LoadAddIngredientCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.AddIngredient);
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
                    _MainCodeBehind.ShowMessage("IngredientsUC");
                });
            }
        }

    }
}
