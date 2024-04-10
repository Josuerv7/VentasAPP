namespace VentasAPP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Principal_Madre : ContentPage
{
    public Principal_Madre()
    {
        InitializeComponent();
    }
    private void OnButton1Clicked(object sender, EventArgs e)
    {
        // Código para el botón 1
        Navigation.PushAsync(new Clientes());
    }

    private void OnButton2Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Productos());


    }

    private void OnButton3Clicked(object sender, EventArgs e)
    {
        // Código para el botón 3
        Navigation.PushAsync(new Configuracion());
    }

}