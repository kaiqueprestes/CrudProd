using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace CrudProd
{
    /// <summary>
    /// Lógica interna para deletarProduto.xaml
    /// </summary>
    public partial class deletarProduto : Window
    {
        MySqlConnection conexaoDb;
        MySqlCommand executeQuery;
        MySqlDataAdapter dataAdp;
        MySqlDataReader dataRd;
        string querySql;

        public deletarProduto()
        {
            InitializeComponent();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conexaoDb = new MySqlConnection("Server = localhost; Database = testdev; Uid = root; Pwd = root;");

                querySql = "DELETE FROM PRODUTO WHERE COD=@COD AND ATIVO = 1";

                executeQuery = new MySqlCommand(querySql, conexaoDb);
                executeQuery.Parameters.AddWithValue("@COD", txtDeletar.Text);

                conexaoDb.Open();

                executeQuery.ExecuteNonQuery();
                MessageBox.Show("Produto Excluído!");

                this.Close();
                //pesquisar regex 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexaoDb.Close();
                conexaoDb = null;
                executeQuery = null;
            }
        }
    }
}

