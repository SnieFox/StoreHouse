using StoreHouse.Model;
using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext.Methods;
using StoreHouse.Model.Models;
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
    internal class AddSupplyViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddSupplyViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        //Methods
        //Fields
        public int temp = 0;
        private static string _Comment;
        public string Comment
        {
            get => _Comment;
            set
            {
                _Comment = value;
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
                OnPropertyChanged();
            }
        }

        private static string _PrimeCost;
        public string PrimeCost
        {
            get
            {
                return _PrimeCost;
            }
            set
            {
                if (temp==0)
                {
                    temp++;
                    _PrimeCost = value;
                    OnPropertyChanged();
                    Sum = DbUsage.GetSum(_PrimeCost, _Count);
                    
                }
                else
                {
                    _PrimeCost = value;
                    OnPropertyChanged();
                    temp = 0;
                }
            }
        }

        private static decimal _Sum;
        public decimal Sum
        {
            get
            {
                return _Sum;
            }
            set
            {
                if (temp == 0)
                {
                    temp++;
                    _Sum = value;
                    OnPropertyChanged();
                    PrimeCost = DbUsage.GetPrimeCost(_Sum, _Count);

                }
                else
                {
                    _Sum = value;
                    OnPropertyChanged();
                    temp = 0;
                }

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
        //Combobox SupplierList
        private List<string> _SupplierList = new() { "Маркет", "Столичний ринок", "МЕТРО" };

        public List<string> SupplierList
        {
            get => _SupplierList;
            set
            {
                _SupplierList = value;
            }
        }
        private string _SeletedSupplier;
        public string SeletedSupplier
        {
            get => _SeletedSupplier;
            set
            {
                _SeletedSupplier = value;
                OnPropertyChanged();
            }
        }

        // Commands
        // Комманда добавления Поставки
        private RelayCommand _AddSupplyCommand;
        public RelayCommand AddSupplyCommand
        {
            get
            {
                return _AddSupplyCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        AddToDb addToDb = new AddToDb();
                        addToDb.AddSupply(
                            DbUsage.GetSupplyIdByName(SeletedProduct),
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                            SeletedSupplier,
                            SeletedProduct,
                            Count,
                            Comment,
                            Sum
                        );
                        _MainCodeBehind.LoadView(ViewType.Supplies);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Щось пішло не так! Перевірте правильність заповнення форми.");
                        throw;
                    }
                });
            }
        }
        /// Переход ко Supplies вьюшке
        private RelayCommand _LoadSuppliesUCCommand;
        public RelayCommand LoadSuppliesUCCommand
        {
            get
            {
                Count = "";
                Sum = 0;
                PrimeCost = "";
                return _LoadSuppliesUCCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.Supplies);
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
