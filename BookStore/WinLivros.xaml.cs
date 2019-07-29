using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BookStore.DomainModels;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for WinLivros.xaml
    /// </summary>
    public partial class WinLivros : Window 
    {

        private Livro _livroEdicao;
        public WinLivros()
        {
            InitializeComponent();
        }
        public WinLivros(Livro l)
        {
            InitializeComponent();
            _livroEdicao = l;
            fillForm(_livroEdicao);
        }


        private void BtnAddLivro_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text != "" && 
                txtAutor.Text != "" && 
                txtAno.Text != "" &&
                txtEditora.Text != "" &&
                txtGen.Text != "")
            {
                Livro_Helper_CRUD lhc = new Livro_Helper_CRUD(App.ligacaoBD);
                Livro l;
                if (_livroEdicao == null)
                {
                    l = new Livro();
                }
                else
                {
                    l = _livroEdicao;
                }
                l.Nome = txtNome.Text;
                l.Autor = txtAutor.Text;
                l.Editora = txtEditora.Text;
                l.Genero = txtGen.Text;
                try
                {
                    l.Ano = Convert.ToInt32(txtAno.Text);
                }
                catch 
                {
                    l.Ano = 1500;
                }
                string status = lhc.atualizar(l);
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

        private void BtnLstLivros_Click(object sender, RoutedEventArgs e)
        {
            lstLivros listarLivros = new lstLivros(chkDisponivel);
            //this.Close();
            listarLivros.Show();
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
            txtAutor.Text = "";
            txtAno.Text = "";
            txtEditora.Text = "";
            txtGen.Text = "";
            addContent.Text = "Adicionar\nLivro";
            _livroEdicao = null;
            
        }

        private void fillForm(Livro l)
        {
            txtNome.Text = l.Nome;
            txtAutor.Text = l.Autor;
            txtAno.Text = l.Ano.ToString();
            txtEditora.Text = l.Editora;
            txtGen.Text = l.Genero;
            addContent.Text = "Atualizar\nLivro";
           
        }
    }
}
