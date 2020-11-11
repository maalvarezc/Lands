using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.Infrastructure
{
    using ViewModels;
    public class InstantLocator
    {
        #region Properties
        public MainViewModel Main { get; set; }
        #endregion

        public InstantLocator()
        {
            this.Main = new MainViewModel();
        }
    }

}
