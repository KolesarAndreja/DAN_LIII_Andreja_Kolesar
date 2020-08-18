using DAN_XLIX.Command;
using DAN_XLIX.Service;
using DAN_XLIX.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLIX.ViewModel
{
    class AddSalaryViewModel : ViewModelBase
    {
        AddSalary addWindow;

        private tblStaff _staff;
        public tblStaff staff
        {
            get
            {
                return _staff;
            }
            set
            {
                _staff = value;
                OnPropertyChanged("staff");
            }
        }


        private tblManager _man;
        public tblManager man
        {
            get
            {
                return _man;
            }
            set
            {
                _man = value;
                OnPropertyChanged("man");
            }
        }

        private int _newX = 0;
        public int newX
        {
            get
            {
                return _newX;
            }
            set
            {
                _newX = value;
                OnPropertyChanged("newX");
            }
        }


        private bool _isUpdated;
        public bool isUpdated
        {
            get
            {
                return _isUpdated;
            }
            set
            {
                _isUpdated = value;
            }
        }

        public AddSalaryViewModel(AddSalary open, vwStaff s, tblManager manager)
        {
            addWindow = open;
            staff = Service.Service.getStaff(s);
            man = manager;
        }

        public double? CalculateSalary()
        {
            double? i = 0.75 * man.workExperience;
            double s = 0.15 * Service.Service.getQualificationNumber(man.qualificationId);
            string g = Service.Service.getGenderString(staff.genderId);
            double p = g == "M" ? 1.12 : 1.15;
            double? res = 1000 * i * s * p + newX;
            return res;
        }


        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return _save;
            }
        }

        private void SaveExecute(object obj)
        {
            try
            {
                staff.salary = (decimal)CalculateSalary();
                Service.Service.AddStaff(staff);
                isUpdated = true;
                addWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object obj)
        {
            string currentText = (obj as TextBox).Text;
            bool b = int.TryParse(currentText, out int n);
            if (!b || n<2 || n>999)
            {
                return false;
            }
            return true;

        }

    }
}
