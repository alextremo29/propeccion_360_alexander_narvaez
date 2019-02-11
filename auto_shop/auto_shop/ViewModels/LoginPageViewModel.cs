using auto_shop.Helpers;
using auto_shop.Models;
using auto_shop.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace auto_shop.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Properties
        private User _user = new User();

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion
        public LoginPageViewModel()
        {
            LoginCommand = new Command(Login);
        }
        public async void Login()
        {
            IsBusy = true;
            Title = string.Empty;
            try
            {
                if (User.usuario != null)
                {
                    if (User.password != null)
                    {
                        if (User.usuario == "alex" && User.password == "123456")
                        {
                            Settings.IsLoggedIn = true;
                            await Navigation.PushAsync(new MainPage(User));
                        }
                        else
                        {
                            Message = "Usuario o contraseña incorrecta";
                        }
                        IsBusy = false;
                    }
                    else
                    {
                        IsBusy = false;
                        Message = "La contraseña es requerido";
                    }

                }
                else
                {
                    IsBusy = false;
                    Message = "El email es requerido";
                }

            }
            catch (Exception e)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Error de conexión", e.Message, "Ok");
            }
        }
    }
}
