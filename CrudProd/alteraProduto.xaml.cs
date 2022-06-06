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
           
        }
    }
}
