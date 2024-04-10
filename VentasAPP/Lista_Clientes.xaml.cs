using System.Collections.ObjectModel;



namespace VentasAPP

{

    public partial class Lista_Clientes : ContentPage

    {

        public class Usuario

        {

            public int id { get; set; }

            public string nombre { get; set; }

            public string direccion { get; set; }

            public string telefono { get; set; }
            public string correo { get; set; }
            public string foto { get; set; }



        }



        ObservableCollection<Usuario> datos = new ObservableCollection<Usuario>();

        public Lista_Clientes()

        {

            InitializeComponent();

        }

        public async Task llamadaGetJsonAsync(string url)



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



            var jsonResult = response.Content.ReadAsStringAsync().Result;



            // titulo.Text = jsonResult;


            var listado = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(jsonResult);

            //Asignamos el nuevo valor de las propiedades



            datos = listado;



            //titulo.Text = listado.Count.ToString();



            //Console.WriteLine("Numero de usuarios:"+listado.Count);



            milista.ItemsSource = null;



            milista.ItemsSource = datos;



            //foreach (Usuario element in listado.)



        }



        private async void milista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            var myItem = myListView.SelectedItem;
            int index = datos.IndexOf((Usuario)myItem);

            string[] actions = { "Detalles", "Eliminar", "Editar" };

            var result = await DisplayActionSheet("Datos Cliente", "Cancelar", null, actions);

            switch (result)
            {
                case "Detalles":
                    // Mostrar detalles
                    DisplayAlert("Datos Cliente", "Nombre: " + datos[index].nombre + "\nUsuario: " + datos[index].direccion + "\nTelefono: " + datos[index].telefono + "\nCorreo: " + datos[index].correo, "Ok");
                    break;
                case "Eliminar":
                    // Obtener el ID del cliente
                    int id= datos[index].id;

                    // Realizar la solicitud para eliminar el registro
                    string resultadoEliminacion = await EliminarCliente(id);

                    if (resultadoEliminacion == "OK")
                    {
                        // Eliminación exitosa, actualiza la lista u realiza otras acciones necesarias
                        datos.RemoveAt(index);
                    }
                    else
                    {
                        // Error al eliminar el registro
                        await DisplayAlert("Exito", "Cliente eliminado", "Aceptar");
                    }
                    break;
                // En el evento milista_ItemTapped
                case "Editar":
                    // Obtener el ID del cliente
                    int clienteId = datos[index].id;

                    // Navegar a la página de edición y pasar el ID
                    await Navigation.PushAsync(new Editar_Clientes(clienteId));
                    break;

            }
        }

        private async Task<string> EliminarCliente(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                // Configura la URL de tu servicio web de eliminación de clientes
                string apiUrl = $"https://appsqlpp.000webhostapp.com/eliminar_clientes.php?id={id}";

                // Realiza la solicitud GET al servicio web
                var response = await client.GetAsync(apiUrl);

                // Lee la respuesta del servicio web
                string responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }


        private void Button_Clicked(object sender, EventArgs e)



        {



            try



            {



                _ = llamadaGetJsonAsync("https://appsqlpp.000webhostapp.com/listado_clientes.php");



            }

            catch

            {



                DisplayAlert("Mensa", "Error al tratar de conectarse", "ok");


            }

        }

    }

}