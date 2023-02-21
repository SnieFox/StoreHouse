using StoreHouse.Model.Commands;
using StoreHouse.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.ViewModels.ManadeDbViewModels
{
    internal class AddIngredientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddIngredientViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            _MainCodeBehind = codeBehind;
        }
    }
}
