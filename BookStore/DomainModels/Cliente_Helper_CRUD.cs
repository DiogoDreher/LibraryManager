using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    class Cliente_Helper_CRUD
    {
        private string _ligacao;

        public Cliente_Helper_CRUD(string ligacaoAUtilizar)
        {
            _ligacao = ligacaoAUtilizar;
        }

        public List<Cliente> list(int estadoAVer)
        {
            List<Cliente> listaADevolver = new List<Cliente>();
            DataTable listaBrutaClientes = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection numero = new SqlConnection(_ligacao);

            pedido.CommandType = CommandType.Text;
            pedido.CommandText = (estadoAVer == Convert.ToInt32(1)) ? "SELECT * FROM Clientes WHERE status = " + estadoAVer.ToString() : "SELECT * FROM Clientes";

            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = numero;
            numero.Open();
            telefone.Fill(listaBrutaClientes);
            numero.Close();

            foreach (DataRow linha in listaBrutaClientes.Rows)
            {
                Cliente c = new Cliente(linha["guidCliente"].ToString());
                c.Nome = linha["nome"].ToString();
                c.Endereco = linha["endereco"].ToString();
                c.Telefone = linha["telefone"].ToString();
                c.Data = linha["dtRegisto"].ToString();
                c.Estado = linha["status"].ToString();
                listaADevolver.Add(c);
            }


            return listaADevolver;
        }

        public string atualizar(Cliente c)
        {
            string erros = "";
            string sql = "";
            if (c.GuidPessoa == "")
            {
                //Criar Cliente
                sql = "INSERT INTO Clientes (nome, endereco, telefone, status)";
                sql += "VALUES (@nome, @end, @tel, @status)";                
            }
            else
            {
                //Atualza Cliente
                sql = "UPDATE Clientes SET nome = @nome, endereco = @end, telefone = @tel, status = @status WHERE guidCliente = @guidCliente";
            }
            try
            {
                SqlCommand pedido = new SqlCommand();
                SqlConnection numero = new SqlConnection(_ligacao);
                pedido.Connection = numero;
                pedido.CommandType = CommandType.Text;
                pedido.CommandText = sql;

                string s;
                if (c.Estado == "Ativo")
                {
                    s = 1.ToString();
                }
                else
                {
                    s = 0.ToString();
                }
                pedido.Parameters.AddWithValue("@nome", c.Nome);
                pedido.Parameters.AddWithValue("@end", c.Endereco);
                pedido.Parameters.AddWithValue("@tel", c.Telefone);
                pedido.Parameters.AddWithValue("@status", s);

                if (c.GuidPessoa != "")
                {
                    pedido.Parameters.AddWithValue("@guidCliente", c.GuidPessoa);
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

        public string apagar(Cliente c)
        {
            string erros = "";
            string sql = "";
            if (c.GuidPessoa != "")
            {
                //Criar Cliente
                sql = "DELETE FROM Clientes WHERE guidCliente = @guidCliente";

                try
                {
                    SqlCommand pedido = new SqlCommand();
                    SqlConnection numero = new SqlConnection(_ligacao);
                    pedido.Connection = numero;
                    pedido.CommandType = CommandType.Text;
                    pedido.CommandText = sql;
                    pedido.Parameters.AddWithValue("@guidCliente", c.GuidPessoa);

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
                erros = "Cliente Inexistente";
            }

            return erros;
        }

        public Cliente read(string uidClienteALer)
        {
            Cliente outCliente;
            DataTable listaBrutaClientes = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection ConexaoBD = new SqlConnection(_ligacao);

            pedido.CommandType = CommandType.Text;
            pedido.CommandText = "SELECT * FROM Clientes WHERE guidCliente=@uidCliente";
            pedido.Parameters.AddWithValue("@uidCliente", uidClienteALer);
            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = ConexaoBD;
            ConexaoBD.Open();
            telefone.Fill(listaBrutaClientes);
            ConexaoBD.Close();

            //Se existir, só pode haver um cliente com o uid indicado
            if (listaBrutaClientes.Rows.Count == 1)
            {
                //Criar um objecto Cliente a partir do registo da tabela
                DataRow linha = listaBrutaClientes.Rows[0];
                outCliente = new Cliente(
                        linha["guidCliente"].ToString(),
                        linha["nome"].ToString(),
                        linha["endereco"].ToString(),
                        linha["telefone"].ToString(),
                        linha["status"].ToString(),
                        linha["dtRegisto"].ToString()
                );
            }
            else
            {
                outCliente = null;
            }
            return outCliente;
        }
    }
}
