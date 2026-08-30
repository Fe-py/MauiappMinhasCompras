namespace MauiappMinhasCompras.Views;

using MauiappMinhasCompras.Models;
using System.Collections.ObjectModel;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();

    // Inicializa a página ListaProduto
    public ListaProduto()
    {
        // Carrega os elementos definidos no arquivo XAML
        InitializeComponent();
        lst_produtos.ItemsSource  = Lista;
    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();
        tmp.ForEach( i=> Lista.Add(i));
    }

    // Executa quando o botão Adicionar é pressionado
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Abre a página NovoProduto através da navegação
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem caso aconteça algum erro
            DisplayAlert("ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        List<Produto> tmp = await App.Db.Search (q);
        tmp.ForEach(i => Lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = Lista.Sum(i => i.Total);
        String msg = $"O total é {soma:C}";
        DisplayAlert("Total dos produtos", msg, "Ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}