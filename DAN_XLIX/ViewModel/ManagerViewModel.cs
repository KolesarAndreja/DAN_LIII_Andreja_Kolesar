using DAN_XLIX.Command;
using DAN_XLIX.Service;
using DAN_XLIX.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIX.ViewModel
{
    class ManagerViewModel:ViewModelBase
    {
        Manager managerWindow;
        #region property
        private List<vwStaff> _staffList;
        public List<vwStaff> staffList
        {
            get
            {
                return _staffList;
            }
            set
            {
                _staffList = value;
                OnPropertyChanged("staffList");
            }
        }

        private tblManager _currentManager;
        public tblManager currentManager
        {
            get
            {
                return _currentManager;
            }
            set
            {
                _currentManager = value;
                OnPropertyChanged("currentManager");
            }
        }

        private vwStaff _staff;
        public vwStaff staff
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
        #endregion

        #region constructor
        public ManagerViewModel(Manager open, tblManager man)
        {
            managerWindow = open;
            currentManager = man;
            staffList = Service.Service.GetFloorEmployees(currentManager.floorNumber);
        }
        #endregion


        #region salary
        private ICommand _allSalary;
        public ICommand allSalary
        {
            get
            {
                if (_allSalary == null)
                {
                    _allSalary = new RelayCommand(param => AllSalaryExecute(), param => CanAllSalaryExecute());
                }
                return _allSalary;
            }
        }
        private void AllSalaryExecute()
        {
            try
            {
                if (staffList.Count != 0)
                {
                    AddSalary add = new AddSalary(staffList, currentManager);
                    add.ShowDialog();
                    if ((add.DataContext as AddSalaryViewModel).isUpdated == true)
                    {
                        staffList = Service.Service.GetFloorEmployees(currentManager.floorNumber);
                    }
                }
                else
                {
                    MessageBox.Show("Empty list of yours employees");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAllSalaryExecute()
        {
            return true;
        }








        private ICommand _addSalary;
        public ICommand addSalary
        {
            get
            {
                if (_addSalary == null)
                {
                    _addSalary = new RelayCommand(param => AddSalaryExecute(), param => CanAddSalaryExecute());
                }
                return _addSalary;
            }
        }

        private void AddSalaryExecute()
        {
            try
            {
                if(staff.engagement == "supervising" || staff.engagement == "reporting")
                {
                    AddSalary add = new AddSalary(staff, currentManager);
                    add.ShowDialog();
                    if ((add.DataContext as AddSalaryViewModel).isUpdated == true)
                    {
                        staffList = Service.Service.GetFloorEmployees(currentManager.floorNumber);
                    }
                }
                else
                {
                    MessageBox.Show("You can edit salary only for employee with supervising or reporting engagement");
                }
         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddSalaryExecute()
        {
            return true;
        }

        #endregion

        #region log out
        private ICommand _logOut;
        public ICommand logOut
        {
            get
            {
                if (_logOut == null)
                {
                    _logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return _logOut;
            }
        }

        private void LogOutExecute()
        {
            try
            {
                Login login = new Login();
                managerWindow.Close();
                login.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogOutExecute()
        {
            return true;
        }
        #endregion

       
    }
}
