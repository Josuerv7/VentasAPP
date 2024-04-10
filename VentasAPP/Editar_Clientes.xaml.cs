using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentasAPP

{

    public partial class Editar_Clientes : ContentPage

    {




        private int clienteId; // Declaración del campo clienteId

        public Editar_Clientes(int clienteId)
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

            id.Text = "";

            nombre.Text = "";

            direccion.Text = "";

            telefono.Text = "";

            correo.Text = "";

            foto.Text = "";


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
                string url = "https://appsqlpp.000webhostapp.com/editar_clientes.php?id=" + id.Text;

                if (!string.IsNullOrWhiteSpace(nombre.Text))
                {
                    url += "&nombre=" + nombre.Text;
                }

                if (!string.IsNullOrWhiteSpace(direccion.Text))
                {
                    url += "&direccion=" + direccion.Text;
                }

                if (!string.IsNullOrWhiteSpace(telefono.Text))
                {
                    url += "&telefono=" + telefono.Text;
                }

                if (!string.IsNullOrWhiteSpace(correo.Text))
                {
                    url += "&correo=" + correo.Text;
                }

                if (!string.IsNullOrWhiteSpace(foto.Text))
                {
                    url += "&foto=" + foto.Text;
                }

                // Realiza la llamada HTTP solo con los campos no vacíos.
                _ = LlamadaGetAsync(url);
            }
        }


    }

}