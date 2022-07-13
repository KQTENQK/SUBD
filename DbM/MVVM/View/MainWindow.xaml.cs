using DbM.MVVM.Model.Menu;
using DbM.MVVM.ViewModel;
using DbM.MVVM.ViewModel.MenuControlsContent;
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

namespace DbM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void OnMaximaziButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnMenuItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MenuItemModel selected = (MenuItemModel)_listViewMenu.SelectedItem;
            MainViewModel context = (MainViewModel)DataContext;
            selected.DbName = context.DbName;
            selected.User = context.User;
            UserControl view = selected.RelatedViewContent;
            ViewModelControlContent viewModel = selected.RelatedViewModelContent;
            _menuContentControl.Content = view;
            view.DataContext = viewModel;
        }
    }
}
