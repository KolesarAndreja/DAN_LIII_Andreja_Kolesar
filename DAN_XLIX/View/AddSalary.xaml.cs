using DAN_XLIX.Service;
using DAN_XLIX.ViewModel;
using System.Windows;

namespace DAN_XLIX.View
{
    /// <summary>
    /// Interaction logic for AddSalary.xaml
    /// </summary>
    public partial class AddSalary : Window
    {
        public AddSalary(vwStaff staff, tblManager man)
        {
            InitializeComponent();
            this.DataContext = new AddSalaryViewModel(this, staff,man);
        }
    }
}
