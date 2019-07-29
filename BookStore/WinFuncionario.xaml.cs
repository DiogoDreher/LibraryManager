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
    /// Interaction logic for WinFuncionario.xaml
    /// </summary>
    public partial class WinFuncionario : Window
    {
        private Funcionario _funcionarioEdicao;
        public WinFuncionario()
        {
            InitializeComponent();
        }
        public WinFuncionario(Funcionario f)
        {
            InitializeComponent();
            _funcionarioEdicao = f;
            fillForm(_funcionarioEdicao);
        }


        private void BtnAddFun_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text != "" && txtEnd.Text != "" && txtTel.Text != "" && txtCargo.Text != "")
            {
                Funcionario_Helper_CRUD fhc = new Funcionario_Helper_CRUD(App.ligacaoBD);
                Funcionario f;
                if (_funcionarioEdicao == null)
                {
                    f = new Funcionario();
                }
                else
                {
                    f = _funcionarioEdicao;
                }
                f.Nome = txtNome.Text;
                f.Endereco = txtEnd.Text;
                f.Telefone = txtTel.Text;
                f.Cargo = txtCargo.Text;
                if (chkEstado.IsChecked == true)
                {
                    f.Estado = 1.ToString();
                }
                else
                {
                    f.Estado = 0.ToString();
                }
                string status = fhc.atualizar(f);
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

        private void BtnLstFun_Click(object sender, RoutedEventArgs e)
        {
            lstFuncionarios listarFuncionarios = new lstFuncionarios(chkAtivos);
            this.Close();
            listarFuncionarios.ShowDialog();
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
            txtCargo.Text = "";
            chkEstado.IsChecked = false;
            addContent.Text = "Adicionar\nFuncionário";
            _funcionarioEdicao = null;
        }

        private void fillForm(Funcionario f)
        {
            txtNome.Text = f.Nome;
            txtEnd.Text = f.Endereco;
            txtTel.Text = f.Telefone;
            txtCargo.Text = f.Cargo;
            if (f.Estado == "Ativo")
            {
                chkEstado.IsChecked = true;
            }
            addContent.Text = "Atualizar\nFuncionário";
        }
    }
}
