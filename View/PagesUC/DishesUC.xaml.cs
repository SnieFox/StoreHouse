using StoreHouse.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace StoreHouse.View
{
    /// <summary>
    /// Логика взаимодействия для DishesUC.xaml
    /// </summary>
    public partial class DishesUC
    {
        public DishesUC()
        {
            InitializeComponent();
            DishesDataGrid.ItemsSource = StoreHouseContext.GetContext().Dishes.ToList();

        }
    }
}
