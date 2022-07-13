using DbM.MVVM.ViewModel.MenuControlsContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DbM.MVVM.Model.TableEditionControlMenu
{
    public class DatabasesMenuItemModel : TableEditionControlMenuItemModel
    {
        public override UserControl RelatedViewContent => throw new NotImplementedException();

        public override ViewModelControlContent RelatedViewModelContent => throw new NotImplementedException();

        public DatabasesMenuItemModel()
        {
            ItemName = "DatabaseNamePlaceHolder";
        }
    }
}
