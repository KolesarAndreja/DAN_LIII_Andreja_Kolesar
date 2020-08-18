using DAN_XLIX.Command;
using DAN_XLIX.Service;
using DAN_XLIX.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLIX.ViewModel
{
    class AddSalaryViewModel : ViewModelBase
    {
        AddSalary addWindow;
        BackgroundWorker backgroundWorker = new BackgroundWorker()
        {
            WorkerReportsProgress = true
            //WorkerSupportsCancellation = true
        };

        private int percent;
        public int Percent
        {
            get { return this.percent; }
            set
            {
                this.percent = value;
                OnPropertyChanged("Percent");
            }
        }
        //private field and property which are bounded with user interface element for feedback of progress
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        private bool calculateAll = false;

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


        private List<vwStaff> _allEmployees;
        public List<vwStaff> allEmployees
        {
            get
            {
                return _allEmployees;
            }
            set
            {
                _allEmployees = value;
                OnPropertyChanged("allEmployees");
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

        #region constructor
        public AddSalaryViewModel(AddSalary open, vwStaff s, tblManager manager)
        {
            addWindow = open;
            staff = Service.Service.getStaff(s);
            man = manager;
        }

        public AddSalaryViewModel(AddSalary open,  List<vwStaff> allEmp, tblManager manager)
        {
            calculateAll = true;
            addWindow = open;
            man = manager;
            allEmployees = allEmp;
            //adding method to DoWork event
            backgroundWorker.DoWork += BW_DoWork;
            //adding method to ProgressChanged event
            backgroundWorker.ProgressChanged += BW_ProgressChanged;
            //adding method to RunWorkerCompleted event
            backgroundWorker.RunWorkerCompleted += BW_RunWorkerCompleted;
        }

        #endregion

        #region calculate
        public double? CalculateSalary()
        {
            double? i = 0.75 * man.workExperience;
            double s = 0.15 * Service.Service.getQualificationNumber(man.qualificationId);
            string g = Service.Service.getGenderString(staff.genderId);
            double p = g == "M" ? 1.12 : 1.15;
            double? res = 1000 * i * s * p + newX;
            return res;
        }
        #endregion
        #region save
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
                if (calculateAll)
                {
                    
                    backgroundWorker.RunWorkerAsync();

                }
                else
                {
                    staff.salary = (decimal)CalculateSalary();
                    Service.Service.AddStaff(staff);
                }
                
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
        #endregion

        #region background worker methods

        public void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            int listCount = allEmployees.Count;
            if (listCount != 0)
            {
                double result = 100 / listCount;

                for (int i = 1; i <= listCount; i++)
                {
                    Thread.Sleep(1000);
                    staff = Service.Service.getStaff(allEmployees[i - 1]);
                    staff.salary = (decimal)CalculateSalary();
                    Service.Service.AddStaff(staff);
                    //invoking method that raises ProgressChanged event and passing the percentage of processing that is complete
                    backgroundWorker.ReportProgress(Convert.ToInt32(result * i));
                }
            }
        }

        public void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //setting value of user interface elements
            Percent = e.ProgressPercentage;
            Message = e.ProgressPercentage.ToString() + "%";
        }

        public void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //if some error occurred during document printing
            if (e.Error != null)
            {
                Message = e.Error.Message.ToString();
            }
            //if printing successfully finished
            else
            {
                Message = "Printing completed.";
            }
        }

        #endregion

    }
}
