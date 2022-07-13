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
    public class UserEditionMenuItemModel : MenuItemModel
    {
        public override UserControl RelatedViewContent => new UserEditionControlView();

        public override ViewModelControlContent RelatedViewModelContent => new UserEditionControlViewModel(base.User, base.DbName);

        public UserEditionMenuItemModel()
        {
            ItemName = "Edit Users";
        }
    }
}
