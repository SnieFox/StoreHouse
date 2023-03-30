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

namespace StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels
{
    class EditSupplyViewModel
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public EditSupplyViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
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
                    _MainCodeBehind.LoadView(ViewType.Supplies);
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
                    _MainCodeBehind.LoadView(ViewType.Supplies);
                });
            }
        }
    }
}
