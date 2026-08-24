using MauiappMinhasCompras.Helpers;
using MauiappMinhasCompras.Views;

namespace MauiappMinhasCompras
{
    public partial class App : Application
    {
        // Armazena a conexão com o banco de dados SQLite
        static SQLiteDatabaseHelper _db;

        // Permite acessar o banco de dados através da aplicação
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // Verifica se a conexão com o banco ainda não foi criada
                if (_db == null)
                {
                    // Define o caminho onde o banco de dados será armazenado
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite.compras.db3");

                    // Cria a conexão com o banco de dados
                    _db = new SQLiteDatabaseHelper(path);
                }

                // Retorna a conexão com o banco de dados
                return _db;
            }
        }

        // Inicializa a aplicação
        public App()
        {
            // Carrega os elementos definidos no App.xaml
            InitializeComponent();

            // Define a ListaProduto como a primeira página da aplicação
            // NavigationPage permite navegar entre as páginas
            MainPage = new NavigationPage(new ListaProduto());
        }
    }
}