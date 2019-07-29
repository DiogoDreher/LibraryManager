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
    /// Interaction logic for lstClientes.xaml
    /// </summary>
    public partial class lstClientes : Window
    {
        private ObservableCollection<Cliente> _contentor;
        private Cliente _editCliente;
        private CheckBox chk;
        public lstClientes(CheckBox chkAtivos)
        {
            InitializeComponent();
            chk = chkAtivos;
            listar(chkAtivos);
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinClientes JanelaClientes = new WinClientes();
            this.Close();
            JanelaClientes.ShowDialog();
        }

        private void ListaClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editCliente = listaClientes.SelectedItem as Cliente;
            if (_editCliente != null)
            {
                MessageBox.Show(_editCliente.getInfo());
            }            

            btnApagar.IsEnabled = true;
            btnEditar.IsEnabled = true;

        }

        private void BtnApagar_Click(object sender, RoutedEventArgs e)
        {
            if (_editCliente != null)
            {
                Cliente_Helper_CRUD chc = new Cliente_Helper_CRUD(App.ligacaoBD);
                string status = chc.apagar(_editCliente);
                MessageBox.Show("Cliente apagado com sucesso!");
                
            }
            listar(chk);
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WinClientes JanelaClientes = new WinClientes(_editCliente);            
            JanelaClientes.Show();
            
        }

        private void listar(CheckBox chkAtivos)
        {
            int estadoAVer = (chkAtivos.IsChecked == true) ? 1 : 0;
            Cliente_Helper_CRUD chc = new Cliente_Helper_CRUD(App.ligacaoBD);
            _contentor = new ObservableCollection<Cliente>(chc.list(estadoAVer));
            listaClientes.ItemsSource = _contentor;
            btnApagar.IsEnabled = false;
            btnEditar.IsEnabled = false;
        }
    }
}
