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
using StoreHouse.View;
using StoreHouse.View.ManageDbPages;
using StoreHouse.ViewModels;
using StoreHouse.ViewModels.Interfaces;
using StoreHouse.ViewModels.ManadeDbViewModels;

namespace StoreHouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public enum ViewType
    {
        Main,
        Remains,
        Supplies,
        WriteOffs,
        Dishes,
        Ingredients,
        AddIngredient,
        AddSupply
    }
    public partial class MainWindow : Window, IMainWindowsCodeBehind
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка вьюмодел для кнопок меню
            MenuViewModel vm = new MenuViewModel();
            //даем доступ к этому кодбихайнд
            vm.CodeBehind = this;
            //делаем эту вьюмодел контекстом данных
            this.DataContext = vm;

            //загрузка стартовой View
            LoadView(ViewType.Main);
        }

        public void LoadView(ViewType typeView)
        {
            switch (typeView)
            {
                case ViewType.Main:
                    //загружаем вьюшку, ее вьюмодель
                    MainUC view = new MainUC();
                    MainUCViewModel vm = new MainUCViewModel(this);
                    //связываем их м/собой
                    view.DataContext = vm;
                    //отображаем
                    this.OutputView.Content = view;
                    break;
                case ViewType.Remains:
                    RemainsUC viewRemains = new RemainsUC();
                    RemainsUCViewModel vmRemains = new RemainsUCViewModel(this);
                    viewRemains.DataContext = vmRemains;
                    this.OutputView.Content = viewRemains;
                    break;
                case ViewType.Supplies:
                    SuppliesUC viewSupplies = new SuppliesUC();
                    SuppliesUCViewModel vmSupplies = new SuppliesUCViewModel(this);
                    viewSupplies.DataContext = vmSupplies;
                    this.OutputView.Content = viewSupplies;
                    break;
                case ViewType.WriteOffs:
                    WriteOffsUC viewWriteOffs = new WriteOffsUC();
                    WriteOffsUCViewModel vmWriteOffs = new WriteOffsUCViewModel(this);
                    viewWriteOffs.DataContext = vmWriteOffs;
                    this.OutputView.Content = viewWriteOffs;
                    break;
                case ViewType.Dishes:
                    DishesUC viewDishes = new DishesUC();
                    DishesUCViewModel vmDishes = new DishesUCViewModel(this);
                    viewDishes.DataContext = vmDishes;
                    this.OutputView.Content = viewDishes;
                    break;
                case ViewType.Ingredients:
                    IngredientsUC viewIngredients = new IngredientsUC();
                    IngredientsUCViewModel vmIngredients = new IngredientsUCViewModel(this);
                    viewIngredients.DataContext = vmIngredients;
                    this.OutputView.Content = viewIngredients;
                    break;
                case ViewType.AddIngredient:
                    AddIngredientWindow viewAddIngredients = new AddIngredientWindow();
                    AddIngredientViewModel vmAddIngredients = new AddIngredientViewModel(this);
                    viewAddIngredients.DataContext = vmAddIngredients;
                    this.OutputView.Content = viewAddIngredients;
                    break;
                case ViewType.AddSupply:
                    AddSupplyWindow viewAddSupply = new AddSupplyWindow();
                    AddSupplyViewModel vmAddSupply = new AddSupplyViewModel(this);
                    viewAddSupply.DataContext = vmAddSupply;
                    this.OutputView.Content = viewAddSupply;
                    break;
            }


        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
