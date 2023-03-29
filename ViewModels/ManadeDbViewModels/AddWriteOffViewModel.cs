using StoreHouse.Model;
using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext;
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

namespace StoreHouse.ViewModels.ManadeDbViewModels
{
    internal class AddWriteOffViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddWriteOffViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        //Methods
        //Fields
        private static string _Cause;
        public string Cause
        {
            get => _Cause;
            set
            {
                _Cause = value;
                OnPropertyChanged();
            }
        }

        private static string _Count;
        public string Count
        {
            get => _Count;
            set
            {
                _Count = value;
                Sum = DbUsage.GetSum(DbUsage.GetPrimeCost(DbUsage.GetIngredientIdByName(SeletedIngredient), SeletedIngredient), _Count);
                OnPropertyChanged();
            }
        }

        //DishCount
        private static string _DishCount;
        public string DishCount
        {
            get => _DishCount;
            set
            {
                _DishCount = value;
                DishSum = DbUsage.GetSum(DishCount, DbUsage.GetAllDishIngById(DbUsage.GetDishIdByName(SeletedDish)));
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

        private static decimal _DishSum;
        public decimal DishSum
        {
            get => _DishSum;
            set
            {
                _DishSum = value;
                OnPropertyChanged();
            }
        }
        //Combobox IngredientsList
        private List<string> _IngredientsList = DbUsage.GetProductNames();

        public List<string> IngredientsList
        {
            get => _IngredientsList;
            set
            {
                _IngredientsList = value;
            }
        }
        private string _SeletedIngredient;
        public string SeletedIngredient
        {
            get => _SeletedIngredient;
            set
            {
                _SeletedIngredient = value;
                OnPropertyChanged();
            }
        }

        //Combobox DishesList
        private List<string> _DishesList = DbUsage.GetDishNames();

        public List<string> DishesList
        {
            get => _DishesList;
            set
            {
                _DishesList = value;
            }
        }
        private string _SeletedDish;
        public string SeletedDish
        {
            get => _SeletedDish;
            set
            {
                _SeletedDish = value;
                OnPropertyChanged();
            }
        }

        // Commands
        // Комманда добавления Списания Ингредиента
        private RelayCommand _AddWriteOffIngredientCommand;
        public RelayCommand AddWriteOffIngredientCommand
        {
            get
            {
                return _AddWriteOffIngredientCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        AddToDb addToDb = new AddToDb();
                        addToDb.AddWriteOffIngredient(
                            DbUsage.GetIngredientIdByName(SeletedIngredient),
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                            SeletedIngredient,
                            Count,
                            Sum,
                            Cause
                        );
                        _MainCodeBehind.LoadView(ViewType.WriteOffs);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Щось пішло не так! Перевірте правильність заповнення форми.");
                        throw;
                    }
                });
            }
        }
        // Комманда добавления Списания Блюда
        private RelayCommand _AddWriteOffDishCommand;
        public RelayCommand AddWriteOffDishCommand
        {
            get
            {
                return _AddWriteOffDishCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        AddToDb addToDb = new AddToDb();
                        addToDb.AddWriteOffDish(
                            DbUsage.GetDishIdByName(SeletedDish),
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                            SeletedDish,
                            DishCount,
                            DishSum,
                            Cause
                        );
                        _MainCodeBehind.LoadView(ViewType.WriteOffs);
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

        private RelayCommand _LoadWriteOffsUCCommand;
        public RelayCommand LoadWriteOffsUCCommand
        {
            get
            {
                return _LoadWriteOffsUCCommand ?? new RelayCommand(obj =>
                {
                    _Count = "";
                    _Sum = 0;
                    _Cause = "";
                    _MainCodeBehind.LoadView(ViewType.WriteOffs);
                });
            }
        }

        //private RelayCommand _LoadWriteOffsUCCommand;
        //public RelayCommand LoadWriteOffsUCCommand
        //{
        //    get
        //    {
        //        Count = "";
        //        Sum = 0;
        //        Cause = "";
        //        return _LoadWriteOffsUCCommand ?? new RelayCommand(obj =>
        //        {
        //            _MainCodeBehind.LoadView(ViewType.WriteOffs);
        //        });
        //    }
        //}

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
