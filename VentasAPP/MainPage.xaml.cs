namespace VentasAPP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


public partial class MainPage : ContentPage

{

    class Usuario_Madre

    {

        public int id { get; set; }

        public string nombre { get; set; }

        public string usuario { get; set; }

        public string contrasena { get; set; }

    }

    List<Usuario_Madre> registros;

    HttpClient client;

    public MainPage()

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
        if (!string.IsNullOrWhiteSpace(storeName))
        {
            // Establecer el título de la página con el nombre de la tienda
            Title = storeName;
        }


        client = new HttpClient(); client.MaxResponseContentBufferSize = 256000;

        try

        {

            var Usuario = Preferences.Default.Get("usuario", ""); if (Usuario != "")

            {

                var Contrasena = Preferences.Default.Get("contrasena", "");

                usuario.Text = Usuario; contrasena.Text = Contrasena;

                recordar.IsToggled = true;

            }

        }

        catch

        {

            usuario.Text = "";

            contrasena.Text = "";

        }

    }

    public async Task HttpSample()

    {

        string direccion = "https://appsqlpp.000webhostapp.com/login_madre.php?usuario=" + usuario.Text + "&contrasena=" + contrasena.Text;

        var uri = new Uri(string.Format(direccion, string.Empty));

        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)

        {

            var content = await response.Content.ReadAsStringAsync();

            if (content.Length <= 0)

            {

                await DisplayAlert("Autenticación ", "Lo sentimos no estas registrado", "OK");

            }

            else

            {

                registros = JsonConvert.DeserializeObject<List<Usuario_Madre>>(content);

                string s = "";

                Usuario_Madre usuario = new Usuario_Madre();

                registros.ForEach(delegate (Usuario_Madre x)

                {

                    Console.WriteLine(x.nombre); s = s + x.nombre + "\n"; usuario = x;

                });

                string mensaje = registros.Count + " Registro \n" + s;

                await DisplayAlert("Bienvenido ", usuario.nombre, "OK");

                var inicioPage = new Principal_Madre();

                inicioPage.BindingContext = usuario;

                if (recordar.IsToggled)

                {

                    Preferences.Default.Set("usuario", usuario.usuario);

                    Preferences.Default.Set("contrasena", usuario.contrasena);

                }

                else

                {

                    Preferences.Default.Set("usuario", "");

                    Preferences.Default.Set("contrasena", "");

                }

                await Navigation.PushAsync(inicioPage);

            }



        }

    }
  
    
      
            
    private async void Button_Clicked(object sender, EventArgs e)
    {

        await HttpSample();

    }
    private void BorrarUsuario_Clicked(object sender, EventArgs e)
    {
        usuario.Text = string.Empty;
    }

    private void MostrarContrasena_Clicked(object sender, EventArgs e)
    {
        if (contrasena.IsPassword)
        {
            contrasena.IsPassword = false;
            mostrarContrasena.Text = "Ocultar";
        }
        else
        {
            contrasena.IsPassword = true;
            mostrarContrasena.Text = "Mostrar";
        }
    }

}