using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    public class Livro
    {
        #region "Contantes"
        public const int MAXNOME = 40;
        #endregion

        #region "Atributos"
        private string _guidLivro;
        private string _nome;
        private string _autor;
        private int _ano;
        private string _genero;
        private string _editora;
        private string _status;
        private string _dataRegisto;
        private string _dataEmprestimo;
        #endregion

        #region "Propriedades"
        public string GuidLivro {
            get {
                return _guidLivro;
            }
        }

        public string Nome {
            get {
                return _nome;
            }
            set {
                _nome = value;
                if (_nome.Length > MAXNOME)
                {
                    _nome = _nome.Substring(0, MAXNOME);
                }
            }
        }

        public string Autor {
            get {
                return _autor;
            }
            set {
                _autor = value;                
            }
        }

        public int Ano {
            get {
                return _ano;
            }
            set {
                _ano = value;
                if (_ano > 2019)
                {
                    _ano = Convert.ToInt32(2019);
                }
            }
        }

        public string Genero {
            get {
                return _genero;
            }
            set {
                _genero = value;
            }
        }

        public string Editora {
            get {
                return _editora;
            }
            set {
                _editora = value;
            }
        }

        public string Estado {
            get {
                return _status;
            }
            set {
                _status = value;
                if (_status == 1.ToString())
                {
                    _status = "Disponivel";
                }
                else
                {
                    _status = "Indisponivel";
                }
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

        public string Emprestimo {
            get {
                return _dataEmprestimo;
            }
            set {
                _dataEmprestimo = value;
                
            }
        }
        #endregion

        #region "Construtores"
        public Livro()
        {
            _guidLivro = "";
            Nome = "";
            Autor = "";
            Ano = 1500;
            Genero = "";
            Editora = "";
            Estado = "";
            Data = "";
            Emprestimo = "";
        }

        public Livro(string guidLivro)
        {
            _guidLivro = guidLivro;
            Nome = "";
            Autor = "";
            Genero = "";
            Editora = "";
            Estado = "";
            Data = "";
            Emprestimo = "";
        }

        public Livro(string guidLivro, string nome, string autor,int ano, string genero, string editora, string estado, string data, string emp)
        {
            _guidLivro = guidLivro;
            Nome = nome;
            Autor = autor;
            Ano = ano;
            Genero = genero;
            Editora = editora;
            Estado = estado;
            Data = data;
            Emprestimo = emp;
        }
        #endregion

        #region "Outros Métodos"
        public string getInfo()
        {
            return $"ID: {GuidLivro}\nNome: {Nome}\nAutor: {Autor}\nAno: {Ano}\nGenero: {Genero}\nEditora: {Editora}\nEstado: {Estado}\nData Empréstimo: " + 
                    ((Estado == "Disponivel") ? " " : Emprestimo.Substring(0,10))  + 
                    $"\nData de Registo: {Data}";

        }
        public override string ToString()
        {
            return Nome;
        }
        #endregion
    }
}
