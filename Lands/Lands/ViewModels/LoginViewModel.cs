using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        private string email;
        private string password;
        private bool isrunning;
        private bool isenabled;
        private bool isremember;

        #endregion       
        public string Email { get; set; }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning {
            get { return this.isrunning; }
            set { SetValue(ref this.isrunning, value); }
        }
        public bool isRemember {
            get { return this.isremember; }
            set { SetValue(ref this.isremember, value); }
        }
        public ICommand LoginCommand { get { return new RelayCommand (Login); } }
        public bool IsEnabled {
            get { return this.isenabled; }
            set { SetValue(ref this.isenabled, value); }
        }
            


        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error","You must enter an Email","Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an Password", "Accept");
                this.Email = string.Empty;    
                return;
            }

            if(this.Email != "maac@hotmail.com" || this.Password != "mario") {

                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Email or Password Incorrect", "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            await Application.Current.MainPage.DisplayAlert("Ok", "Ingreso...", "Accept");
            return;
        }

        public LoginViewModel()
        {
            this.isRemember = true;
            this.isenabled = true;
        }
    }
}
