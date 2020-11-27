using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{       
    public class MainViewModel
    {       
        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }

        #region singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }
            else
            {
                return instance;
            }
        }

        #endregion

    }
}
