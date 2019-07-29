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
    /// Interaction logic for WinEmp.xaml
    /// </summary>
    public partial class WinEmp : Window
    {
        public WinEmp()
        {
            InitializeComponent();
            limpaForm();
        }        

        private void BtnAddEmp_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = cmbCliente.SelectedItem as Cliente;
            Livro l = cmbLivro.SelectedItem as Livro;
            Funcionario f = cmbFuncionario.SelectedItem as Funcionario;
            if (dataEmprestimo.SelectedDate != DateTime.Today.AddDays(1) &&
                c.GuidPessoa != "" &&
                l.GuidLivro != "" &&
                f.GuidPessoa != "")
            {
                Emprestimo_Helper_CRUD ehc = new Emprestimo_Helper_CRUD(App.ligacaoBD);
                Emprestimo emp;
                
                emp = new Emprestimo();
                
                emp.ClienteEmp = c;
                emp.FuncionarioEmp = f;
                emp.LivroEmp = l;
                try
                {
                    emp.DataEmp = Convert.ToDateTime(dataEmprestimo.SelectedDate);
                }
                catch
                {
                    emp.DataEmp = Convert.ToDateTime("1900/01/01");
                }
                string statusOp = ehc.atualizar(emp);
                if (statusOp != "")
                {
                    MessageBox.Show("Erro: " + statusOp);
                }
                else
                {
                    limpaForm();
                }
            }
            else
            {
                MessageBox.Show("Todos os campos devem ser preenchidos!");
            }
        }

        private void BtnLstEmp_Click(object sender, RoutedEventArgs e)
        {
            lstEmp listarEmprestimos;
            Cliente c = cmbClienteLst.SelectedItem as Cliente;
            Livro l = cmbLivroLst.SelectedItem as Livro;
            Funcionario f = cmbFuncionarioLst.SelectedItem as Funcionario;
            DateTime dt = Convert.ToDateTime(dataEmprestimoLst.SelectedDate);
            listarEmprestimos = new lstEmp(c, l, f, dt, chkHist);
            this.Close();
            listarEmprestimos.ShowDialog();
            
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

        private void limpaForm()
        {
            preencherComboEmp();
            preencherComboLst();
            cmbCliente.SelectedIndex =-1;
            cmbLivro.SelectedIndex =-1;
            cmbFuncionario.SelectedIndex =-1;
            cmbClienteLst.SelectedIndex = -1;
            cmbLivroLst.SelectedIndex = -1;
            cmbFuncionarioLst.SelectedIndex = -1;
            dataEmprestimo.SelectedDate = DateTime.Today.AddDays(1);
            dataEmprestimoLst.SelectedDate = DateTime.Today.AddDays(1);
        }

        #region: "Combos de Inserção"
        private void preencherComboEmp()
        {
            //Combobox Livros
            Livro_Helper_CRUD lhc = new Livro_Helper_CRUD(App.ligacaoBD);
            List<Livro> lstL;
            if (cmbLivro.ItemsSource != null)
                cmbLivro.ItemsSource = null;
            if (cmbLivro.Items.Count > 0)
                cmbLivro.Items.Clear();
            lstL = lhc.list(1);           
            
            if (lstL.Count > 0)
            {
                cmbLivro.ItemsSource = lstL;
            }
            else
            {
                Livro L = new Livro();
                L.Nome = "Sem Livros";
                cmbLivro.Items.Add(L);
            }
            cmbLivro.SelectedIndex = 0;

            //Combobox Clientes
            Cliente_Helper_CRUD chc = new Cliente_Helper_CRUD(App.ligacaoBD);
            if (cmbCliente.ItemsSource != null)
                cmbCliente.ItemsSource = null;
            if (cmbCliente.Items.Count > 0)
                cmbCliente.Items.Clear();
            List<Cliente> lstC = chc.list(1);
            if (lstC.Count > 0)
            {
                cmbCliente.ItemsSource = lstC;
            }
            else
            {
                Cliente C = new Cliente();
                C.Nome = "Sem Clientes";
                cmbCliente.Items.Add(C);
            }
            cmbCliente.SelectedIndex = 0;

            //Combobox Funcionarios
            Funcionario_Helper_CRUD fhc = new Funcionario_Helper_CRUD(App.ligacaoBD);
            if (cmbFuncionario.ItemsSource != null)
                cmbFuncionario.ItemsSource = null;
            if (cmbFuncionario.Items.Count > 0)
                cmbFuncionario.Items.Clear();
            List<Funcionario> lstF = fhc.list(1);
            if (lstF.Count > 0)
            {
                cmbFuncionario.ItemsSource = lstF;
            }
            else
            {
                Funcionario F = new Funcionario();
                F.Nome = "Sem Funcionários";
                cmbFuncionario.Items.Add(F);
            }
            cmbFuncionario.SelectedIndex = 0;
        }

        private int findIndexForCliente(Cliente c)
        {
            int indexCliente = -1;
            int MaxItensC = cmbCliente.Items.Count;

            int iCnt = 0;
            while (indexCliente == -1 && iCnt < MaxItensC)
            {
                Cliente onCombo = cmbCliente.Items[iCnt] as Cliente;
                if (c.GuidPessoa == onCombo.GuidPessoa)
                    indexCliente = iCnt;
                else
                    iCnt++;
            }
            return indexCliente;
        }

        private int findIndexForLivro(Livro l)
        {
            int indexLivro = -1;
            int MaxItensL = cmbLivro.Items.Count;

            int iCnt = 0;

            while (indexLivro == -1 && iCnt < MaxItensL)
            {
                Livro onCombo = cmbLivro.Items[iCnt] as Livro;
                if (l.GuidLivro == onCombo.GuidLivro)
                    indexLivro = iCnt;
                else
                    iCnt++;
            }
            return indexLivro;
        }

        private int findIndexForFuncionario(Funcionario f)
        {
            int indexFuncionario = -1;
            int MaxItensF = cmbFuncionario.Items.Count;

            int iCnt = 0;
            while (indexFuncionario == -1 && iCnt < MaxItensF)
            {
                Funcionario onCombo = cmbFuncionario.Items[iCnt] as Funcionario;
                if (f.GuidPessoa == onCombo.GuidPessoa)
                    indexFuncionario = iCnt;
                else
                    iCnt++;
            }

            return indexFuncionario;
        }
        #endregion

        #region: "Combos de Listagem"
        private void preencherComboLst()
        {
            //Combobox Livros
            Livro_Helper_CRUD lhc = new Livro_Helper_CRUD(App.ligacaoBD);
            List<Livro> lstL;
            if (cmbLivroLst.ItemsSource != null)
                cmbLivroLst.ItemsSource = null;
            if (cmbLivroLst.Items.Count > 0)
                cmbLivroLst.Items.Clear();
            lstL = lhc.list(0);

            if (lstL.Count > 0)
            {
                cmbLivroLst.ItemsSource = lstL;
            }
            else
            {
                Livro L = new Livro();
                L.Nome = "Sem Livros Emprestados";
                cmbLivroLst.Items.Add(L);
            }
            cmbLivroLst.SelectedIndex = 0;

            //Combobox Clientes
            Cliente_Helper_CRUD chc = new Cliente_Helper_CRUD(App.ligacaoBD);
            if (cmbClienteLst.ItemsSource != null)
                cmbClienteLst.ItemsSource = null;
            if (cmbClienteLst.Items.Count > 0)
                cmbClienteLst.Items.Clear();
            List<Cliente> lstC = chc.list(1);
            if (lstC.Count > 0)
            {
                cmbClienteLst.ItemsSource = lstC;
            }
            else
            {
                Cliente C = new Cliente();
                C.Nome = "Sem Clientes";
                cmbClienteLst.Items.Add(C);
            }
            cmbClienteLst.SelectedIndex = 0;

            //Combobox Funcionarios
            Funcionario_Helper_CRUD fhc = new Funcionario_Helper_CRUD(App.ligacaoBD);
            if (cmbFuncionarioLst.ItemsSource != null)
                cmbFuncionarioLst.ItemsSource = null;
            if (cmbFuncionarioLst.Items.Count > 0)
                cmbFuncionarioLst.Items.Clear();
            List<Funcionario> lstF = fhc.list(1);
            if (lstF.Count > 0)
            {
                cmbFuncionarioLst.ItemsSource = lstF;
            }
            else
            {
                Funcionario F = new Funcionario();
                F.Nome = "Sem Funcionários";
                cmbFuncionarioLst.Items.Add(F);
            }
            cmbFuncionarioLst.SelectedIndex = 0;
        }

        private int findIndexForLstCliente(Cliente c)
        {
            int indexCliente = -1;
            int MaxItensC = cmbClienteLst.Items.Count;

            int iCnt = 0;
            while (indexCliente == -1 && iCnt < MaxItensC)
            {
                Cliente onCombo = cmbClienteLst.Items[iCnt] as Cliente;
                if (c.GuidPessoa == onCombo.GuidPessoa)
                    indexCliente = iCnt;
                else
                    iCnt++;
            }
            return indexCliente;
        }

        private int findIndexForLstLivro(Livro l)
        {
            int indexLivro = -1;
            int MaxItensL = cmbLivroLst.Items.Count;

            int iCnt = 0;

            while (indexLivro == -1 && iCnt < MaxItensL)
            {
                Livro onCombo = cmbLivroLst.Items[iCnt] as Livro;
                if (l.GuidLivro == onCombo.GuidLivro)
                    indexLivro = iCnt;
                else
                    iCnt++;
            }
            return indexLivro;
        }

        private int findIndexForLstFuncionario(Funcionario f)
        {
            int indexFuncionario = -1;
            int MaxItensF = cmbFuncionarioLst.Items.Count;

            int iCnt = 0;
            while (indexFuncionario == -1 && iCnt < MaxItensF)
            {
                Funcionario onCombo = cmbFuncionarioLst.Items[iCnt] as Funcionario;
                if (f.GuidPessoa == onCombo.GuidPessoa)
                    indexFuncionario = iCnt;
                else
                    iCnt++;
            }

            return indexFuncionario;
        }
        #endregion
    }
}
