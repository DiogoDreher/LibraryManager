using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    public class Emprestimo
    {
        #region: "Atributos"
        private string _uidEmprestimo;
        private Cliente _Cliente;
        private Livro _Livro;
        private Funcionario _Funcionario;
        private DateTime _dataEmprestimo;
        private string _dataRegisto;
        private DateTime _dataDevolucao;
        private string _estado;
        #endregion

        #region: "Propriedades/Metodos"
        public string uidEmprestimo {
            get {
                return _uidEmprestimo;
            }
        }

        public Cliente ClienteEmp {
            get {
                return _Cliente;
            }
            set {
                _Cliente = value;
            }
        }

        public Livro LivroEmp {
            get {
                return _Livro;
            }
            set {
                _Livro = value;
            }
        }

        public Funcionario FuncionarioEmp {
            get {
                return _Funcionario;
            }
            set {
                _Funcionario = value;
            }
        }

        public DateTime DataEmp {
            get {
                return _dataEmprestimo;
            }
            set {
                _dataEmprestimo = value;
            }
        }

        public DateTime DataDevolucao {
            get {
                return _dataDevolucao;
            }
            set {
                _dataDevolucao = value;
            }
        }

        public string Data {
            get {
                return _dataRegisto;
            }
            set {
                _dataRegisto = value;
            }
        }

        public string Estado {
            get {
                return _estado;
            }
            set {
                _estado = value;
                //if (DateTime.Now > DataEmp.AddDays(30))
                //{
                //    _estado = "Atrasado.";
                //}
                //else
                //{
                //    _estado = "Em dia.";
                //}
            }
        }
        #endregion

        #region: "Construtores"
        public Emprestimo()
        {
            _uidEmprestimo = "";
            ClienteEmp = null;
            LivroEmp = null;
            FuncionarioEmp = null;
            DataEmp = Convert.ToDateTime("1900/01/01");
            DataDevolucao = Convert.ToDateTime("1900/01/01");
            Estado = "";
        }

        public Emprestimo(string guidEmp)
        {
            _uidEmprestimo = guidEmp;
            ClienteEmp = null;
            LivroEmp = null;
            FuncionarioEmp = null;
            DataEmp = Convert.ToDateTime("1900/01/01");
            DataDevolucao = Convert.ToDateTime("1900/01/01");
            Estado = "";
        }

        #endregion

        #region: "Outros Metodos"
        public string getInfo()
        {
            return $"Dados do empréstimo\n\nID: {uidEmprestimo}\n" +
                   $"Data do Registo: {Data}\n\n" +
                   $"Livro: {LivroEmp.Nome}\n" +
                   $"Cliente: {ClienteEmp.Nome}\n" +
                   $"Funcionário: {FuncionarioEmp.Nome}\n" +
                   $"Data do Empréstimo: {DataEmp.ToShortDateString()}\n" +
                   ((Estado == "Devolvido.") ? $"Data da Devolução: {DataDevolucao.ToShortDateString()}\n" : "") +       
                   $"Estado: {Estado}";
        }
        #endregion
    }
}
