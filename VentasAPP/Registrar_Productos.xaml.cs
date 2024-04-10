using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentasAPP

{

    public partial class Registrar_Productos : ContentPage

    {



        public Registrar_Productos()

        {

            InitializeComponent();

        }



        public async Task LlamadaGetAsync(string url)

        {

            //Creamos una instancia de HttpClient

            var client = new HttpClient();

            //Asignamos la URL

            client.BaseAddress = new Uri(url);

            //Llamada asíncrona al sitio

            var response = await client.GetAsync(client.BaseAddress);

            //Nos aseguramos de recibir una respuesta satisfactoria

            response.EnsureSuccessStatusCode();

            //Convertimos la respuesta a una variable string

            var cadenaResultante = response.Content.ReadAsStringAsync().Result;

            resultado.Text = cadenaResultante;

            nombre.Text = "";

            descripcion.Text = "";

            cantidad.Text = "";

            precio_cost.Text = "";

            precio_vent.Text = "";

            imagen.Text = "";

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            _ = LlamadaGetAsync("https://appsqlpp.000webhostapp.com/agregar_productos.php?nombre=" + nombre.Text + "&descripcion=" + descripcion.Text + "&cantidad=" + cantidad.Text + "&precio_cost=" + precio_cost.Text + "&precio_vent=" + precio_vent.Text + "&imagen=" + imagen.Text);


        }

    }

}