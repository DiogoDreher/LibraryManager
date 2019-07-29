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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            WinClientes JanelaClientes = new WinClientes();
            this.Close();
            JanelaClientes.Show();
        }

        private void BtnFuncionarios_Click(object sender, RoutedEventArgs e)
        {
            WinFuncionario JanelaFuncionario = new WinFuncionario();
            this.Close();
            JanelaFuncionario.Show();
        }

        private void BtnLivros_Click(object sender, RoutedEventArgs e)
        {
            WinLivros JanelaLivros = new WinLivros();
            this.Close();
            JanelaLivros.Show();
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Você tem certeza que deseja sair?", "Mensagem do Sistema", MessageBoxButton.YesNo) == MessageBoxResult.Yes )

            {

                App.Current.Shutdown();

            }
        }

        private void BtnEmp_Click(object sender, RoutedEventArgs e)
        {
            WinEmp JanelaEmprestimo = new WinEmp();
            this.Close();
            JanelaEmprestimo.Show();
        }
    }
}
