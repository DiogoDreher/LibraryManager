using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    class Funcionario_Helper_CRUD
    {
        private string _ligacao;

        public Funcionario_Helper_CRUD(string ligacaoAUtilizar)
        {
            _ligacao = ligacaoAUtilizar;
        }

        public List<Funcionario> list(int estadoAVer)
        {
            List<Funcionario> listaADevolver = new List<Funcionario>();
            DataTable listaBrutaClientes = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection numero = new SqlConnection(_ligacao);

            pedido.CommandType = CommandType.Text;
            pedido.CommandText = (estadoAVer == Convert.ToInt32(1)) ? "SELECT * FROM Funcionarios WHERE status = " + estadoAVer.ToString() : "SELECT * FROM Funcionarios";

            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = numero;
            numero.Open();
            telefone.Fill(listaBrutaClientes);
            numero.Close();

            foreach (DataRow linha in listaBrutaClientes.Rows)
            {
                Funcionario f = new Funcionario(linha["guidFuncionarios"].ToString());
                f.Nome = linha["nome"].ToString();
                f.Endereco = linha["endereco"].ToString();
                f.Telefone = linha["telefone"].ToString();
                f.Cargo = linha["cargo"].ToString();
                f.Data = linha["dtAdmissao"].ToString();
                f.Estado = linha["status"].ToString();
                listaADevolver.Add(f);
            }


            return listaADevolver;
        }

        public string atualizar(Funcionario f)
        {
            string erros = "";
            string sql = "";
            if (f.GuidPessoa == "")
            {
                //Criar Funcionario
                sql = "INSERT INTO Funcionarios (nome, endereco, telefone, cargo, status)";
                sql += "VALUES (@nome, @end, @tel, @cargo, @status)";
            }
            else
            {
                //Atualza Cliente
                sql = "UPDATE Funcionarios SET nome = @nome, endereco = @end, telefone = @tel, cargo = @cargo, status = @status WHERE guidFuncionarios = @guidFuncionarios";
            }
            try
            {
                SqlCommand pedido = new SqlCommand();
                SqlConnection numero = new SqlConnection(_ligacao);
                pedido.Connection = numero;
                pedido.CommandType = CommandType.Text;
                pedido.CommandText = sql;

                string s;
                if (f.Estado == "Ativo")
                {
                    s = 1.ToString();
                }
                else
                {
                    s = 0.ToString();
                }
                pedido.Parameters.AddWithValue("@nome", f.Nome);
                pedido.Parameters.AddWithValue("@end", f.Endereco);
                pedido.Parameters.AddWithValue("@tel", f.Telefone);
                pedido.Parameters.AddWithValue("@cargo", f.Cargo);
                pedido.Parameters.AddWithValue("@status", s);

                if (f.GuidPessoa != "")
                {
                    pedido.Parameters.AddWithValue("@guidFuncionarios", f.GuidPessoa);
                }
                numero.Open();
                pedido.ExecuteNonQuery();
                numero.Close();
                erros = "";
            }
            catch (Exception ex)
            {
                erros = ex.Message;
            }
            return erros;
        }

        public string apagar(Funcionario f)
        {
            string erros = "";
            string sql = "";
            if (f.GuidPessoa != "")
            {
                //Criar Cliente
                sql = "DELETE FROM Funcionarios WHERE guidFuncionarios = @guidFuncionarios";

                try
                {
                    SqlCommand pedido = new SqlCommand();
                    SqlConnection numero = new SqlConnection(_ligacao);
                    pedido.Connection = numero;
                    pedido.CommandType = CommandType.Text;
                    pedido.CommandText = sql;
                    pedido.Parameters.AddWithValue("@guidFuncionarios", f.GuidPessoa);

                    numero.Open();
                    pedido.ExecuteNonQuery();
                    numero.Close();
                    erros = "";

                }
                catch (Exception ex)
                {
                    erros = ex.Message;
                }
            }
            else
            {
                erros = "Funcionario Inexistente";
            }

            return erros;
        }

        public Funcionario read(string uidFuncionarioALer)
        {
            Funcionario outFuncionario;
            DataTable listaBrutaFuncionarios = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection ConexaoBD = new SqlConnection(_ligacao);

            pedido.CommandType = CommandType.Text;
            pedido.CommandText = "SELECT * FROM Funcionarios WHERE guidFuncionarios=@uidFuncionario";
            pedido.Parameters.AddWithValue("@uidFuncionario", uidFuncionarioALer);
            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = ConexaoBD;
            ConexaoBD.Open();
            telefone.Fill(listaBrutaFuncionarios);
            ConexaoBD.Close();

            //Se existir, só pode haver um funcionario com o uid indicado
            if (listaBrutaFuncionarios.Rows.Count == 1)
            {
                //Criar um objecto Funcionario a partir do registo da tabela
                DataRow linha = listaBrutaFuncionarios.Rows[0];
                outFuncionario = new Funcionario(
                        linha["guidFuncionarios"].ToString(),
                        linha["nome"].ToString(),
                        linha["endereco"].ToString(),
                        linha["telefone"].ToString(),
                        linha["status"].ToString(),
                        linha["cargo"].ToString(),
                        linha["dtAdmissao"].ToString()
                );
            }
            else
            {
                outFuncionario = null;
            }
            return outFuncionario;
        }
    }
}
