using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    //Classe Funcionário deriva da classe Pessoa
    public class Funcionario : Pessoa
    {
        #region "Atributos"
        private string _cargo;
        private string _dtRegisto;
        #endregion

        #region "Propriedades"
        public string Cargo {
            get {
                return _cargo;
            }
            set {
                _cargo = value;
            }
        }

        public string Data {
            get {
                return _dtRegisto;
            }
            set {
                _dtRegisto = value;
            }
        }
        #endregion

        #region "Construtores"
        public Funcionario() : base()
        {
            Cargo = "";
            Data = "";
        }

        public Funcionario(string guidP) : base(guidP)
        {
            Cargo = "";
            Data = "";
        }

        public Funcionario(string guidP, string nome, string end, string tel, string estado,string cargo, string data) : base(guidP, nome, end, tel, estado)
        {
            Cargo = cargo;
            Data = data;
        }
        #endregion

        #region "Outros Métodos"
        public override string getInfo()
        {
            return $"ID: {GuidPessoa}\nNome: {Nome}\nEndereço: {Endereco}\nTelefone: {Telefone}\nCargo: {Cargo}\nEstado: {Estado}\nData de Registo: {Data}";
        }
        #endregion

    }
}
