using DAN_XLIX.Command;
using DAN_XLIX.Model;
using DAN_XLIX.Service;
using DAN_XLIX.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLIX.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        Login login;
        private Person _person;
        public Person person
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                OnPropertyChanged("person");
            }
        }

        public LoginViewModel(Login openLogin)
        {
            login = openLogin;
            person = new Person();
        }

        #region Commands
        private ICommand _loginBtn;
        public ICommand loginBtn
        {
            get
            {
                if (_loginBtn == null)
                {
                    _loginBtn = new RelayCommand(LoginExecute, CanLoginExecute);
                }
                return _loginBtn;
            }
        }

        //check who has log in
        private void LoginExecute(object obj)
        {
            person.password = (obj as PasswordBox).Password;
            tblUser currentUser = Service.Service.IsValidUser(person.username, person.password);
            if (currentUser == null)
            {
                if (person.isOwner())
                {
                    Owner o = new Owner();
                    login.Close();
                    o.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.Try again");
                }
            }
            else
            {
                person.id = currentUser.userId;
                switch (person.role)
                {
                    case "staff":
                        tblStaff st = Service.Service.getStaff(currentUser.userId);
                        Staff s = new Staff();
                        login.Close();
                        s.ShowDialog();
                        break;
                    case "manager":
                        tblManager man = Service.Service.getManager(currentUser.userId);
                        Manager m = new Manager(man);
                        login.Close();
                        m.ShowDialog();
                        break;
                }
            }
            
        }

        private bool CanLoginExecute(object obj)
        {
            return true;
        }
        #endregion


    }
}
