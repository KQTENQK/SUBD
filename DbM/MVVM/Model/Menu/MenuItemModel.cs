using DbM.MVVM.ViewModel.MenuControlsContent;
using DbM.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DbM.MVVM.Model.Menu
{
    public abstract class MenuItemModel
    {
        public User.User User { get; set; }
        public string DbName { get; set; }
        public string ItemName { get; protected set; }

        public abstract UserControl RelatedViewContent { get; }
        public abstract ViewModelControlContent RelatedViewModelContent { get; }
    }
}
