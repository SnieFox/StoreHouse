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
    internal class SuppliesUCViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public SuppliesUCViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        public SuppliesUCViewModel(){}

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
        // ChosenRemainsItem Field
        private static List<OutputSupplies> _AllSupplies = ViewSettings.GetOutputSupplies();
        public List<OutputSupplies> AllSupplies
        {
            get => _AllSupplies;
            set
            {
                _AllSupplies = value;
                OnPropertyChanged();
            }
        }

        public static void SetAllSupplies() => _AllSupplies = ViewSettings.GetOutputSupplies();
        private static OutputSupplies _ChoosenSupplyItem;
        public OutputSupplies ChoosenSupplyItem
        {
            get => _ChoosenSupplyItem;
            set
            {
                _ChoosenSupplyItem = value;
                OnPropertyChanged();
            }
        }
        private static string _SuppliesCount = Convert.ToString(DbUsage.GetAllSupplies().Count);
        public string SuppliesCount
        {
            get => _SuppliesCount;
            set
            {
                _SuppliesCount = value;
                OnPropertyChanged();
            }
        }

        public static void SetSuppliesCount() => _SuppliesCount = Convert.ToString(DbUsage.GetAllSupplies().Count);
        public static OutputSupplies GetChoosenSupplyItem()
        {
            return _ChoosenSupplyItem;
        }

        //Properties
        //Commands
        private RelayCommand _LoadAddSupplyCommand;
        public RelayCommand LoadAddSupplyCommand
        {
            get
            {
                return _LoadAddSupplyCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.AddSupply);
                });
            }
        }
        private RelayCommand _LoadEditSupplyCommand;
        public RelayCommand LoadEditSupplyCommand
        {
            get
            {
                return _LoadEditSupplyCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditSupply);
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
                    AllSupplies = DbUsage.SearchSuppliesByName(SearchBar);
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
                    _MainCodeBehind.ShowMessage("SuppliesUC");
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
