namespace PM2E3VNRO.Views;
using PM2E3VNRO.Controllers;
using PM2E3VNRO.Models;
using System.Windows.Input;
public partial class Listado : ContentPage
{
    public Listado()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        viewListado.Header = "Cargando...";

        var notas = await App.db.SelectAll();
        viewListado.ItemsSource = notas.OrderByDescending(nota => nota.Fecha).ToList();

        viewListado.Header = string.Empty;
    }


    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Notas nota = args.SelectedItem as Notas;
        await Navigation.PushAsync(new Detalle(nota));
    }
}