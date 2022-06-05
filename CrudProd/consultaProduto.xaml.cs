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

namespace CrudProd
{
    /// <summary>
    /// Lógica interna para consultaProduto.xaml
    /// </summary>
    public partial class consultaProduto : Window
    {
        public consultaProduto()
        {
            InitializeComponent();
        }

        private void buttonAlteraProduto_Click(object sender, RoutedEventArgs e)
        {
            var alterarProduto = new alteraProduto();

            alterarProduto.Show();
        }

        private void buttonDeletaProduto_Click(object sender, RoutedEventArgs e)
        {
            var deletarProduto = new deletarProduto();

            deletarProduto.Show();
        }
    }
}
