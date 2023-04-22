using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext.Methods;
using StoreHouse.Model.Models;
using StoreHouse.ViewModels.ViewSettingMethods;

namespace StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels
{
    internal class EditDishViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;

        public EditDishViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        public EditDishViewModel()
        {
        }

        private static string _EditDishHeader = $"Редагувати {DishesUCViewModel.GetChoosenDishItem().Name}";
        public string EditDishHeader
        {
            get => _EditDishHeader;
            private set
            {
                _EditDishHeader = value;
                OnPropertyChanged();
            }
        }

        private static string _NewCount;
        public string NewCount
        {
            get => _NewCount;
            set
            {
                _NewCount = value;
                OnPropertyChanged();
            }
        }
        private static string _NewPrice;
        public string NewPrice
        {
            get => _NewPrice;
            set
            {
                _NewPrice = value;
                OnPropertyChanged();
            }
        }

        private static OutputAddDish _ChoosenEditDishItem;
        public OutputAddDish ChoosenEditDishItem
        {
            get => _ChoosenEditDishItem;
            set
            {
                _ChoosenEditDishItem = value;
                OnPropertyChanged();
            }
        }
        public static OutputAddDish GetChoosenEditDishItem() => _ChoosenEditDishItem;
        

        private static List<OutputAddDish> _AllDishesIngredients = DbUsage.GetDishIngredientList();
        public List<OutputAddDish> AllDishesIngredients
        {
            get => _AllDishesIngredients;
            set
            {
                _AllDishesIngredients = value;
                OnPropertyChanged();
            }
        }

        public static void SetAllDishesIngredients(List<OutputAddDish> list)
        {
            _AllDishesIngredients = list;
        }

        private RelayCommand _AddIngredientToDish;
        public RelayCommand AddIngredientToDish
        {
            get
            {
                return _AddIngredientToDish ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditIngredientInDish);
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
                    _MainCodeBehind.LoadView(ViewType.EditDishes);
                });
            }
        }
        private RelayCommand _ChangeCount;
        public RelayCommand ChangeCount
        {
            get
            {
                return _ChangeCount ?? new RelayCommand(obj =>
                {
                    EditDb edit = new EditDb();
                    {
                        edit.EditDishIngredientCount(
                            DbUsage.GetDishIdByName(DishesUCViewModel.GetChoosenDishItem().Name),
                            _ChoosenEditDishItem.Name,
                            _NewCount
                        );
                    }
                    _AllDishesIngredients = DbUsage.GetDishIngredientList();
                    _MainCodeBehind.LoadView(ViewType.EditDishes);
                });
            }
        }
        private RelayCommand _NewPriceCommand;
        public RelayCommand NewPriceCommand
        {
            get
            {
                return _NewPriceCommand ?? new RelayCommand(obj =>
                {
                    EditDb edit = new EditDb();
                    {
                        edit.EditDishPrice(
                            DbUsage.GetDishIdByName(DishesUCViewModel.GetChoosenDishItem().Name),
                            _NewPrice
                        );
                    }
                    DishesUCViewModel.SetAllDishes();
                    _MainCodeBehind.LoadView(ViewType.Dishes);
                });
            }
        }

        private RelayCommand _EditCountOfIngr;
        public RelayCommand EditCountOfIngr
        {
            get
            {
                return _EditCountOfIngr ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditCountInDishIngr);
                });
            }
        }

        private RelayCommand _DeleteDishIngr;
        public RelayCommand DeleteDishIngr
        {
            get
            {
                return _DeleteDishIngr ?? new RelayCommand(obj =>
                {
                    DeleteFromDb delete = new DeleteFromDb();
                    {
                        delete.DeleteDishIngr(
                            DbUsage.GetOutputAddDishesIdByName(_ChoosenEditDishItem.Name, DbUsage.GetDishIdByName(DishesUCViewModel.GetChoosenDishItem().Name))
                        );
                    }
                    AllDishesIngredients = DbUsage.GetDishIngredientList();
                });
            }
        }

        private RelayCommand _DeleteDishCommand;
        public RelayCommand DeleteDishCommand
        {
            get
            {
                return _DeleteDishCommand ?? new RelayCommand(obj =>
                {
                    DeleteFromDb delete = new DeleteFromDb();
                    {
                        delete.DeleteDish(
                            DbUsage.GetDishIdByName(DishesUCViewModel.GetChoosenDishItem().Name)
                        );
                    }
                    DishesUCViewModel.SetDishesCount();
                    DishesUCViewModel.SetAllDishes();
                    WriteOffsUCViewModel.SetAllWriteOffs();
                    WriteOffsUCViewModel.SetWriteOffsCount();
                    _MainCodeBehind.LoadView(ViewType.Dishes);
                });
            }
        }
        private RelayCommand _OpenDishesCommand;
        public RelayCommand OpenDishesCommand
        {
            get
            {
                return _OpenDishesCommand ?? new RelayCommand(obj =>
                {
                    DishesUCViewModel.SetDishesCount();
                    DishesUCViewModel.SetAllDishes();
                    _MainCodeBehind.LoadView(ViewType.Dishes);
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
