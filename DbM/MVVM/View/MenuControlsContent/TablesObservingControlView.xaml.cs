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
    public partial class TablesObservingControlView : UserControl
    {
        public TablesObservingControlView()
        {
            InitializeComponent();
        }

        public void OnListViewTablesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TablesObservingControlViewModel context = (TablesObservingControlViewModel)DataContext;
            TableObservingContentView contentView = new TableObservingContentView();
            TableModel selected = (TableModel)_listViewTables.SelectedItem;
            TableObservingContentViewModel contentViewModel = new TableObservingContentViewModel(context.User, context.DbName, selected.Name);
            contentView.DataContext = contentViewModel;
            _dataEditionControl.Content = contentView;
        }
    }
}
