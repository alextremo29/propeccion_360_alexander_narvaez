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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();


        }

        private async void btn_login_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_usuario.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un usuario", "Aceptar");
                txt_usuario.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_password.Text))
            {
                await DisplayAlert("Error", "Debe ingresar una contraseña", "Aceptar");
                txt_password.Focus();
                return;
            }

            wait_respuesta.IsRunning = true;
            string result;
            try
            {
                btn_login.IsEnabled = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=login&usuario={0}&password={1}", txt_usuario.Text, txt_password.Text);
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;
                btn_login.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                btn_login.IsEnabled = true;
                wait_respuesta.IsRunning = false;
                return;
            }




            if (string.IsNullOrEmpty(result) || result == "[]")
            {
                await DisplayAlert("Error", "Usuario o contraseña no valido", "Aceptar");
                wait_respuesta.IsRunning = false;
                txt_password.Text = string.Empty;
                txt_password.Focus();
                return;
            }
            var user = JsonConvert.DeserializeObject<User>(result);
            //await DisplayAlert("Exito", string.Format("El usuario: {0}, {1} fue agregado correctamente!", user.nombreusuario, user.asignar_prospecto), "Aceptar");
            string resultMarca;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=traer_marcas");
                var response = await client.GetAsync(url);
                resultMarca = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                wait_respuesta.IsRunning = false;
                return;
            }
            var marcas = JsonConvert.DeserializeObject<List<MarcasModel>>(resultMarca);
            using (var datos = new DataAccessMarca())
            {
                datos.DeleteMarca();
                foreach (var marcasRow in marcas)
                {
                    //await DisplayAlert("Exito", string.Format("El marca: {0}, {1} fue agregado correctamente!", marcasRow.idserial, marcasRow.descripcion), "Aceptar");
                    datos.InsertMarca(marcasRow);
                }
            }
            string resultAsesor;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=traer_asesores");
                var response = await client.GetAsync(url);
                resultAsesor = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                wait_respuesta.IsRunning = false;
                return;
            }
            var asesores = JsonConvert.DeserializeObject<List<User>>(resultAsesor);
            using (var datos = new DataAccessAsesor())
            {
                datos.DeleteAsesores();
                foreach (var asesoresRow in asesores)
                {
                    //await DisplayAlert("Exito", string.Format("El marca: {0}, {1} fue agregado correctamente!", asesoresRow.idusuario, asesoresRow.nombreusuario), "Aceptar");
                    datos.InsertAsesor(asesoresRow);
                }
            }


            string resultModelo;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=traer_modelos");
                var response = await client.GetAsync(url);
                resultModelo = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                wait_respuesta.IsRunning = false;
                return;
            }
            var modelos = JsonConvert.DeserializeObject<List<ModeloModel>>(resultModelo);
            using (var datos = new DataAccessModelo())
            {
                datos.DeleteModelos();
                foreach (var modelosRow in modelos)
                {
                    //await DisplayAlert("Exito", string.Format("El marca: {0}, {1} fue agregado correctamente!", modelosRow.idserial, modelosRow.descripcion), "Aceptar");
                    datos.InsertModelo(modelosRow);
                }
            }

            string resultVersion;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=traer_versiones");
                var response = await client.GetAsync(url);
                resultVersion = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                wait_respuesta.IsRunning = false;
                return;
            }
            var versiones = JsonConvert.DeserializeObject<List<VersionModel>>(resultVersion);
            using (var datos = new DataAccessVersion())
            {
                datos.DeleteVersiones();
                foreach (var versionRow in versiones)
                {
                    //await DisplayAlert("Exito", string.Format("El marca: {0}, {1} fue agregado correctamente!", versionRow.idserial, versionRow.descripcion), "Aceptar");
                    datos.InserVersion(versionRow);
                }
            }

            string resultDetalle;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=traer_detalles");
                var response = await client.GetAsync(url);
                resultDetalle = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error de conexión", ex.Message, "Ok");
                wait_respuesta.IsRunning = false;
                return;
            }
            var detalles = JsonConvert.DeserializeObject<List<DetalleVersionModel>>(resultDetalle);
            using (var datos = new DataAccessDetalleVersion())
            {
                datos.DeleteDetalles();
                foreach (var detalleRow in detalles)
                {
                    //await DisplayAlert("Exito", string.Format("El marca: {0}, {1} fue agregado correctamente!", detalleRow.idserial, detalleRow.costo ), "Aceptar");
                    datos.InsertDetalle(detalleRow);
                }
            }

            wait_respuesta.IsRunning = false;
            await Navigation.PushAsync(new MainPage(user));
        }
    }
}