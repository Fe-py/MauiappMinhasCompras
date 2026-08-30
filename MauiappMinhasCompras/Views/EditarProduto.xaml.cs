using MauiappMinhasCompras.Models;

namespace MauiappMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto Produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os dados informados pelo usuário
            Produto p = new Produto
            {
                Id = Produto_anexado.Id,
                // Obtém a descrição digitada no campo de descrição
                Descricao = txt_descricao.Text,

                // Converte a quantidade informada para um número inteiro
                Quantidade = Convert.ToInt32(txt_quantidade.Text),

                // Converte o preço informado para um número decimal
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere o produto no banco de dados SQLite
            await App.Db.Update(p);

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