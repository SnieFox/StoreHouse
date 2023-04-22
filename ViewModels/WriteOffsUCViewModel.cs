using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.Interfaces;
using StoreHouse.ViewModels.ViewSettingMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.ViewModels
{
    internal class WriteOffsUCViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public WriteOffsUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        public WriteOffsUCViewModel(){}

        //Properties
        // ChosenRemainsItem Field
        private static List<OutputWriteOff> _AllWriteOffs = ViewSettings.GetOutputWriteOffs();
        public List<OutputWriteOff> AllWriteOffs
        {
            get => _AllWriteOffs;
            set
            {
                _AllWriteOffs = value;
                OnPropertyChanged();
            }
        }
        public static void SetAllWriteOffs() => _AllWriteOffs = ViewSettings.GetOutputWriteOffs();

        private static string _SearchBar;
        public string SearchBar
        {
            get => _SearchBar;
            set
            {
                _SearchBar = value;
                OnPropertyChanged();
            }
        }

        private static OutputWriteOff _ChoosenWriteOffItem;
        public OutputWriteOff ChoosenWriteOffItem
        {
            get => _ChoosenWriteOffItem;
            set
            {
                _ChoosenWriteOffItem = value;
                OnPropertyChanged();
            }
        }
        public static OutputWriteOff GetChoosenWriteOffItem()
        {
            return _ChoosenWriteOffItem;
        }

        private static string _WriteOffsCount = Convert.ToString(DbUsage.GetAllWriteOffs().Count);
        public string WriteOffsCount
        {
            get => _WriteOffsCount;
            set
            {
                _WriteOffsCount = value;
                OnPropertyChanged();
            }
        }

        public static void SetWriteOffsCount() => _WriteOffsCount = Convert.ToString(DbUsage.GetAllWriteOffs().Count);
        //Commands
        private RelayCommand _LoadAddWriteOffCommand;
        public RelayCommand LoadAddWriteOffCommand
        {
            get
            {
                return _LoadAddWriteOffCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.AddWriteOff);
                });
            }
        }
        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        private RelayCommand _ShowMessageCommand;
        public RelayCommand ShowMessageCommand
        {
            get
            {
                return _ShowMessageCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.ShowMessage("WriteOffsUC");
                });
            }
        }
        private RelayCommand _SearchButtonCommand;
        public RelayCommand SearchButtonCommand
        {
            get
            {
                return _SearchButtonCommand ?? new RelayCommand(obj =>
                {
                    AllWriteOffs = DbUsage.SearchWriteOffsByName(SearchBar);
                });
            }
        }

        private RelayCommand _LoadEditWriteOffCommand;
        public RelayCommand LoadEditWriteOffCommand
        {
            get
            {
                if (ChoosenWriteOffItem.IngredientId != null)
                {
                    return _LoadEditWriteOffCommand ?? new RelayCommand(obj =>
                    {
                        _MainCodeBehind.LoadView(ViewType.EditWriteOffIngredient);
                    });
                }
                else
                {
                    return _LoadEditWriteOffCommand ?? new RelayCommand(obj =>
                    {
                        _MainCodeBehind.LoadView(ViewType.EditWriteOffDish);
                    });
                }
            }
        }
        private RelayCommand _LoadDetailsWriteOffCommand;
        public RelayCommand LoadDetailsWriteOffCommand
        {
            get
            {
                if (ChoosenWriteOffItem.DishId != null)
                {
                    return _LoadDetailsWriteOffCommand ?? new RelayCommand(obj =>
                    {
                        if(ChoosenWriteOffItem.DishId!=-1)
                            _MainCodeBehind.LoadView(ViewType.DetailsWriteOffDish);
                    });
                }
                else
                {
                    return _LoadDetailsWriteOffCommand ?? new RelayCommand(obj =>
                    {
                        _MainCodeBehind.LoadView(ViewType.DetailsWriteOffIngredient);
                    });
                }
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
