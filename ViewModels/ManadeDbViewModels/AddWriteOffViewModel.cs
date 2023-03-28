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
                Sum = DbUsage.GetSum(DbUsage.GetPrimeCost(DbUsage.GetIngredientIdByName(SeletedProduct), SeletedProduct), _Count);
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


        // Commands
        // Комманда добавления Списания
        private RelayCommand _AddWriteOffCommand;
        public RelayCommand AddWriteOffCommand
        {
            get
            {
                return _AddWriteOffCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        AddToDb addToDb = new AddToDb();
                        addToDb.AddWriteOff(
                            DbUsage.GetIngredientIdByName(SeletedProduct),
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                            SeletedProduct,
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
