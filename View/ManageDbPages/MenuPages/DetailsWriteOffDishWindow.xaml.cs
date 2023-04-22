using StoreHouse.ViewModels.ManadeDbViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels;

namespace StoreHouse.View.ManageDbPages.MenuPages
{
    /// <summary>
    /// Логика взаимодействия для DetailsWriteOffDishWindow.xaml
    /// </summary>
    public partial class DetailsWriteOffDishWindow : UserControl
    {
        public DetailsWriteOffDishWindow()
        {
            InitializeComponent();
            WriteOffDetailsDataGrid.ItemsSource = DetailsWriteOffDishViewModel.GetDishIngredientList();

        }
    }
}
