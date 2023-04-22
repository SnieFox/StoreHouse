using StoreHouse.Model.Commands;
using StoreHouse.Model.DbContext;
using StoreHouse.Model.Models;
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
    internal class DetailsWriteOffDishViewModel : INotifyPropertyChanged
    {
        //Fields
        private IMainWindowsCodeBehind _MainCodeBehind;

        //ctor
        public DetailsWriteOffDishViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }

        public static List<OutputAddDish> GetDishIngredientList()
        {
            List<OutputAddDish> list = DbUsage.GetAllDishIngById((int)WriteOffsUCViewModel.GetChoosenWriteOffItem().DishId);
            List<OutputAddDish> returnList = new List<OutputAddDish>();
            foreach (var dish in list)
            {
                string[] dishCountSplit = dish.Count.Split('к');
                string dishCount = dishCountSplit[0];
                returnList.Add(new OutputAddDish(dish.Name, $"{dishCount}{DbUsage.GetIngredientUnitByName(dish.Name)}",$"{dish.Sum}грн"));
            }
            return returnList;
        }


        private RelayCommand _LoadEditWriteOffCommand;
        public RelayCommand LoadEditWriteOffCommand
        {
            get
            {
                return _LoadEditWriteOffCommand ?? new RelayCommand(obj =>
                {
                    _MainCodeBehind.LoadView(ViewType.EditWriteOffDish);
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
