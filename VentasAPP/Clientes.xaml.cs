
namespace VentasAPP;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Clientes : ContentPage
{
	public Clientes()
	{
		InitializeComponent();
	}
    private void OnButton1Clicked(object sender, EventArgs e)
    {
        // C�digo para el bot�n 1
        Navigation.PushAsync(new Registrar_Clientes());
    }

    private void OnButton2Clicked(object sender, EventArgs e)
    {
        // C�digo para el bot�n 2
        Navigation.PushAsync(new Lista_Clientes());
    }



}