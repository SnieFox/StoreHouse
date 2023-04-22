using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext.Methods;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.Interfaces;

namespace StoreHouse.ViewModels.ManadeDbViewModels
{
    internal class AddDishViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddDishViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        public AddDishViewModel(string primeCost)
        {
            this.PrimeCost = primeCost;
        }

        //Fields
        private static string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        private static string _PrimeCost = DbUsage.GetPrimeCost(GetAddDishesList());
        public string PrimeCost
        {
            get => _PrimeCost;
            set
            {
                _PrimeCost = value;
                OnPropertyChanged();
            }
        }

        private string _Price;
        public string Price
        {
            get => _Price;
            set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedCategory;
        public string SelectedCategory
        {
            get => _SelectedCategory;
            set
            {
                _SelectedCategory = value;
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
        //Combobox TypeList
        private List<string> _TypeList = new() { "Піца", "Соус", "Снек", "Коктейль" };
        public List<string> TypeList
        {
            get => _TypeList;
            set
            {
                _TypeList = value;
            }
        }
        //Combobox UnitList
        private List<string> _CategoryList = new() { "Їжа", "Напій"};

        public List<string> CategoryList
        {
            get => _CategoryList;
            set
            {
                _CategoryList = value;
            }
        }

        private static List<OutputAddDish> _OutputAddDishesIngredients = new();/* = DbUsage.GetAllDishIngById(DbUsage.GetDishId(DbUsage.GetAllDishes()));*/
        public List<OutputAddDish> OutputAddDishesIngredients
        {
            get => _OutputAddDishesIngredients;
            set
            {
                _OutputAddDishesIngredients = value;
                OnPropertyChanged();
            }
        }
        public static List<OutputAddDish> GetAddDishesList() => _OutputAddDishesIngredients;
        public static void SetAddDishesList(OutputAddDish list)
        {
            _OutputAddDishesIngredients.Add(list);
            _PrimeCost = DbUsage.GetPrimeCost(GetAddDishesList());
        }
        //public static void SetAddDishesList(List<OutputAddDish> list)
        //{
        //    _OutputAddDishesIngredients.Add(list);
        //    _PrimeCost = DbUsage.GetPrimeCost(GetAddDishesList());
        //}

        //Commands

        private RelayCommand _AddDishCommand;
        public RelayCommand AddDishCommand
        {
            get
            {
                return _AddDishCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        AddToDb addToDb = new AddToDb();
                        addToDb.AddDish(
                            _Name,
                            _SelectedType,
                            _SelectedCategory,
                            Math.Round(Convert.ToDecimal(_PrimeCost.Replace('.', ',')), 2),
                            Math.Round(Convert.ToDecimal(_Price.Replace('.', ',')), 2),
                            GetAddDishesList());

                        foreach (var ingr in OutputAddDishesIngredients)
                        {
                            AddToDb addingrToDb = new AddToDb();
                            addingrToDb.AddOutputAddDish(
                                DbUsage.GetDishIdByName(_Name),
                                ingr.Name,
                                $"{Math.Round(Convert.ToDouble(ingr.Count), 2)}кг",
                                $"{Math.Round(Convert.ToDouble(ingr.Sum), 2)}грн"
                            );
                        }

                        //AddDishViewModel.SetAddDishesList(DbUsage.GetAllDishIngById(DbUsage.GetDishId(DbUsage.GetAllDishes())));
                        DishesUCViewModel.SetDishesCount();
                        DishesUCViewModel.SetAllDishes();
                        _MainCodeBehind.LoadView(ViewType.Dishes);
                    }
                    catch (FormatException e)
                    {
                        DishesUCViewModel.SetDishesCount();
                        DishesUCViewModel.SetAllDishes();
                        _MainCodeBehind.LoadView(ViewType.Dishes);
                    }
                    catch (Exception e)
                    {
                        _OutputAddDishesIngredients = null;
                        _Price = "";
                        _PrimeCost = "";
                        _SelectedCategory = "";
                        _SelectedType = "";
                        _Name = "";
                        MessageBox.Show("Щось пішло не так! Перевірте правильність заповнення форми.");
                    }
                });
            }
        }
        /// Переход ко Dishes вьюшке
        private RelayCommand _LoadDishesUCCommand;
        public RelayCommand LoadDishesUCCommand
        {
            get
            {
                return _LoadDishesUCCommand ?? new RelayCommand(obj =>
                {
                    DishesUCViewModel.SetDishesCount();
                    DishesUCViewModel.SetAllDishes();
                    _MainCodeBehind.LoadView(ViewType.Dishes);
                });
            }
        }

        /// Переход ко AddIngredientToDishes вьюшке
        private RelayCommand _LoadAddIngredientToDishCommand;
        public RelayCommand LoadAddIngredientToDishCommand
        {
            get
            {
                return _LoadAddIngredientToDishCommand ?? new RelayCommand(obj =>
                {
                    DishesUCViewModel.SetDishesCount();
                    DishesUCViewModel.SetAllDishes();
                    _MainCodeBehind.LoadView(ViewType.AddIngredientToDish);
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
