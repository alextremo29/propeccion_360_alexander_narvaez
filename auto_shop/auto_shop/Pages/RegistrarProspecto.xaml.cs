using auto_shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace auto_shop.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarProspecto : ContentPage
    {
        private User asesor;
        private List<MarcasModel> _marca;
        private List<User> _asesor;
        private List<ModeloModel> _modelos;
        private List<VersionModel> _versiones;
        private List<DetalleVersionModel> _detalles;

        public RegistrarProspecto(User user)
        {
            InitializeComponent();
            txt_genero.SelectedIndex = 0;
            txt_edad.SelectedIndex = 0;
            txt_razon.SelectedIndex = 0;
            txt_ciudad.SelectedIndex = 0;
            asesor = user;


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

            }
            if (asesor.asignar_prospecto == 1)
            {
                txt_asesor.IsVisible = true;
                using (var datos = new DataAccessAsesor())
                {
                    var abc = datos.GetAsesores();
                    _asesor = new List<User>(abc);
                    txt_asesor.ItemsSource = _asesor;

                }

            }

        }

        private async void btn_registrar_Clicked(object sender, EventArgs e)
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
            Prospecto prospecto = new Prospecto
            {
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
            using (var datos = new DataAccess())
            {
                datos.InsertProspecto(prospecto);
            }
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



            await DisplayAlert("Confirmación", "Se ha agregado correctamente el prospecto", "Aceptar");


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

                }

            }
            //await DisplayAlert("Exito", string.Format("El marca: {0} fue agregado correctamente!", idMarca), "Aceptar");

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

                }
                //await DisplayAlert("Exito", string.Format("El marca: {0} fue agregado correctamente!", idModelo), "Aceptar");

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