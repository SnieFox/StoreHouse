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
using System.Windows;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.Model.DbContext.Methods;
using StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels;

namespace StoreHouse.ViewModels.ManadeDbViewModels
{
    internal class EditIngredientInDishViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public EditIngredientInDishViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        public EditIngredientInDishViewModel(){}



        //Fields
        private static string _Count;
        public string Count
        {
            get => _Count;
            set
            {
                _Count = value;
                Sum = Math.Round(DbUsage.GetSum(DbUsage.GetPrimeCost(DbUsage.GetIngredientIdByName(SeletedProduct), SeletedProduct), _Count), 2);
                OnPropertyChanged();
            }
        }

        private static decimal _Sum;
        public decimal Sum
        {
            get => _Sum;
            set
            {
                _Sum = value;
                OnPropertyChanged();
            }
        }

        private string _SeletedProduct;
        public string SeletedProduct
        {
            get => _SeletedProduct;
            set
            {
                _SeletedProduct = value;
                OnPropertyChanged();
            }
        }

        //Combobox ProductList
        private List<string> _ProductList = DbUsage.GetProductNames();
        public List<string> ProductList
        {
            get => _ProductList;
            set
            {
                _ProductList = value;
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
        }

        // Commands
        // Комманда добавления Списания
        private RelayCommand _AddIngredientToDish;
        public RelayCommand AddIngredientToDish
        {
            get
            {
                return _AddIngredientToDish ?? new RelayCommand(obj =>
                {
                    try
                    {
                        SetAddDishesList(new OutputAddDish(SeletedProduct,
                            $"{Count}кг",
                            Math.Round(Sum, 2).ToString()));

                        EditDb editPC = new EditDb();
                        editPC.EditDishPrimeCost(
                            DbUsage.GetDishIdByName(DishesUCViewModel.GetChoosenDishItem().Name),
                            Convert.ToString(
                                DbUsage.GetPrimeCost(DbUsage.GetIngredientIdByName(SeletedProduct), SeletedProduct)),
                            _Count
                            );

                        foreach (var ingr in OutputAddDishesIngredients)
                        {
                            AddToDb addingrToDb = new AddToDb();
                            addingrToDb.AddOutputAddDish(
                                DbUsage.GetDishIdByName(DishesUCViewModel.GetChoosenDishItem().Name),
                                ingr.Name,
                                ingr.Count,
                                ingr.Sum
                            );
                        }

                        EditDishViewModel.SetAllDishesIngredients(DbUsage.GetDishIngredientList());
                        _MainCodeBehind.LoadView(ViewType.EditDishes);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Щось пішло не так! Перевірте правильність заповнення форми.");
                        throw;
                    }
                });
            }
        }

        private RelayCommand _LoadAddDishUCCommand;
        public RelayCommand LoadAddDishUCCommand
        {
            get
            {
                return _LoadAddDishUCCommand ?? new RelayCommand(obj =>
                {
                    Count = "";
                    Sum = 0;
                    SeletedProduct = "";
                    _MainCodeBehind.LoadView(ViewType.EditDishes);
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
