namespace VentasAPP;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Productos : ContentPage
{

    public class Usuario

    {

        public int id { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public string cantidad { get; set; }
        public string precio_cost { get; set; }
        public string precio_vent { get; set; }
        public string imagen { get; set; }


    }



    ObservableCollection<Usuario> datos = new ObservableCollection<Usuario>();

    public Productos()

    {

        InitializeComponent();

    }

    private void OnButton1Clicked(object sender, EventArgs e)
    {
        // Código para el botón 1
        Navigation.PushAsync(new Registrar_Productos());
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

        var result = await DisplayActionSheet("Datos Producto", "Cancelar", null, actions);

        switch (result)
        {
            case "Detalles":
                // Mostrar detalles
                DisplayAlert("Datos Productos", "Nombre: " + datos[index].nombre + "\nDescripcion: " + datos[index].descripcion + "\nCantidad: " + datos[index].cantidad + "\nPrecio Costo: " + datos[index].precio_cost + "\nPrecio Venta: " + datos[index].precio_vent , "Ok");
                break;
            case "Eliminar":
                // Obtener el ID del cliente
                int id = datos[index].id;

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
                    await DisplayAlert("Exito", "Producto Eliminado.", "Aceptar");
                }
                break;
            case "Editar":
                // Obtener el ID del cliente
                int clienteId = datos[index].id;

                // Navegar a la página de edición y pasar el ID
                await Navigation.PushAsync(new Editar_Productos(clienteId));
                break;
        }
    }

    private async Task<string> EliminarCliente(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            // Configura la URL de tu servicio web de eliminación de clientes
            string apiUrl = $"https://appsqlpp.000webhostapp.com/eliminar_productos.php?id={id}";

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



            _ = llamadaGetJsonAsync("https://appsqlpp.000webhostapp.com/listado_productos.php");



        }

        catch

        {



            DisplayAlert("Mensa", "Error al tratar de conectarse", "ok");


        }

    }

}





