using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHouse.ViewModels.Interfaces
{
    internal interface IMainWindowsCodeBehind
    {
        /// <summary>
        /// Загрузка нужной View
        /// </summary>
        void LoadView(ViewType typeView);
        void ShowMessage(string v);
    }
}
