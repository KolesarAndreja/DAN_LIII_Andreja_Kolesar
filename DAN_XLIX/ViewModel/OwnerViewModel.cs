using DAN_XLIX.Command;
using DAN_XLIX.Service;
using DAN_XLIX.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLIX.ViewModel
{
    class OwnerViewModel:ViewModelBase
    {
        #region Prop
        Owner owner;
        private tblUser _newEmployee;
        public tblUser newEmployee
        {
            get
            {
                return _newEmployee;
            }
            set
            {
                _newEmployee = value;
                OnPropertyChanged("newEmployee");
            }
        }

        private tblManager _newManager;
        public tblManager newManager
        {
            get
            {
                return _newManager;
            }
            set
            {
                _newManager = value;
                OnPropertyChanged("newManager");
            }
        }

        private tblStaff _newStaff;
        public tblStaff newStaff
        {
            get
            {
                return _newStaff;
            }
            set
            {
                _newStaff = value;
                OnPropertyChanged("newStaff");
            }
        }

        private List<tblGender> _genderList;
        public List<tblGender> genderList
        {
            get
            {
                return _genderList;
            }
            set
            {
                _genderList = value;
                OnPropertyChanged("genderList");
            }
        }

        private List<tblProfessionalQualification> _qualificationList;
        public List<tblProfessionalQualification> qualificationList
        {
            get
            {
                return _qualificationList;
            }
            set
            {
                _qualificationList = value;
                OnPropertyChanged("qualificationList");
            }
        }

        private List<tblEngagement> _engagementList;
        public List<tblEngagement> engagementList
        {
            get
            {
                return _engagementList;
            }
            set
            {
                _engagementList = value;
                OnPropertyChanged("engagementList");
            }
        }

        private tblProfessionalQualification _qualification;
        public tblProfessionalQualification qualification
        { 
            get
            {
                return _qualification;
            }
            set
            {
                _qualification = value;
                OnPropertyChanged("qualification");
            }
        }

        private tblGender _gender;
        public tblGender gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                OnPropertyChanged("gender");
            }
        }

        private tblEngagement _engagement;
        public tblEngagement engagement
        {
            get
            {
                return _engagement;
            }
            set
            {
                _engagement = value;
                OnPropertyChanged("engagement");
            }
        }
        #endregion

        #region constructor
        public OwnerViewModel(Owner openOwner)
        {
            owner = openOwner;
            newEmployee = new tblUser();
            newManager = new tblManager();
            newStaff = new tblStaff();
            qualification = new tblProfessionalQualification();
            engagement = new tblEngagement();
            gender = new tblGender();

            engagementList = Service.Service.GetEngagementList();
            qualificationList = Service.Service.GetQualificationList();
            genderList = Service.Service.GetGenderList();
        }
        #endregion

        #region Visibility
        private Visibility _viewStaff = Visibility.Collapsed;
        public Visibility viewStaff
        {
            get
            {
                return _viewStaff;
            }
            set
            {
                _viewStaff = value;
                OnPropertyChanged("viewStaff");
            }
        }
        private Visibility _viewManager = Visibility.Collapsed;
        public Visibility viewManager
        {
            get
            {
                return _viewManager;
            }
            set
            {
                _viewManager = value;
                OnPropertyChanged("viewManager");
            }
        }
        private ICommand _createStaff;
        public ICommand createStaff
        {
            get
            {
                if (_createStaff == null)
                {
                    _createStaff = new RelayCommand(param => CreateStuffExecute(), param => CanCreateStuffExecute());
                }
                return _createStaff;
            }
        }

        private void CreateStuffExecute()
        {

            try
            {
                
                if(viewManager == Visibility.Visible)
                {
                    viewManager = Visibility.Collapsed;
                }
                viewStaff = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCreateStuffExecute()
        {
            return true;
        }

        private ICommand _createManager;
        public ICommand createManager
        {
            get
            {
                if (_createManager == null)
                {
                    _createManager = new RelayCommand(param => CreateManagerExecute(), param => CanCreateManagerExecute());
                }
                return _createManager;
            }
        }

        private void CreateManagerExecute()
        {

            try
            {
                if(viewStaff == Visibility.Visible)
                {
                    viewStaff = Visibility.Hidden;
                }
                viewManager = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCreateManagerExecute()
        {
            return true;
        }
        #endregion

        #region Commands

        private ICommand _save1;
        public ICommand save1
        {
            get
            {
                if (_save1 == null)
                {
                    _save1 = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return _save1;
            }
        }

        private void SaveExecute(object obj)
        {
            try
            {
                string currentPassword = (obj as PasswordBox).Password;
                newEmployee.password = currentPassword;
                tblUser e = Service.Service.AddUser(newEmployee);
                newManager.userId = e.userId;
                newManager.qualificationId = qualification.id;
                tblManager me = Service.Service.AddManager(newManager);

                if (e != null && me!=null)
                {
                    MessageBox.Show("Manager has been succesfully created!");
                    newEmployee = new tblUser();
                    newManager = new tblManager();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object obj)
        {
            if (!String.IsNullOrEmpty(newEmployee.fullname) && !String.IsNullOrEmpty(newEmployee.email) && !String.IsNullOrEmpty(newEmployee.username) && qualification.id!=0)
            {
                string currentPassword = (obj as PasswordBox).Password;
                if (!String.IsNullOrEmpty(currentPassword))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        private ICommand _save2;
        public ICommand save2
        {
            get
            {
                if (_save2 == null)
                {
                    _save2 = new RelayCommand(Save2Execute, CanSave2Execute);
                }
                return _save2;
            }
        }

        private void Save2Execute(object obj)
        {
            try
            {
                string currentPassword2 = (obj as PasswordBox).Password;
                newEmployee.password = currentPassword2;
                tblUser e = Service.Service.AddUser(newEmployee);
                newStaff.userId = e.userId;
                newStaff.genderId = gender.id;
                newStaff.engegamentId = engagement.id;
                tblStaff me = Service.Service.AddStaff(newStaff);

                if (e != null && me!=null)
                {
                    MessageBox.Show("Staff has been succesfully created!");
                    newEmployee = new tblUser();
                    newStaff = new tblStaff();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSave2Execute(object obj)
        {
            if (!String.IsNullOrEmpty(newEmployee.fullname) && !String.IsNullOrEmpty(newEmployee.email) && !String.IsNullOrEmpty(newEmployee.username) && !String.IsNullOrEmpty(newStaff.citizenship) && gender.id!=0 && engagement.id!=0)
            {
                string currentPassword2 = (obj as PasswordBox).Password;
                if (!String.IsNullOrEmpty(currentPassword2))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
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
                owner.Close();
                login.Show();

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
