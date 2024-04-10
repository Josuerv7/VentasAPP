namespace VentasAPP

{

    public partial class Registrar_Clientes : ContentPage

    {



        public Registrar_Clientes()

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

            direccion.Text = "";

            telefono.Text = "";

            correo.Text = "";

            foto.Text = "";

        }

        private void Button_Clicked(object sender, EventArgs e)

        {

            _ = LlamadaGetAsync("https://appsqlpp.000webhostapp.com/agregar_clientes.php?nombre=" + nombre.Text + "&direccion=" + direccion.Text + "&telefono=" + telefono.Text + "&correo=" + correo.Text + "&foto=" + foto.Text);


        }

    }

}