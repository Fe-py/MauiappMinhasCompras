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
        try
        {
            // Limpa a lista para evitar produtos duplicados
            Lista.Clear();

            // Busca todos os produtos cadastrados no banco de dados
            List<Produto> tmp = await App.Db.GetAll();

            // Adiciona os produtos encontrados na lista
            tmp.ForEach(i => Lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem caso aconteça algum erro
            await DisplayAlert("ops", ex.Message, "ok");
        }
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
        try
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
        catch (Exception ex)
        {
            // Exibe uma mensagem caso aconteça algum erro
            await DisplayAlert("ops", ex.Message, "OK");
        }
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

    // Executa quando a opção Remover de um produto é pressionada
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o MenuItem que foi selecionado pelo usuário
            MenuItem selecionado = sender as MenuItem;

            // Obtém o produto associado ao MenuItem através do BindingContext
            Produto p = selecionado.BindingContext as Produto;

            // Exibe uma mensagem solicitando confirmação antes de remover o produto
            bool confirm = await DisplayAlert(
                "Tem Ceteza ?", $"remover produto {p.Descricao}", "sim", "não");

            // Verifica se o usuário confirmou a exclusão
            if (confirm)
            {
                // Remove o produto do banco de dados utilizando seu Id
                await App.Db.Delete(p.Id);

                // Remove o produto da lista exibida na interface
                Lista.Remove(p);
            }
        }
        catch
        {
            // Exibe uma mensagem caso aconteça algum erro
            await DisplayAlert("ops", "oi", "ok");
        }
    }

    // Executa quando um produto da lista é selecionado
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            // Obtém o produto que foi selecionado na ListView
            Produto p = e.SelectedItem as Produto;

            // Abre a página de edição do produto selecionado
            Navigation.PushAsync(new Views.EditarProduto
            {
                // Envia o produto selecionado para a página de edição
                BindingContext = p,
            });
        }
        catch
        {
            // Ignora o erro caso aconteça algum problema durante a seleção
        }
    }
}