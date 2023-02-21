using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;

namespace StoreHouse.ViewModels
{
    internal class MenuViewModel : INotifyPropertyChanged
    {
        //ctor
        public MenuViewModel()
        {
            
        }

        public IMainWindowsCodeBehind CodeBehind { get; set; }


        /// <summary>
        /// Переход к Remains вьюшке
        /// </summary>
        private RelayCommand _LoadRemainsUCCommand;
        public RelayCommand LoadRemainsUCCommand
        {
            get
            {
                return _LoadRemainsUCCommand ?? new RelayCommand(obj =>
                {
                    CodeBehind.LoadView(ViewType.Remains);
                });
            }
        }

        /// <summary>
        /// Переход к Supplies вьюшке
        /// </summary>
        private RelayCommand _LoadSuppliesUCCommand;
        public RelayCommand LoadSuppliesUCCommand
        {
            get
            {
                return _LoadSuppliesUCCommand ?? new RelayCommand(obj =>
                {
                    CodeBehind.LoadView(ViewType.Supplies);
                });
            }
        }

        /// <summary>
        /// Переход к WriteOffs вьюшке
        /// </summary>
        private RelayCommand _LoadWriteOffsUCCommand;
        public RelayCommand LoadWriteOffsUCCommand
        {
            get
            {
                return _LoadWriteOffsUCCommand ?? new RelayCommand(obj =>
                {
                    CodeBehind.LoadView(ViewType.WriteOffs);
                });
            }
        }

        /// <summary>
        /// Переход к Dishes вьюшке
        /// </summary>
        private RelayCommand _LoadDishesUCCommand;
        public RelayCommand LoadDishesUCCommand
        {
            get
            {
                return _LoadDishesUCCommand ?? new RelayCommand(obj =>
                {
                    CodeBehind.LoadView(ViewType.Dishes);
                });
            }
        }

        /// <summary>
        /// Переход ко Ingredients вьюшке
        /// </summary>
        private RelayCommand _LoadIngredientsUCCommand;
        public RelayCommand LoadIngredientsUCCommand
        {
            get
            {
                return _LoadIngredientsUCCommand ?? new RelayCommand(obj =>
                {
                    CodeBehind.LoadView(ViewType.Ingredients);
                });
            }
        }

        /// <summary>
        /// Возвращение к главной вьюшке
        /// </summary>
        private RelayCommand _LoadMainUCCommand;
        public RelayCommand LoadMainUCCommand
        {
            get
            {
                return _LoadMainUCCommand ?? new RelayCommand(obj =>
                {
                    CodeBehind.LoadView(ViewType.Main);
                });
            }
        }

        private RelayCommand _LoadAddIngredientCommand;
        public RelayCommand LoadAddIngredientCommand
        {
            get
            {
                return _LoadAddIngredientCommand ?? new RelayCommand(obj =>
                {
                    CodeBehind.LoadView(ViewType.AddIngredient);
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
