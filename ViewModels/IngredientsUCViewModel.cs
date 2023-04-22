using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.View.ManageDbPages.MenuPages;
using StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels;
using System.Runtime.CompilerServices;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.DbContext.Methods;
using StoreHouse.Model.Models;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.ViewSettingMethods;

namespace StoreHouse.ViewModels
{
    internal class IngredientsUCViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public IngredientsUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        public IngredientsUCViewModel(){}

        //Properties
        private static List<OutputIngredient> _AllIngredients = ViewSettings.GetOutputIngredients();
        public List<OutputIngredient> AllIngredients
        {
            get => _AllIngredients;
            set
            {
                _AllIngredients = value;
                OnPropertyChanged();
            }
        }

        public static void SetAllIngredients() => _AllIngredients = ViewSettings.GetOutputIngredients();

        private static OutputIngredient _ChoosenIngredientItem;
        public OutputIngredient ChoosenIngredientItem
        {
            get => _ChoosenIngredientItem;
            set
            {
                _ChoosenIngredientItem = value;
                OnPropertyChanged();
            }
        }

        private static string _SearchBar;
        public string SearchBar
        {
            get => _SearchBar;
            set
            {
                _SearchBar = value;
                OnPropertyChanged();
            }
        }

        private static string _IngredientsCount = Convert.ToString(DbUsage.GetAllIngredients().Count);
        public string IngredientsCount
        {
            get => _IngredientsCount;
            set
            {
                _IngredientsCount = value;
                OnPropertyChanged();
            }
        }
        public static void SetIngredientCount()=>_IngredientsCount = Convert.ToString(DbUsage.GetAllIngredients().Count);

        public static OutputIngredient GetChoosenIngredientItem()
        {
            return _ChoosenIngredientItem;
        }


        private string _NewPrimeCost;
        public string NewPrimeCost
        {
            get => _NewPrimeCost;
            set
            {
                _NewPrimeCost = value;
                OnPropertyChanged();
            }
        }

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
        private RelayCommand _ChangePrimeCostCommand;

        public RelayCommand ChangePrimeCostCommand
        {
            get
            {
                return _ChangePrimeCostCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.ChangePrimeCost);
                });
            }
        }

        private RelayCommand _CloseWindowCommand;
        public RelayCommand CloseWindowCommand
        {
            get
            {
                return _CloseWindowCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.Ingredients);

                });
            }
        }
        private RelayCommand _SearchButtonCommand;
        public RelayCommand SearchButtonCommand
        {
            get
            {
                return _SearchButtonCommand ?? new RelayCommand(obj =>
                {
                    AllIngredients = DbUsage.SearchIngredientsByName(SearchBar);
                });
            }
        }
        private RelayCommand _ChangePrimeCost;
        public RelayCommand ChangePrimeCost
        {
            get
            {
                return _ChangePrimeCost ?? new RelayCommand(obj =>
                {
                    EditDb edit = new EditDb();
                    {
                        edit.EditIngredientPrimeCost(
                            DbUsage.GetIngredientIdByName(GetChoosenIngredientItem().Name),
                            NewPrimeCost
                            );
                    }
                    _MainCodeBehind.LoadView(ViewType.Ingredients);
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
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
