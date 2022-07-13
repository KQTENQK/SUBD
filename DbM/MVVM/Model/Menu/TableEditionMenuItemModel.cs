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
    public class TableEditionMenuItemModel : MenuItemModel
    {
        public override UserControl RelatedViewContent => new TableEditionControlView();

        public override ViewModelControlContent RelatedViewModelContent => new TableEditionControlViewModel(base.User, base.DbName);

        public TableEditionMenuItemModel()
        {
            ItemName = "Edit Tables Structure";
        }
    }
}
