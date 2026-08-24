using MauiappMinhasCompras.Models;

namespace MauiappMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    // Inicializa a página de cadastro de produtos
    public NovoProduto()
    {
        // Carrega os elementos definidos no arquivo XAML
        InitializeComponent();
    }

    // Executa quando o botão Salvar é pressionado
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria um novo objeto Produto com os dados informados pelo usuário
            Produto p = new Produto
            {
                // Obtém a descrição digitada no campo de descrição
                Descricao = txt_descricao.Text,

                // Converte a quantidade informada para um número inteiro
                Quantidade = Convert.ToInt32(txt_quantidade.Text),

                // Converte o preço informado para um número decimal
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere o produto no banco de dados SQLite
            await App.Db.Insert(p);

            // Informa ao usuário que o produto foi salvo com sucesso
            await DisplayAlert("Sucesso", "Produto salvo!", "OK");
        }
        catch (Exception ex)
        {
            // Exibe a mensagem caso aconteça algum erro durante o cadastro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}