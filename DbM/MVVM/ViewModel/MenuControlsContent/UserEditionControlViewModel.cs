using DbM.MVVM.Model.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbM.MVVM.ViewModel.MenuControlsContent
{
    public class UserEditionControlViewModel : ViewModelControlContent
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public UserEditionControlViewModel(User user, string dbName) : base(user, dbName)
        {
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
