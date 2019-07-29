using BookStore.DomainModels;
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

namespace BookStore
{
    /// <summary>
    /// Interaction logic for WinClientes.xaml
    /// </summary>
    public partial class WinClientes : Window
    {
        private Cliente _clienteEdicao;
        public WinClientes()
        {
            InitializeComponent();
        }

        public WinClientes(Cliente c)
        {
            InitializeComponent();
            _clienteEdicao = c;
            fillForm(_clienteEdicao);
        }

        private void BtnAddCliente_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text != "" &&txtEnd.Text != "" && txtTel.Text != "")
            {
                Cliente_Helper_CRUD chc = new Cliente_Helper_CRUD(App.ligacaoBD);
                Cliente c;
                if (_clienteEdicao == null)
                {
                    c = new Cliente();
                }
                else
                {
                    c = _clienteEdicao;
                }
                c.Nome = txtNome.Text;
                c.Endereco = txtEnd.Text;
                c.Telefone = txtTel.Text;
                if (chkEstado.IsChecked == true)
                {
                    c.Estado = 1.ToString();
                }
                else
                {
                    c.Estado = 0.ToString();
                }
                string status = chc.atualizar(c);
                if (status != "")
                {
                    MessageBox.Show("Erro: " + status);
                }
                else
                {
                    limpaFrom();
                }
            }

        }

        private void BtnLstClientes_Click(object sender, RoutedEventArgs e)
        {
            lstClientes listarClientes = new lstClientes(chkAtivos);
            this.Close();
            listarClientes.ShowDialog();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow JanelaPrincipal = new MainWindow();
            this.Close();
            JanelaPrincipal.ShowDialog();
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Você tem certeza que deseja sair?", "Mensagem do Sistema", MessageBoxButton.YesNo) == MessageBoxResult.Yes)

            {

                App.Current.Shutdown();

            }
        }

        private void limpaFrom()
        {
            txtNome.Text = "";
            txtEnd.Text = "";
            txtTel.Text = "";
            chkEstado.IsChecked = false;
            addContent.Text = "Adicionar\nClientes";
            _clienteEdicao = null;
        }

        private void fillForm(Cliente c)
        {
            txtNome.Text = c.Nome;
            txtEnd.Text = c.Endereco;
            txtTel.Text = c.Telefone;
            if (c.Estado == "Ativo")
            {
                chkEstado.IsChecked = true;
            }
            addContent.Text = "Atualizar\nCliente";
        }
    }
}
