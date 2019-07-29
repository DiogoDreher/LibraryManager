using BookStore.DomainModels;
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

namespace BookStore
{
    /// <summary>
    /// Interaction logic for lstEmp.xaml
    /// </summary>
    public partial class lstEmp : Window
    {

        private ObservableCollection<Emprestimo> _contentor;
        private Emprestimo _editEmp;
        private CheckBox chk;

        public lstEmp(Cliente c, Livro l, Funcionario f, DateTime dt, CheckBox chkHist)
        {
            chk = chkHist;
            InitializeComponent();
            listar(c, l, f, dt, chkHist);
            if (chk.IsChecked == true)
            {
                btnApagar.Visibility = Visibility.Hidden;
            }
        }
        

        private void BtnApagar_Click(object sender, RoutedEventArgs e)
        {
            if (_editEmp != null)
            {
                Emprestimo_Helper_CRUD ehc = new Emprestimo_Helper_CRUD(App.ligacaoBD);
                string status = ehc.apagar(_editEmp);
                MessageBox.Show("Livro devolvido!");

            }
            listar(null,null,null,DateTime.Today.AddDays(1), chk);
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            WinEmp JanelaEmp = new WinEmp();
            this.Close();
            JanelaEmp.ShowDialog();
        }

        private void ListaEmprestimos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editEmp = listaEmprestimos.SelectedItem as Emprestimo;
            if (_editEmp != null)
            {
                MessageBox.Show(_editEmp.getInfo());
            }
                btnApagar.IsEnabled = true;
            
            
        }
        

        private void listar(Cliente c, Livro l, Funcionario f, DateTime dt, CheckBox chkHist)
        {
            int estadoAVer = (chkHist.IsChecked == true) ? 1: 0;
            Emprestimo_Helper_CRUD ehc = new Emprestimo_Helper_CRUD(App.ligacaoBD);
            _contentor = new ObservableCollection<Emprestimo>(ehc.list(c, l, f, dt, estadoAVer));
            listaEmprestimos.ItemsSource = _contentor;
            btnApagar.IsEnabled = false;
        }
    }
}
