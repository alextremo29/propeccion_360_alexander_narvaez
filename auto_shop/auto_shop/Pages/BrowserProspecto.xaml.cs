using auto_shop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace auto_shop.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowserProspecto : ContentPage
    {
        private List<Prospecto> _prospecto;
        private User asesor;

        public BrowserProspecto(User user)
        {
            InitializeComponent();
            ListaProspectos.ItemTemplate = new DataTemplate(typeof(ProspectoCell));
            ListaProspectos.RowHeight = 70;
            ListaProspectos.IsPullToRefreshEnabled = true;
            asesor = user;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var datos = new DataAccess())
            {
                var abc = datos.GetSincronizarProspectos();
                _prospecto = new List<Prospecto>(abc);
                ListaProspectos.ItemsSource = _prospecto;
            }

        }
        private async void btn_sincronizar_Clicked(object sender, EventArgs e)
        {
            wait_respuesta.IsRunning = true;
            using (var datos = new DataAccess())
            {
                var prospectos = datos.GetSincronizarProspectos();
                _prospecto = new List<Prospecto>(prospectos);
                foreach (var prospecto in _prospecto)
                {
                    string result;
                    try
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                        string url = string.Format("/login_tablet?metodo=insertar_prospecto&nombre={0}&apellido={1}&correo={2}&telefono={3}&genero={4}&edad={5}&modelo={6}&asesor={7}&financiacion={8}&pedido={9}&razon={10}&habeas={11}&ciudad={12}&ot_ciudad={13}&cotizacion={14}&ficha_tecnica={15}&usuela={16}", prospecto.nombre, prospecto.apellido, prospecto.correo, prospecto.telefono, prospecto.genero, prospecto.edad, prospecto.detalle, prospecto.idasesor, prospecto.financiacion, prospecto.pedido, prospecto.razon_compra, prospecto.habeas_data, prospecto.ciudad, prospecto.ot_ciudad, prospecto.enviar_cotizacion, prospecto.ficha_tecnica, prospecto.usuela);
                        var response = await client.GetAsync(url);
                        result = response.Content.ReadAsStringAsync().Result;

                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                        wait_respuesta.IsRunning = false;
                        return;
                    }
                    var respuesta = JsonConvert.DeserializeObject(result);
                    if (string.IsNullOrEmpty(result) || result == "[0]")
                    {
                        await DisplayAlert("Error", string.Format("No fue posible Sincronizar el prospecto: {0}, por favor intente de nuevo", prospecto.nombre), "Aceptar");
                        return;
                    }
                    else
                    {
                        datos.UpdateProspectoSincronizado(prospecto.idserial);
                        //await DisplayAlert("Exito", string.Format("El prospecto: {0} fue agregado correctamente!", prospecto.nombre), "Aceptar");
                        var prospectos_sincronizados = datos.GetSincronizarProspectos();
                        _prospecto = new List<Prospecto>(prospectos_sincronizados);
                        ListaProspectos.ItemsSource = _prospecto;
                    }
                }
                wait_respuesta.IsRunning = false;
                await DisplayAlert("Exito", string.Format("Los Prospectos fueron sincronizados correctamente!!"), "Aceptar");
            }
        }

        private async void ListaProspectos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var prospectoSelect = e.SelectedItem as Prospecto;
            await Navigation.PushAsync(new EditarProspectoSync(prospectoSelect, asesor));

        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ListaProspectos.ItemsSource = _prospecto;
            }
            else
            {
                ListaProspectos.ItemsSource = _prospecto.Where(x => x.nombre.ToUpper().StartsWith(e.NewTextValue.ToUpper()));
            }
        }

    }
}