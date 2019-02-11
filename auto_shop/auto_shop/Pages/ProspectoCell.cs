using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace auto_shop.Pages
{
    public class ProspectoCell : ViewCell
    {
        public ProspectoCell()
        {

            var nombreCompletoLabel = new Label
            {
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromHex("#474747")
            };
            nombreCompletoLabel.SetBinding(Label.TextProperty, new Binding("nombreCompleto"));

            //var sincronizadoLabel = new Label
            //{
            //    FontSize = 15,
            //    FontAttributes = FontAttributes.Bold,
            //    HorizontalTextAlignment = TextAlignment.Start,
            //    HorizontalOptions = LayoutOptions.Start,
            //    TextColor = Color.FromHex("#474747")
            //};
            //sincronizadoLabel.SetBinding(Label.TextProperty, new Binding("sincronizado"));

            var correoProspectoLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 15,
                TextColor = Color.FromHex("#474747")
            };
            correoProspectoLabel.SetBinding(Label.TextProperty, new Binding("correo"));
            var stackDatosCliente = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children ={
                    nombreCompletoLabel,correoProspectoLabel
                }
            };
            var telefonoProspectoLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.End,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#0C67C2")
            };
            telefonoProspectoLabel.SetBinding(Label.TextProperty, new Binding("telefono"));
            var stackProspecto = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    stackDatosCliente,telefonoProspectoLabel
                }
            };
            View = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                    stackProspecto
                }
            };
        }
    }
}