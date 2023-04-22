using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.DbContext.Methods;
using System.Windows;
using StoreHouse.Model.DbContext;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels
{
    class EditSupplyViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public EditSupplyViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }


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

        private static string _NewPrimeCost;
        public string NewPrimeCost
        {
            get
            {
                return _NewPrimeCost;
            }
            set
            {
                if (temp == 0)
                {
                    temp++;
                    _NewPrimeCost = value;
                    OnPropertyChanged();
                    NewSum = DbUsage.GetSum(_NewPrimeCost, _Count);

                }
                else
                {
                    _NewPrimeCost = value;
                    OnPropertyChanged();
                    temp = 0;
                }
            }
        }

        private static decimal _NewSum;
        public decimal NewSum
        {
            get
            {
                return _NewSum;
            }
            set
            {
                if (temp == 0)
                {
                    temp++;
                    _NewSum = value;
                    OnPropertyChanged();
                    NewPrimeCost = DbUsage.GetPrimeCost(_NewSum, _Count);

                }
                else
                {
                    _NewSum = value;
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



        //Commands
        /// Delete Supply
        ///
        private RelayCommand _DeleteSupplyCommand;
        public RelayCommand DeleteSupplyCommand
        {
            get
            {
                return _DeleteSupplyCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        DeleteFromDb delete = new DeleteFromDb() ;
                        {
                            delete.DeleteSupply(
                                DbUsage.GetSupplyIdByName(SuppliesUCViewModel.GetChoosenSupplyItem().Product));
                        }
                        _Count = "";
                        _NewSum = 0;
                        _NewPrimeCost = "";
                        SuppliesUCViewModel.SetSuppliesCount();
                        SuppliesUCViewModel.SetAllSupplies();
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

        /// Edit Supply
        ///
        private RelayCommand _EditSupplyCommand;
        public RelayCommand EditSupplyCommand
        {
            get
            {
                return _EditSupplyCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        EditDb edit = new EditDb();
                        {
                            edit.EditSupply(
                                DbUsage.GetSupplyIdByName(SuppliesUCViewModel.GetChoosenSupplyItem().Product),
                                DbUsage.GetIngredientIdByName(SeletedProduct),
                                DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                                SeletedSupplier,
                                SeletedProduct,
                                Count,
                                Comment,
                                NewSum);
                        }
                        _Count = "";
                        _NewSum = 0;
                        _NewPrimeCost = "";
                        SuppliesUCViewModel.SetAllSupplies();
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
        ///
        private RelayCommand _LoadSuppliesUCCommand;
        public RelayCommand LoadSuppliesUCCommand
        {
            get
            {
                return _LoadSuppliesUCCommand ?? new RelayCommand(obj =>
                {
                    _Count = "";
                    _NewSum = 0;
                    _NewPrimeCost = "";
                    SuppliesUCViewModel.SetSuppliesCount();
                    SuppliesUCViewModel.SetAllSupplies();
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
