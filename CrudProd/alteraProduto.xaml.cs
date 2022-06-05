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
    public partial class alteraProduto : Window
    {
        MySqlConnection conexaoDb;
        MySqlCommand executrQuery;
        MySqlDataAdapter dataAdp;
        MySqlDataReader dataRd;
        string querySql;

        public alteraProduto()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dataHoraCadastro = DateTime.Now.ToString("yyyy/dd/MM HH:mm:ss"); //DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")

                var ativo = "0";

                if (checkAtivo.IsChecked == true)
                {
                    ativo = "1";
                }

                conexaoDb = new MySqlConnection("Server = localhost; Database = testdev; Uid = root; Pwd = root;");

                querySql = "UPDATE PRODUTO SET descricao=@descricao, codGrupo=@codGrupo, codBarra=@codBarra, precoCusto=@precoCusto, precoVenda=@precoVenda, dataHoraCadastro=@dataHoraCadastro, ativo =@ativo" +
                    "WHERE cod=@cod";

                executrQuery = new MySqlCommand(querySql, conexaoDb);
                executrQuery.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                executrQuery.Parameters.AddWithValue("@codBarra", txtCodigoBarra.Text);
                executrQuery.Parameters.AddWithValue("@codGrupo", "1");
                executrQuery.Parameters.AddWithValue("@precoCusto", txtPrecoCusto.Text);
                executrQuery.Parameters.AddWithValue("@precoVenda", txtPrecoVenda.Text);
                executrQuery.Parameters.AddWithValue("@dataHoraCadastro", dataHoraCadastro);
                executrQuery.Parameters.AddWithValue("@ativo", ativo);
                executrQuery.Parameters.AddWithValue("@cod", txtCodProd.Text);

                conexaoDb.Open();

                executrQuery.ExecuteNonQuery();
                MessageBox.Show("Produto Alterado!");

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
                executrQuery = null;
            }
        }
    }
}
