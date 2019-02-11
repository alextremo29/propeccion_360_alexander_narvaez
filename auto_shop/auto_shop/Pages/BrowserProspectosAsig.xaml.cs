using auto_shop.ViewModels;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using auto_shop.Models;

namespace auto_shop.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowserProspectosAsig : ContentPage
    {
        private List<Prospecto> _prospecto;
        private User asesor;

        public BrowserProspectosAsig(User user)
        {
            InitializeComponent();
            ListaProspectosAsig.ItemTemplate = new DataTemplate(typeof(ProspectoCell));
            ListaProspectosAsig.RowHeight = 70;
            asesor = user;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            wait_respuesta.IsRunning = true;
            string resultProspectos;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=traer_prospectos_asig&asesor={0}", asesor.idserial);
                var response = await client.GetAsync(url);
                resultProspectos = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                wait_respuesta.IsRunning = false;
                return;
            }
            _prospecto = JsonConvert.DeserializeObject<List<Prospecto>>(resultProspectos);
            ListaProspectosAsig.ItemsSource = _prospecto;
            wait_respuesta.IsRunning = false;

        }

        private async void ListaProspectosAsig_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            wait_respuesta.IsRunning = true;
            var prospectoSelect = e.SelectedItem as Prospecto;
            await Navigation.PushAsync(new EditarProspectoAsig(prospectoSelect, asesor));
            wait_respuesta.IsRunning = false;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ListaProspectosAsig.ItemsSource = _prospecto;
            }
            else
            {
                ListaProspectosAsig.ItemsSource = _prospecto.Where(x => x.nombre.ToUpper().StartsWith(e.NewTextValue.ToUpper()));
            }
        }
    }
}