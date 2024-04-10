using Microsoft.Maui.Controls;
using System;


namespace VentasAPP
{
    public partial class Configuracion : ContentPage
    {
        public Configuracion()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            string savedImageURL = Preferences.Get("ImagenURL", "");
            string storeName = Preferences.Get("NombreTienda", "");

            if (!string.IsNullOrWhiteSpace(savedImageURL))
            {
                // Establecer la URL guardada como fuente de la imagen
                imagen.Source = new UriImageSource { Uri = new Uri(savedImageURL) };
            }
        }
private void CargarNombre_Clicked(object sender, EventArgs e)
        {
            string storeName = storeNameEntry.Text;

            if (!string.IsNullOrWhiteSpace(storeName))
            {
                // Guardar el nombre de la tienda en Preferencias
                Preferences.Set("NombreTienda", storeName);
                hechoLabel.Text = "Hecho";
            }
        }
    
        private void CargarImagen_Clicked(object sender, EventArgs e)
        {
            string imageURL = imageURLEntry.Text;

            if (!string.IsNullOrWhiteSpace(imageURL))
            {
                // Actualizar la fuente de la imagen con la URL ingresada
                imagen.Source = new UriImageSource { Uri = new Uri(imageURL) };

                // Guardar la URL de la imagen en Preferencias
                Preferences.Set("ImagenURL", imageURL);
            }
        }
    }
}