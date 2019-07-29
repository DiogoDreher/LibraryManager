using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    //Classe Cliente deriva da classe Pessoa
    public class Cliente : Pessoa
    {
        #region "Atributos"
        private string _dtRegisto;
        #endregion

        #region "Propriedades"
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
        public Cliente():base()
        {
            Data = "";
        }

        public Cliente(string guidP):base(guidP)
        {
            Data = "";
        }

        public Cliente(string guidP, string nome, string end, string tel, string estado, string data) : base(guidP, nome, end, tel, estado)
        {
            Data = data;
        }
        #endregion

        #region "Outros Métodos"
        public override string getInfo()
        {
            return $"ID: {GuidPessoa}\nNome: {Nome}\nEndereço: {Endereco}\nTelefone: {Telefone}\nEstado: {Estado}\nData de Registo: {Data}";
        }

        
        #endregion

    }
}
