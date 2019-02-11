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
    public partial class EditarProspectoAsig : ContentPage
    {
        private User asesor;
        private Prospecto prospectoSelect;
        private List<MarcasModel> _marca;
        private List<User> _asesor;
        private List<ModeloModel> _modelos;
        private List<VersionModel> _versiones;
        private List<DetalleVersionModel> _detalles;

        public EditarProspectoAsig(Prospecto prospecto, User user)
        {
            InitializeComponent();
            radio_habeas.IsToggled = prospecto.habeas_data;
            txt_nombre.Text = prospecto.nombre;
            txt_apellido.Text = prospecto.apellido;
            txt_correo.Text = prospecto.correo;
            txt_celular.Text = prospecto.telefono;
            txt_ciudad.SelectedIndex = prospecto.ciudad;
            txt_otra_ciudad.Text = prospecto.ot_ciudad;
            txt_genero.SelectedIndex = prospecto.genero;
            txt_edad.SelectedIndex = prospecto.edad;
            radio_financiacion.IsToggled = prospecto.financiacion;
            radio_pedido.IsToggled = prospecto.pedido;
            radio_cotizacion.IsToggled = prospecto.enviar_cotizacion;
            radio_ficha.IsToggled = prospecto.ficha_tecnica;
            txt_razon.SelectedIndex = prospecto.razon_compra;

            asesor = user;
            prospectoSelect = prospecto;
        }
        protected override void OnAppearing()
        {
            txt_asesor.IsVisible = false;
            base.OnAppearing();
            using (var datos = new DataAccessMarca())
            {
                var abc = datos.GetMarcas();
                _marca = new List<MarcasModel>(abc);
                txt_marca.ItemsSource = _marca;

                var idMarca = _marca.FindIndex(x => x.idserial == prospectoSelect.marca);
                if (prospectoSelect.marca == 99)
                {
                    idMarca = -1;
                }
                txt_marca.SelectedIndex = idMarca;


            }
            if (asesor.asignar_prospecto == 1)
            {
                txt_asesor.IsVisible = true;
                using (var datos = new DataAccessAsesor())
                {
                    var abc = datos.GetAsesores();
                    _asesor = new List<User>(abc);
                    txt_asesor.ItemsSource = _asesor;

                    var idAsesor = _asesor.FindIndex(x => x.idserial == prospectoSelect.idasesor);
                    if (prospectoSelect.marca == 99)
                    {
                        idAsesor = -1;
                    }
                    txt_asesor.SelectedIndex = idAsesor;

                }

            }

        }

        private async void btn_actualizar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre.Text))
            {
                await DisplayAlert("Error", "Debe ingresar El nombre del prospecto", "Aceptar");
                txt_nombre.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_apellido.Text))
            {
                await DisplayAlert("Error", "Debe ingresar El apellido del prospecto", "Aceptar");
                txt_apellido.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_correo.Text))
            {
                await DisplayAlert("Error", "Debe ingresar El correo del prospecto", "Aceptar");
                txt_correo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_celular.Text))
            {
                await DisplayAlert("Error", "Debe ingresar El telefono celular del prospecto", "Aceptar");
                txt_celular.Focus();
                return;
            }

            if (!txt_celular.Text.ToCharArray().All(Char.IsDigit))
            {
                await DisplayAlert("Error", "El celular debe ser numerico", "Aceptar");
                return;
            }

            if (txt_genero.SelectedIndex == 0)
            {
                await DisplayAlert("Error", "Debe seleccionar el genero del prospecto", "Aceptar");
                return;
            }
            if (txt_marca.SelectedIndex < 0)
            {
                await DisplayAlert("Error", "Debe seleccionar la marca de interes del prospecto", "Aceptar");
                return;
            }
            if (txt_edad.SelectedIndex == 0)
            {
                await DisplayAlert("Error", "Debe seleccionar la edad del prospecto", "Aceptar");
                return;
            }
            if (txt_modelo.SelectedIndex < 0)
            {
                await DisplayAlert("Error", "Debe seleccionar un modelo", "Aceptar");
                return;
            }
            if (txt_version.SelectedIndex < 0)
            {
                await DisplayAlert("Error", "Debe seleccionar una version", "Aceptar");
                return;
            }
            if (txt_detalle.SelectedIndex < 0)
            {
                await DisplayAlert("Error", "Debe seleccionar una detalle", "Aceptar");
                return;
            }
            if (txt_ciudad.SelectedIndex == 0)
            {
                await DisplayAlert("Error", "Debe seleccionar una ciudad", "Aceptar");
                return;
            }
            if (Convert.ToString(txt_ciudad.SelectedItem) == "Otra" && string.IsNullOrEmpty(txt_otra_ciudad.Text))
            {
                await DisplayAlert("Error", "Por favor indique ciudad del prospecto", "Aceptar");
                return;
            }
            if (asesor.asignar_prospecto == 1 && txt_asesor.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Por favor debe asignar un asesor", "Aceptar");
                return;
            }

            var idMarca = _marca[txt_marca.SelectedIndex].idserial;
            var idModelo = _modelos[txt_modelo.SelectedIndex].idserial;
            var idVersion = _versiones[txt_version.SelectedIndex].idserial;
            var idDetalle = _detalles[txt_detalle.SelectedIndex].idserial;


            var asesorAsig = asesor.idserial;
            var usuela = asesor.idserial;
            if (asesor.asignar_prospecto == 1)
            {
                asesorAsig = _asesor[txt_asesor.SelectedIndex].idserial;
            }
            Prospecto prospectoact = new Prospecto
            {
                idserial = prospectoSelect.idserial,
                nombre = txt_nombre.Text,
                apellido = txt_apellido.Text,
                correo = txt_correo.Text,
                telefono = txt_celular.Text,
                genero = txt_genero.SelectedIndex,
                edad = txt_edad.SelectedIndex,
                marca = idMarca,
                modelo = idModelo,
                version = idVersion,
                detalle = idDetalle,
                idasesor = asesorAsig,
                usuela = usuela,
                financiacion = radio_financiacion.IsToggled,
                enviar_cotizacion = radio_cotizacion.IsToggled,
                ficha_tecnica = radio_ficha.IsToggled,
                pedido = radio_pedido.IsToggled,
                razon_compra = txt_razon.SelectedIndex,
                sincronizado = false,
                habeas_data = radio_habeas.IsToggled,
                ciudad = txt_ciudad.SelectedIndex,
                ot_ciudad = txt_otra_ciudad.Text
            };
            wait_respuesta.IsRunning = true;
            string result;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://prospeccion360.spfactory.co");
                string url = string.Format("/login_tablet?metodo=actualizar_prospecto&nombre={0}&apellido={1}&correo={2}&telefono={3}&genero={4}&edad={5}&modelo={6}&asesor={7}&financiacion={8}&pedido={9}&razon={10}&habeas={11}&ciudad={12}&ot_ciudad={13}&cotizacion={14}&ficha_tecnica={15}&usuela={16}&idserial={17}", prospectoact.nombre, prospectoact.apellido, prospectoact.correo, prospectoact.telefono, prospectoact.genero, prospectoact.edad, prospectoact.detalle, prospectoact.idasesor, prospectoact.financiacion, prospectoact.pedido, prospectoact.razon_compra, prospectoact.habeas_data, prospectoact.ciudad, prospectoact.ot_ciudad, prospectoact.enviar_cotizacion, prospectoact.ficha_tecnica, prospectoact.usuela, prospectoSelect.idserial);
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
                await DisplayAlert("Error", string.Format("No fue posible Sincronizar el prospecto: {0}, por favor intente de nuevo", prospectoact.nombre), "Aceptar");
                return;
            }
            else
            {
                txt_nombre.Text = string.Empty;
                txt_apellido.Text = string.Empty;
                txt_correo.Text = string.Empty;
                txt_celular.Text = string.Empty;
                txt_otra_ciudad.Text = string.Empty;
                txt_genero.SelectedIndex = 0;
                txt_edad.SelectedIndex = 0;
                txt_marca.SelectedIndex = -1;
                txt_modelo.SelectedIndex = -1;
                txt_version.SelectedIndex = -1;
                txt_detalle.SelectedIndex = -1;
                radio_financiacion.IsToggled = false;
                radio_pedido.IsToggled = false;
                radio_cotizacion.IsToggled = false;
                radio_ficha.IsToggled = false;
                txt_razon.SelectedIndex = 0;
                txt_ciudad.SelectedIndex = 0;


                wait_respuesta.IsRunning = false;
                await DisplayAlert("Confirmación", "Se ha actualizado correctamente el prospecto", "Aceptar");
                await Navigation.PushAsync(new BrowserProspectosAsig(asesor));

            }



        }

        private void txt_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            var marca = (Picker)sender;
            var idMarca = 0;
            int marcaSelected = marca.SelectedIndex;
            if (marcaSelected > -1)
            {
                idMarca = _marca[marcaSelected].idserial;
                using (var datos = new DataAccessModelo())
                {
                    var abc = datos.GetModelos(idMarca);
                    _modelos = new List<ModeloModel>(abc);
                    txt_modelo.ItemsSource = _modelos;

                    var idModelo = _modelos.FindIndex(x => x.idserial == prospectoSelect.modelo);
                    if (prospectoSelect.modelo == 99)
                    {
                        idModelo = -1;
                    }
                    txt_modelo.SelectedIndex = idModelo;

                }

            }
        }

        private void txt_modelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var modelo = (Picker)sender;
            var idModelo = 0;
            int modeloSelected = modelo.SelectedIndex;
            if (modeloSelected > -1)
            {
                idModelo = _modelos[modeloSelected].idserial;
                using (var datos = new DataAccessVersion())
                {
                    var abc = datos.Getversiones(idModelo);
                    _versiones = new List<VersionModel>(abc);
                    txt_version.ItemsSource = _versiones;

                    var idVersion = _versiones.FindIndex(x => x.idserial == prospectoSelect.version);
                    if (prospectoSelect.version == 99)
                    {
                        idVersion = -1;
                    }
                    txt_version.SelectedIndex = idVersion;
                }

            }
        }

        private void txt_version_SelectedIndexChanged(object sender, EventArgs e)
        {
            var version = (Picker)sender;
            var idVersion = 0;
            int versionSelected = version.SelectedIndex;
            if (versionSelected > -1)
            {
                idVersion = _versiones[versionSelected].idserial;
                using (var datos = new DataAccessDetalleVersion())
                {
                    var abc = datos.GetDetalles(idVersion);
                    _detalles = new List<DetalleVersionModel>(abc);
                    txt_detalle.ItemsSource = _detalles;

                    var idDetalle = _detalles.FindIndex(x => x.idserial == prospectoSelect.detalle);
                    if (prospectoSelect.detalle == 99)
                    {
                        idDetalle = -1;
                    }
                    txt_detalle.SelectedIndex = idDetalle;

                }
                //await DisplayAlert("Exito", string.Format("El marca: {0} fue agregado correctamente!", idVersion), "Aceptar");
            }
        }

        private void txt_ciudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_otra_ciudad.Text = string.Empty;
            var ciudad = (Picker)sender;
            string ciudadSelected = Convert.ToString(ciudad.SelectedItem);
            txt_otra_ciudad.IsVisible = false;
            if (ciudadSelected == "Otra")
            {
                txt_otra_ciudad.IsVisible = true;
            }
        }

        private void radio_cotizacion_Toggled(object sender, ToggledEventArgs e)
        {
            var envio_cotizacion = (Switch)sender;
            radio_ficha.IsToggled = false;
            radio_ficha.IsEnabled = true;
            if (envio_cotizacion.IsToggled)
            {
                radio_ficha.IsToggled = true;
                radio_ficha.IsEnabled = false;
            }
        }




    }
}