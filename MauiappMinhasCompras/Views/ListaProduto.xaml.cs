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

    // Executa quando a lista é atualizada através do gesto de arrastar para baixo
    private async void lst_produtos_Refreshing(object sender, EventArgs e)
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
        finally
        {
            // Finaliza o indicador de atualização da ListView
            lst_produtos.IsRefreshing = false;
        }
    }

    // Executa quando o botão de filtro por categoria é pressionado
    // Retorna os produtos pertencentes à categoria selecionada
    private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        try
        {
            // Verifica se uma categoria foi selecionada
            if (Categoriaaa.SelectedItem == null)
            {
                await DisplayAlert("Ops", "Selecione uma categoria.", "OK");
                return;
            }

            // Pega a categoria selecionada no Picker
            string categoria = Categoriaaa.SelectedItem.ToString();

            // Limpa a lista para mostrar apenas os resultados da categoria
            Lista.Clear();

            // Se a categoria escolhida for Todos, busca todos os produtos
            if (categoria == "Todos")
            {
                List<Produto> tmp = await App.Db.GetAll();

                // Adiciona todos os produtos encontrados na lista
                tmp.ForEach(i => Lista.Add(i));
            }
            else
            {
                // Busca somente os produtos da categoria escolhida
                List<Produto> tmp = await App.Db.SearchCategoria(categoria);

                // Adiciona os produtos encontrados na lista
                tmp.ForEach(i => Lista.Add(i));
            }
        }


        catch (Exception ex)
        {
            // Exibe uma mensagem caso aconteça algum erro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }


    // Executa quando o botão de somar por categoria é pressionado
    private async void ToolbarItem_Clicked_3(object sender, EventArgs e)
    {
        try
        {
            // Verifica se uma categoria foi selecionada
            if (Categoriaaa.SelectedItem == null)
            {
                await DisplayAlert("Ops", "Selecione uma categoria.", "OK");
                return;
            }

            // Pega a categoria selecionada no Picker
            string categoria = Categoriaaa.SelectedItem.ToString();

            // Cria uma lista para receber os produtos
            List<Produto> tmp;

            // Se escolher Todos, busca todos os produtos
            if (categoria == "Todos")
            {
                // Busca todos os produtos cadastrados
                tmp = await App.Db.GetAll();
            }
            else
            {
                // Busca somente os produtos da categoria selecionada
                tmp = await App.Db.SearchCategoria(categoria);
            }

            // Soma o preço de cada produto multiplicado pela quantidade
            double soma = tmp.Sum(i => i.Preco * i.Quantidade);

            // Cria a mensagem com o valor total
            String msg = $"O total de {categoria} é {soma:C}";

            // Mostra o resultado
            await DisplayAlert("Total da categoria", msg, "Ok");
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem caso aconteça algum erro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    // Obtém o DatePicker responsável pela data final do filtro
    private DatePicker GetData_final1()
    {
        // Retorna o campo de seleção da data final
        return data_final;
    }

    // Executa quando o botão de filtro por período é pressionado
    private async void ToolbarItem_Clicked_4(object sender, EventArgs e)
    {
        try
        {
            // Obtém a data inicial selecionada pelo usuário
            DateTime inicial = data_inicial.Date.Value;

            // Obtém a data final selecionada pelo usuário
            DateTime final = data_final.Date.Value;

            // Verifica se a data inicial é maior que a data final
            if (inicial > final)
            {
                // Exibe uma mensagem informando que o período é inválido
                await DisplayAlert(
                    "Ops",
                    "A data inicial não pode ser maior que a data final.",
                    "OK");

                return;
            }

            // Limpa a lista atual
            Lista.Clear();

            // Busca os produtos dentro do período informado
            List<Produto> tmp = await App.Db.SearchPeriodo(inicial, final);

            // Adiciona os produtos encontrados na lista
            tmp.ForEach(i => Lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem caso aconteça algum erro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}