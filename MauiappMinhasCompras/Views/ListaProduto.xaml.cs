namespace MauiappMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // Inicializa a página ListaProduto
    public ListaProduto()
    {
        // Carrega os elementos definidos no arquivo XAML
        InitializeComponent();
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
}