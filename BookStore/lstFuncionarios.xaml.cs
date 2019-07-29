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
    /// Interaction logic for lstFuncionarios.xaml
    /// </summary>
    public partial class lstFuncionarios : Window
    {
        private ObservableCollection<Funcionario> _contentor;
        private Funcionario _editFuncionario;
        private CheckBox chk;
        public lstFuncionarios(CheckBox chkAtivos)
        {
            InitializeComponent();
            chk = chkAtivos;
            listar(chkAtivos);
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinFuncionario JanelaFuncionario = new WinFuncionario();
            this.Close();
            JanelaFuncionario.ShowDialog();
        }
        private void ListaFuncionarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editFuncionario = listaFuncionarios.SelectedItem as Funcionario;
            if (_editFuncionario != null)
            {
                MessageBox.Show(_editFuncionario.getInfo());
            }

            btnApagar.IsEnabled = true;
            btnEditar.IsEnabled = true;
        }

        private void BtnApagar_Click(object sender, RoutedEventArgs e)
        {
            if (_editFuncionario != null)
            {
                Funcionario_Helper_CRUD fhc = new Funcionario_Helper_CRUD(App.ligacaoBD);
                string status = fhc.apagar(_editFuncionario);
                MessageBox.Show("Funcionário apagado com sucesso!");
            }
            listar(chk);
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WinFuncionario JanelaFuncionario = new WinFuncionario(_editFuncionario);
            JanelaFuncionario.Show();
        }

        private void listar(CheckBox chkAtivos)
        {
            int estadoAVer = (chkAtivos.IsChecked == true) ? 1 : 0;
            Funcionario_Helper_CRUD fhc = new Funcionario_Helper_CRUD(App.ligacaoBD);
            _contentor = new ObservableCollection<Funcionario>(fhc.list(estadoAVer));
            listaFuncionarios.ItemsSource = _contentor;
            btnApagar.IsEnabled = false;
            btnEditar.IsEnabled = false;
        }
    }
}
