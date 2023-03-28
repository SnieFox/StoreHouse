using StoreHouse.ViewModels.ViewSettingMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StoreHouse.ViewModels.ManadeDbViewModels;

namespace StoreHouse.View.ManageDbPages
{
    /// <summary>
    /// Логика взаимодействия для AddDishWindow.xaml
    /// </summary>
    public partial class AddDishWindow
    {
        public AddDishWindow()
        {
            InitializeComponent();
            AddDishesDataGrid.ItemsSource = AddDishViewModel.GetAddDishesList();
        }
    }
}
