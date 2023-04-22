using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.OutputDataModels;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels
{
    internal class DetailsDishViewModel : INotifyPropertyChanged
    {
        private IMainWindowsCodeBehind _MainCodeBehind;
        public DetailsDishViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
        public DetailsDishViewModel() { }

        private static string _DetailsForName = $"Деталі для '{DishesUCViewModel.GetChoosenDishItem().Name}'";
        public string DetailsForName
        {
            get => _DetailsForName;
            set
            {
                _DetailsForName = value;
                OnPropertyChanged();
            }
        }

        private static List<OutputAddDish> _AllDishIngredients = DbUsage.GetDishIngredientList();
        public List<OutputAddDish> AllDishIngredients
        {
            get => _AllDishIngredients;
            set
            {
                _AllDishIngredients = value;
                OnPropertyChanged();
            }
        }

        public static void SetDishIngredientList(List<OutputAddDish> setList)
        {
            _AllDishIngredients = setList;
        }
        

        private RelayCommand _OpenDishesCommand;
        public RelayCommand OpenDishesCommand
        {
            get
            {
                return _OpenDishesCommand ?? new RelayCommand(obj =>
                {
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
