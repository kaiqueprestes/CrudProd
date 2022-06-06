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
    /// Lógica interna para consultaProduto.xaml
    /// </summary>
    public partial class consultaProduto : Window
    {
        MySqlConnection conexaoDb;
        MySqlCommand executeQuery;
        MySqlDataAdapter dataAdp;
        MySqlDataReader dataRd;
        string querySql;

        public consultaProduto()
        {
            InitializeComponent();
        }

        private void buttonAlteraProduto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodProd.Text))
                {
                    MessageBox.Show("Digite o código do produto para alterar");
                }
                else
                {
                    var dataHoraCadastro = DateTime.Now.ToString("yyyy/dd/MM HH:mm:ss"); //DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")

                    var ativo = "0";

                    if (checkAtivo.IsChecked == true)
                    {
                        ativo = "1";
                    }

                    conexaoDb = new MySqlConnection("Server = localhost; Database = testdev; Uid = root; Pwd = root;");

                    querySql = "UPDATE PRODUTO SET descricao= @descricao, codGrupo=@codGrupo, codBarra=@codBarra, precoCusto=@precoCusto, precoVenda=@precoVenda, dataHoraCadastro=@dataHoraCadastro, ativo = @ativo WHERE cod = @cod";

                    executeQuery = new MySqlCommand(querySql, conexaoDb);
                    executeQuery.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                    executeQuery.Parameters.AddWithValue("@codBarra", txtCodigoBarra.Text);
                    executeQuery.Parameters.AddWithValue("@codGrupo", "1");
                    executeQuery.Parameters.AddWithValue("@precoCusto", txtPrecoCusto.Text.Replace(",", "."));
                    executeQuery.Parameters.AddWithValue("@precoVenda", txtPrecoVenda.Text.Replace(",", "."));
                    executeQuery.Parameters.AddWithValue("@dataHoraCadastro", dataHoraCadastro);
                    executeQuery.Parameters.AddWithValue("@ativo", ativo);
                    executeQuery.Parameters.AddWithValue("@cod", txtCodProd.Text);

                    conexaoDb.Open();

                    executeQuery.ExecuteNonQuery();
                    MessageBox.Show("Produto Alterado!");

                    conexaoDb.Close();
                    conexaoDb = null;
                    executeQuery = null;

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDeletaProduto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conexaoDb = new MySqlConnection("Server = localhost; Database = testdev; Uid = root; Pwd = root;");

                if (string.IsNullOrEmpty(txtCodProd.Text))
                {
                    MessageBox.Show("Digite o código do produto");
                }
                else
                {
                    if (checkAtivo.IsChecked == false)
                    {
                        querySql = "DELETE FROM PRODUTO WHERE COD=@COD and ativo = 0";

                        executeQuery = new MySqlCommand(querySql, conexaoDb);
                        executeQuery.Parameters.AddWithValue("@COD", txtCodProd.Text);

                        conexaoDb.Open();

                        executeQuery.ExecuteNonQuery();

                        MessageBox.Show("Produto Excluído!");

                        this.Close();

                        conexaoDb.Close();
                        conexaoDb = null;
                        executeQuery = null;
                    }
                    else
                    {
                        MessageBox.Show("Não é possivél excluir um produto ativo!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonConsultaProduto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodProd.Text))
                {
                    MessageBox.Show("Digite o código do produto");
                }

                conexaoDb = new MySqlConnection("Server = localhost; Database = testdev; Uid = root; Pwd = root;");

                querySql = "SELECT * FROM PRODUTO INNER JOIN PRODUTO_GRUPO ON PRODUTO.CODGRUPO=PRODUTO_GRUPO.COD WHERE PRODUTO.COD=@cod";

                executeQuery = new MySqlCommand(querySql, conexaoDb);
                executeQuery.Parameters.AddWithValue("@cod", txtCodProd.Text);

                conexaoDb.Open();

                dataRd = executeQuery.ExecuteReader();

                while (dataRd.Read())
                {
                    txtDescricao.Text = Convert.ToString(dataRd["descricao"]);
                    comboTipoGrupo.Text = Convert.ToString(dataRd["codGrupo"]) + "-" + Convert.ToString(dataRd["nome"]);
                    txtCodigoBarra.Text = Convert.ToString(dataRd["codBarra"]);
                    txtPrecoCusto.Text = Convert.ToString(dataRd["precoCusto"]);
                    txtPrecoVenda.Text = Convert.ToString(dataRd["precoVenda"]);

                    if (Convert.ToInt32(dataRd["ativo"]) == 1)
                    {
                        checkAtivo.IsChecked = true;
                    }
                }
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
