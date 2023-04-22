using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext.Methods;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StoreHouse.Model.DbContext;

namespace StoreHouse.ViewModels.ManadeDbViewModels
{
    internal class AddIngredientViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddIngredientViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        //Methods


        //Fields
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedUnit;
        public string SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedType;
        public string SelectedType
        {
            get => _SelectedType;
            set
            {
                _SelectedType = value;
                OnPropertyChanged();
            }
        }

        private string _CurrentRemains;
        public string CurrentRemains
        {
            get => _CurrentRemains;
            set
            {
                _CurrentRemains = value;
                OnPropertyChanged();
            }
        }

        private string _PrimeCost;
        public string PrimeCost
        {
            get => _PrimeCost;
            set
            {
                _PrimeCost = value;
                OnPropertyChanged();
            }
        }

        //Combobox UnitList
        private List<string> _UnitList = new() { "кг", "л", "шт" };

        public List<string> UnitList
        {
            get => _UnitList;
            set
            {
                _UnitList = value;
            }
        }

        //Combobox UnitList
        private List<string> _TypeList = new() { "Їжа", "Напій", "Виробництво" };

        public List<string> TypeList
        {
            get => _TypeList;
            set
            {
                _TypeList = value;
            }
        }

        //Commands
        private RelayCommand _AddIngredientCommand;
        public RelayCommand AddIngredientCommand
        {
            get
            {
                return _AddIngredientCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        AddToDb addToDb = new AddToDb();
                        addToDb.AddIngredient(
                            _Name,
                            _SelectedUnit,
                            $"{_CurrentRemains} {_SelectedUnit}",
                            Math.Round(Convert.ToDecimal(_PrimeCost.Replace('.', ',')), 2),
                            Math.Round(DbUsage.GetSum(_PrimeCost, _CurrentRemains), 2),
                            _SelectedType);
                        IngredientsUCViewModel.SetAllIngredients();
                            _MainCodeBehind.LoadView(ViewType.Ingredients);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Щось пішло не так! Перевірте правильність заповнення форми.");
                    }
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
                    IngredientsUCViewModel.SetAllIngredients();
                    _MainCodeBehind.LoadView(ViewType.Ingredients);
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
