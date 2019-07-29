using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    public class Pessoa
    {
        #region "Constantes"
        public const int MAXNOME = 50;
        public const int MAXEND = 100;
        #endregion

        #region "Atributos"
        private string _guidPessoa;
        private string _nome;
        private string _endereco;
        private string _telefone;
        private string _status;
        #endregion


        #region "Propriedades"
        public string GuidPessoa {
            get {
                return _guidPessoa;
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

        public string Endereco {
            get {
                return _endereco;
            }
            set {
                _endereco = value;
                if (_endereco.Length > MAXEND)
                {
                    _endereco = _endereco.Substring(0, MAXEND);
                }
            }
        }

        public string Telefone {
            get {
                return _telefone;
            }
            set {
                _telefone = value;
                if (_telefone.Length > 16)
                {
                    _telefone = "+000 000 000 000";
                }
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
                    _status = "Ativo";
                }
                else
                {
                    _status = "Inativo";
                }
            }
        }
        #endregion

        #region "Construtores"
        public Pessoa()
        {
            _guidPessoa = "";
            Nome = "";
            Endereco = "";
            Telefone = "";
        }

        public Pessoa(string guidP)
        {
            _guidPessoa = guidP;
            Nome = "";
            Endereco = "";
            Telefone = "";
            Estado = "";
        }

        public Pessoa(string guidP, string nome, string end, string tel, string estado)
        {
            _guidPessoa = guidP;
            Nome = nome;
            Endereco = end;
            Telefone = tel;
            Estado = estado;
        }

        #endregion

        #region "Outros Métodos"
        public virtual string getInfo()
        {
            return $"ID: {GuidPessoa}\nNome: {Nome}\nEndereço: {Endereco}\nTelefone: {Telefone}";
        }

        public override string ToString()
        {
            return Nome;
        }
        #endregion
    }
}
