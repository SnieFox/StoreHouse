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
using StoreHouse.View.ManageDbPages.MenuPages;
using StoreHouse.ViewModels;
using StoreHouse.ViewModels.Interfaces;
using StoreHouse.ViewModels.ManadeDbViewModels;
using StoreHouse.ViewModels.ManadeDbViewModels.MenuPagesViewModels;

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
        AddSupply,
        AddWriteOff,
        AddDish,
        AddIngredientToDish,
        IngredientSupply,
        EditSupply,
        EditWriteOffIngredient,
        EditWriteOffDish,
        DetailsWriteOffIngredient,
        DetailsWriteOffDish,
        ChangePrimeCost,
        EditDishes,
        DetailsDishes,
        EditCountInDishIngr,
        EditIngredientInDish
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
                case ViewType.AddWriteOff:
                    AddWriteOffWindow viewAddWriteOff = new AddWriteOffWindow();
                    AddWriteOffViewModel vmAddWriteOff = new AddWriteOffViewModel(this);
                    viewAddWriteOff.DataContext = vmAddWriteOff;
                    this.OutputView.Content = viewAddWriteOff;
                    break;
                case ViewType.AddDish:
                    AddDishWindow viewAddDish = new AddDishWindow();
                    AddDishViewModel vmAddDish = new AddDishViewModel(this);
                    viewAddDish.DataContext = vmAddDish;
                    this.OutputView.Content = viewAddDish;
                    break;
                case ViewType.AddIngredientToDish:
                    AddIngredientToDishWindow viewAddIngredientToDish = new AddIngredientToDishWindow();
                    AddIngredientToDishViewModel vmAddIngredientToDish = new AddIngredientToDishViewModel(this);
                    viewAddIngredientToDish.DataContext = vmAddIngredientToDish;
                    this.OutputView.Content = viewAddIngredientToDish;
                    break;
                case ViewType.IngredientSupply:
                    IngredientSuppliesWindow viewIngredientSuppliesWindow = new IngredientSuppliesWindow();
                    IngredientSuppliesViewModel vmIngredientSuppliesWindow = new IngredientSuppliesViewModel(this);
                    viewIngredientSuppliesWindow.DataContext = vmIngredientSuppliesWindow;
                    this.OutputView.Content = viewIngredientSuppliesWindow;
                    break;
                case ViewType.EditSupply:
                    EditSupplyWindow viewEditSupplyWindow = new EditSupplyWindow();
                    EditSupplyViewModel vmEditSupplyWindow = new EditSupplyViewModel(this);
                    viewEditSupplyWindow.DataContext = vmEditSupplyWindow;
                    this.OutputView.Content = viewEditSupplyWindow;
                    break;
                case ViewType.EditWriteOffIngredient:
                    EditWriteOffIngredientWindow viewEditWriteOffIngredientWindow = new EditWriteOffIngredientWindow();
                    EditWriteOffViewModel vmEditWriteOffIngredientWindow = new EditWriteOffViewModel(this);
                    viewEditWriteOffIngredientWindow.DataContext = vmEditWriteOffIngredientWindow;
                    this.OutputView.Content = viewEditWriteOffIngredientWindow;
                    break;
                case ViewType.EditWriteOffDish:
                    EditWriteOffDishWindow viewEditWriteOffDishWindow = new EditWriteOffDishWindow();
                    EditWriteOffViewModel vmEditWriteOffDishWindow = new EditWriteOffViewModel(this);
                    viewEditWriteOffDishWindow.DataContext = vmEditWriteOffDishWindow;
                    this.OutputView.Content = viewEditWriteOffDishWindow;
                    break;
                case ViewType.DetailsWriteOffIngredient:
                    DetailsWriteOffIngredientWindow viewDetailsWriteOffIngredientWindow = new DetailsWriteOffIngredientWindow();
                    DetailsWriteOffIngredientViewModel vmDetailsWriteOffIngredientWindow = new DetailsWriteOffIngredientViewModel(this);
                    viewDetailsWriteOffIngredientWindow.DataContext = vmDetailsWriteOffIngredientWindow;
                    this.OutputView.Content = viewDetailsWriteOffIngredientWindow;
                    break;
                case ViewType.DetailsWriteOffDish:
                    DetailsWriteOffDishWindow viewDetailsWriteOffDishWindow = new DetailsWriteOffDishWindow();
                    DetailsWriteOffDishViewModel vmDetailsWriteOffDishWindow = new DetailsWriteOffDishViewModel(this);
                    viewDetailsWriteOffDishWindow.DataContext = vmDetailsWriteOffDishWindow;
                    this.OutputView.Content = viewDetailsWriteOffDishWindow;
                    break;
                case ViewType.ChangePrimeCost:
                    ChangePrimeCostWindow viewChangePrimeCostWindow = new ChangePrimeCostWindow();
                    IngredientsUCViewModel vmChangePrimeCostWindow = new IngredientsUCViewModel(this);
                    viewChangePrimeCostWindow.DataContext = vmChangePrimeCostWindow;
                    this.OutputView.Content = viewChangePrimeCostWindow;
                    break;
                case ViewType.EditDishes:
                    EditDishWindow viewEditDishesWindow = new EditDishWindow();
                    EditDishViewModel vmEditDishesWindow = new EditDishViewModel(this);
                    viewEditDishesWindow.DataContext = vmEditDishesWindow;
                    this.OutputView.Content = viewEditDishesWindow;
                    break;
                case ViewType.DetailsDishes:
                    DetailsDishWindow viewDetailsDishesWindow = new DetailsDishWindow();
                    DetailsDishViewModel vmDetailsDishesWindow = new DetailsDishViewModel(this);
                    viewDetailsDishesWindow.DataContext = vmDetailsDishesWindow;
                    this.OutputView.Content = viewDetailsDishesWindow;
                    break;
                case ViewType.EditCountInDishIngr:
                    EditCountIngInDish viewEditCountInDishIngrWindow = new EditCountIngInDish();
                    EditDishViewModel vmEditCountInDishIngrWindow = new EditDishViewModel(this);
                    viewEditCountInDishIngrWindow.DataContext = vmEditCountInDishIngrWindow;
                    this.OutputView.Content = viewEditCountInDishIngrWindow;
                    break;
                case ViewType.EditIngredientInDish:
                    EditIngredientInDishWindow viewEditIngredientInDishWindow = new EditIngredientInDishWindow();
                    EditIngredientInDishViewModel vmEditIngredientInDishWindow = new EditIngredientInDishViewModel(this);
                    viewEditIngredientInDishWindow.DataContext = vmEditIngredientInDishWindow;
                    this.OutputView.Content = viewEditIngredientInDishWindow;
                    break;
            }


        }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
