using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
  

namespace Lands
{
    using Views;
    using Xamarin.Forms.Internals;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            this.MainPage = new NavigationPage (new PageLogin());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
