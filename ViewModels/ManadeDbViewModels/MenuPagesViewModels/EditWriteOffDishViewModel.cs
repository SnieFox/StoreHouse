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

namespace StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels
{
    internal class EditWriteOffDishViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public EditWriteOffDishViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

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

        //Commands

        private RelayCommand _DeleteWriteOffCommand;
        public RelayCommand DeleteWriteOffCommand
        {
            get
            {
                return _DeleteWriteOffCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        DeleteFromDb delete = new DeleteFromDb();
                        {
                            delete.DeleteWriteOff(
                                DbUsage.GetWriteOffIdByName(WriteOffsUCViewModel.GetChoosenWriteOffItem().Product));
                        }
                        _Count = "";
                        _SeletedDish = null;
                        _SeletedIngredient = null;
                        _Cause = "";
                        WriteOffsUCViewModel.SetWriteOffsCount();
                        WriteOffsUCViewModel.SetAllWriteOffs();
                        _MainCodeBehind.LoadView(ViewType.WriteOffs);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Щось пішло не так! Перевірте правильність заповнення форми.");
                    }
                });
            }
        }

        /// Edit WriteOffIngredient
        private RelayCommand _EditWriteOffIngredientCommand;
        public RelayCommand EditWriteOffIngredientCommand
        {
            get
            {
                return _EditWriteOffIngredientCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        EditDb edit = new EditDb();
                        {
                            edit.EditWriteOffIngredient(
                                DbUsage.GetWriteOffIdByName(WriteOffsUCViewModel.GetChoosenWriteOffItem().Product),
                                DbUsage.GetIngredientIdByName(SeletedIngredient),
                                SeletedIngredient,
                                Count,
                                Sum,
                                Cause);
                        }
                        _Count = "";
                        _SeletedIngredient = null;
                        _Sum = 0;
                        WriteOffsUCViewModel.SetAllWriteOffs();
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
        /// Edit WriteOffDish
        private RelayCommand _EditWriteOffDishCommand;
        public RelayCommand EditWriteOffDishCommand
        {
            get
            {
                return _EditWriteOffDishCommand ?? new RelayCommand(obj =>
                {
                    try
                    {
                        if (WriteOffsUCViewModel.GetChoosenWriteOffItem().DishId != -1)
                        {
                            EditDb edit = new EditDb();
                            {
                                edit.EditWriteOffDish(
                                    DbUsage.GetWriteOffIdByName(WriteOffsUCViewModel.GetChoosenWriteOffItem().Product),
                                    DbUsage.GetDishIdByName(SeletedDish),
                                    SeletedDish,
                                    DishCount,
                                    DishSum,
                                    Cause);
                            }
                            _Count = "";
                            _SeletedIngredient = null;
                            _Sum = 0;
                            WriteOffsUCViewModel.SetAllWriteOffs();
                            _MainCodeBehind.LoadView(ViewType.WriteOffs);
                        }
                        else
                        {
                            MessageBox.Show("Недоступно, страва була видалена");
                        }
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
        ///
        private RelayCommand _LoadWriteOffUCCommand;
        public RelayCommand LoadWriteOffUCCommand
        {
            get
            {
                return _LoadWriteOffUCCommand ?? new RelayCommand(obj =>
                {
                    //_Count = "";
                    //_NewSum = 0;
                    //_NewPrimeCost = "";
                    WriteOffsUCViewModel.SetWriteOffsCount();
                    WriteOffsUCViewModel.SetAllWriteOffs();
                    _MainCodeBehind.LoadView(ViewType.WriteOffs);
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
