using DbM.MVVM.View.MenuControlsContent;
using DbM.MVVM.ViewModel.MenuControlsContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DbM.MVVM.Model.Menu
{
    public class TablesObservingMenuItemModel : MenuItemModel
    {
        public override UserControl RelatedViewContent => new TablesObservingControlView();

        public override ViewModelControlContent RelatedViewModelContent => new TablesObservingControlViewModel(base.User, base.DbName);

        public TablesObservingMenuItemModel()
        {
            ItemName = "Tables";
        }
    }
}
