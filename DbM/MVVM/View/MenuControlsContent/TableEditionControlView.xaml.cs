using DbM.MVVM.Model.Data;
using DbM.MVVM.View.Data;
using DbM.MVVM.ViewModel.Data;
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

namespace DbM.MVVM.View.MenuControlsContent
{
    public partial class TableEditionControlView : UserControl
    {
        public TableEditionControlView()
        {
            InitializeComponent();
        }

        public void OnListViewDatabasesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TableEditionControlViewModel context = (TableEditionControlViewModel)DataContext;
            //TableEditionContentView contentView = new TableEditionContentView();
            DatabaseModel selected = (DatabaseModel)_listViewDatabases.SelectedItem;

            context.LoadTablesAsync(selected.Name);
            //TableEditionContentViewModel contentViewModel = new TableEditionContentViewModel(context.User, context.DbName, selected.Name);
            //contentView.DataContext = contentViewModel;
        }

        public void OnListViewTablesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
