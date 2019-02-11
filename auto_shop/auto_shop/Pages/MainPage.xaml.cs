using auto_shop.Models;
using auto_shop.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace auto_shop
{
    public partial class MainPage : ContentPage
    {
        private User asesor;
        public MainPage(User user)
        {
            InitializeComponent();
            btn_prospectos_asignados.IsVisible = true;
            txt_nombreusuario.Text = string.Format("{0} ", user.nombreusuario);
            asesor = user;


        }

        protected override void OnAppearing()
        {
            if (asesor.asignar_prospecto == 1)
            {
                btn_prospectos_asignados.IsVisible = false;
            }
        }
        private async void btn_registrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarProspecto(asesor));
        }

        private async void btn_listar_prospecto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BrowserProspecto(asesor));
        }

        private async void btn_prospectos_asignados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BrowserProspectosAsig(asesor));
        }
    }
}
