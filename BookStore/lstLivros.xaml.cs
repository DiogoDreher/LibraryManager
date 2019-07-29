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
    /// Interaction logic for lstLivros.xaml
    /// </summary>
    public partial class lstLivros : Window
    {
        private ObservableCollection<Livro> _contentor;
        private Livro _editLivro;
        private CheckBox chk;
        public lstLivros(CheckBox chkDisponivel) 
        {
            InitializeComponent();
            chk = chkDisponivel;
            listar(chkDisponivel);
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            //WinLivros JanelaLivro = new WinLivros();
            this.Close();
            //JanelaLivro.ShowDialog();
        }

        private void ListaLivros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editLivro = listaLivros.SelectedItem as Livro;
            if (_editLivro != null)
            {
                MessageBox.Show(_editLivro.getInfo());
            }
            btnEditar.IsEnabled = true;
            btnApagar.IsEnabled = true;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
            WinLivros JanelaLivros = new WinLivros(_editLivro);
            JanelaLivros.Show();
            this.Close();

        }

        private void BtnApagar_Click(object sender, RoutedEventArgs e)
        {
            if (_editLivro != null)
            {
                Livro_Helper_CRUD lhc = new Livro_Helper_CRUD(App.ligacaoBD);
                string status = lhc.apagar(_editLivro);
                MessageBox.Show("Livro apagado com sucesso!");

            }
            listar(chk);
        }

        private void listar(CheckBox chkDisponivel)
        {
            int estadoAVer = (chkDisponivel.IsChecked == true) ? 1 : 0;
            Livro_Helper_CRUD lhc = new Livro_Helper_CRUD(App.ligacaoBD);
            _contentor = new ObservableCollection<Livro>(lhc.list(estadoAVer));
            listaLivros.ItemsSource = _contentor;
            btnApagar.IsEnabled = false;
            btnEditar.IsEnabled = false;
        }

        
    }
}
