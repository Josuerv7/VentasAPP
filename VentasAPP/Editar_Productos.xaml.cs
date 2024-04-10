using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentasAPP

{

    public partial class Editar_Productos : ContentPage

    {




        private int clienteId; // Declaración del campo clienteId

        public Editar_Productos(int clienteId)
        {
            InitializeComponent();
            this.clienteId = clienteId; // Asigna el ID recibido al campo clienteId de la página
            id.Text = "" + clienteId.ToString();
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
            // Verifica si el campo id no está en blanco, ya que es necesario para identificar el registro.
            if (string.IsNullOrWhiteSpace(id.Text))
            {
                resultado.Text = "Por favor, complete el campo 'id'.";
            }
            else
            {
                // Los campos en blanco no se incluirán en la solicitud HTTP.
                string url = "https://appsqlpp.000webhostapp.com/editar_productos.php?id=" + id.Text;

                if (!string.IsNullOrWhiteSpace(nombre.Text))
                {
                    url += "&nombre=" + nombre.Text;
                }

                if (!string.IsNullOrWhiteSpace(descripcion.Text))
                {
                    url += "&descripcion=" + descripcion.Text;
                }

                if (!string.IsNullOrWhiteSpace(cantidad.Text))
                {
                    url += "&cantidad=" + cantidad.Text;
                }

                if (!string.IsNullOrWhiteSpace(precio_cost.Text))
                {
                    url += "&precio_cost=" + precio_cost.Text;
                }

                if (!string.IsNullOrWhiteSpace(precio_vent.Text))
                {
                    url += "&precio_vent=" +precio_vent.Text;
                }

                if (!string.IsNullOrWhiteSpace(imagen.Text))
                {
                    url += "&imagen=" + imagen.Text;
                }

                // Realiza la llamada HTTP solo con los campos no vacíos.
                _ = LlamadaGetAsync(url);
            }
        }


    }

}