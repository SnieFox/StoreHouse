using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels;
using StoreHouse.ViewModels.ViewSettingMethods;

namespace StoreHouse.ViewModels
{
    internal class DishesUCViewModel : INotifyPropertyChanged
    {

        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        private static OutputDish _ChoosenDishesItem;
        public OutputDish ChoosenDishesItem
        {
            get => _ChoosenDishesItem;
            set
            {
                _ChoosenDishesItem = value;
                OnPropertyChanged();
            }
        }
        public static OutputDish GetChoosenDishItem()
        {
            return _ChoosenDishesItem;
        }
        //ctor
        public DishesUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        public DishesUCViewModel(){}

        //Properties


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
        private static List<OutputDish> _AllDishes = ViewSettings.GetOutputDishes();
        public List<OutputDish> AllDishes
        {
            get => _AllDishes;
            set
            {
                _AllDishes = value;
                OnPropertyChanged();
            }
        }

        public static void SetAllDishes()=> _AllDishes = ViewSettings.GetOutputDishes();
        
        private static string _DishesCount = Convert.ToString(DbUsage.GetAllDishes().Count);
        public string DishesCount
        {
            get => _DishesCount;
            set
            {
                _DishesCount = value;
                OnPropertyChanged();
            }
        }
        public static void SetDishesCount()=>_DishesCount = Convert.ToString(DbUsage.GetAllDishes().Count);
        //Commands
        private RelayCommand _LoadAddDishCommand;
        public RelayCommand LoadAddDishCommand
        {
            get
            {
                return _LoadAddDishCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.AddDish);
                });
            }
        }
        private RelayCommand _EditDishesCommand;
        public RelayCommand EditDishesCommand
        {
            get
            {
                return _EditDishesCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditDishes);
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
                    AllDishes = DbUsage.SearchDishesByName(SearchBar);
                });
            }
        }

        private RelayCommand _DishesDetailsCommand;
        public RelayCommand DishesDetailsCommand
        {
            get
            {
                return _DishesDetailsCommand ?? new RelayCommand(obj =>
                {
                    DetailsDishViewModel.SetDishIngredientList(DbUsage.GetDishIngredientList());
                    _MainCodeBehind.LoadView(ViewType.DetailsDishes);
                });
            }
        }
        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        /// 
        private RelayCommand _ShowMessageCommand;
        public RelayCommand ShowMessageCommand
        {
            get
            {
                return _ShowMessageCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.ShowMessage("DishesUC");
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
