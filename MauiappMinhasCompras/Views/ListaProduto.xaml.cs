namespace MauiappMinhasCompras.Views;

using MauiappMinhasCompras.Models;
using System.Collections.ObjectModel;

public partial class ListaProduto : ContentPage
{
    // Cria uma lista de produtos que atualiza a interface automaticamente
    ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();

    // Inicializa a página ListaProduto
    public ListaProduto()
    {
        // Carrega os elementos definidos no arquivo XAML
        InitializeComponent();

        // Define a lista de produtos como fonte de dados da ListView
        lst_produtos.ItemsSource = Lista;
    }

    // Executa quando a página aparece
    protected async override void OnAppearing()
    {
        // Limpa a lista para evitar produtos duplicados
        Lista.Clear();

        // Busca todos os produtos cadastrados no banco de dados
        List<Produto> tmp = await App.Db.GetAll();

        // Adiciona os produtos encontrados na lista
        tmp.ForEach(i => Lista.Add(i));
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

    // Executa quando o texto do campo de busca é alterado
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Obtém o texto digitado pelo usuário
        string q = e.NewTextValue;

        // Limpa a lista para evitar produtos duplicados durante a pesquisa
        Lista.Clear();

        // Busca no banco os produtos que correspondem ao texto digitado
        List<Produto> tmp = await App.Db.Search(q);

        // Adiciona os produtos encontrados na lista
        tmp.ForEach(i => Lista.Add(i));
    }

    // Executa quando o botão Somar é pressionado
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Calcula o valor total dos produtos
        double soma = Lista.Sum(i => i.Preco * i.Quantidade);

        // Cria a mensagem com o valor total
        String msg = $"O total é {soma:C}";

        // Exibe o valor total dos produtos
        DisplayAlert("Total dos produtos", msg, "Ok");
    }

    // Executa quando a opção Remover é pressionada
    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}