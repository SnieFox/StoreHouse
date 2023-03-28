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
using StoreHouse.Model.DbContext.Methods;
using StoreHouse.Model.OutputDataModels;

namespace StoreHouse.ViewModels.ManadeDbViewModels
{
    class AddIngredientToDishViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddIngredientToDishViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }



        //Fields
        private static string _Count;
        public string Count
        {
            get => _Count;
            set
            {
                _Count = value;
                Sum = Math.Round(DbUsage.GetSum(DbUsage.GetPrimeCost(DbUsage.GetIngredientIdByName(SeletedProduct), SeletedProduct), _Count),2);
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

                        //AddToDb addToDb = new AddToDb();
                        //addToDb.AddOutputAddDish(
                        //    DbUsage.GetDishId(DbUsage.GetAllDishes()),
                        //    SeletedProduct,
                        //    $"{Count}кг",
                        //    Math.Round(Sum, 2).ToString()
                        //    );
                        //AddDishViewModel.SetAddDishesList(DbUsage.GetAllDishIngById(DbUsage.GetDishId(DbUsage.GetAllDishes())));
                       
                        AddDishViewModel.SetAddDishesList(new OutputAddDish(SeletedProduct,
                            $"{Count}кг",
                            Math.Round(Sum, 2).ToString()));
                        _MainCodeBehind.LoadView(ViewType.AddDish);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Щось пішло не так! Перевірте правильність заповнення форми.");
                        throw;
                    }
                });
            }
        }
        /// Переход ко WriteOff вьюшке

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
                    _MainCodeBehind.LoadView(ViewType.AddDish);
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
