﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace CrudProd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection conexaoDb;
        MySqlCommand executrQuery;
        MySqlDataAdapter dataAdp;
        MySqlDataReader dataRd;
        string querySql;


        public MainWindow()
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

                querySql = "INSERT INTO PRODUTO (descricao, codGrupo, codBarra, precoCusto, precoVenda,dataHoraCadastro, ativo)" +
                    "VALUES(@descricao,@codGrupo, @codBarra,@precoCusto,@precoVenda,@dataHoraCadastro,@ativo)";

                executrQuery = new MySqlCommand(querySql, conexaoDb);
                executrQuery.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                executrQuery.Parameters.AddWithValue("@codBarra", txtCodBarra.Text);
                executrQuery.Parameters.AddWithValue("@codGrupo", "1");
                executrQuery.Parameters.AddWithValue("@precoCusto", txtPrecoCusto.Text);
                executrQuery.Parameters.AddWithValue("@precoVenda", txtPrecoVenda.Text);
                executrQuery.Parameters.AddWithValue("@dataHoraCadastro", dataHoraCadastro);
                executrQuery.Parameters.AddWithValue("@ativo", ativo);

                conexaoDb.Open();

                executrQuery.ExecuteNonQuery();
                MessageBox.Show("Produto Cadastrado!");

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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var cadastroProd = new MainWindow();

            cadastroProd.Show();
        }
    }
}
