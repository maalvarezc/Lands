using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Lands.Models;
using Lands.Services;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LandsViewModel : BaseViewModel
    {

        private ApiService apiService;

        #region Attributes
        private ObservableCollection<Land> lands;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get { return this.Lands; }
            set { SetValue(ref this.lands, value); }
            #endregion

        }

        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.loadLands();
        }

        private async void loadLands()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSucess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.message, "Accept");
                //back por código
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");
            if (!response.IsSucess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.message, "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var list = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(list);
        }
    }
}
