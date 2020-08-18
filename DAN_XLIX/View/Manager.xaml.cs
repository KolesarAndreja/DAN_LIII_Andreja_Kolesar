using DAN_XLIX.Service;
using DAN_XLIX.ViewModel;
using System.Windows;

namespace DAN_XLIX.View
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager(tblManager man)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this, man);
        }
    }
}
