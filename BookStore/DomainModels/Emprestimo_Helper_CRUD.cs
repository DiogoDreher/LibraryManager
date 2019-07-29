using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    class Emprestimo_Helper_CRUD
    {
        private string _ligacao;

        public Emprestimo_Helper_CRUD(string ligacaoAUtilizar)
        {
            _ligacao = ligacaoAUtilizar;
        }

        public List<Emprestimo> list(Cliente c, Livro l, Funcionario f, DateTime dt, int estado)
        {
            List<Emprestimo> listaADevolver = new List<Emprestimo>();
            DataTable listaBrutaEmp = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection numero = new SqlConnection(_ligacao);

            string est;

            string param1 = "estado = @estado3 ORDER BY dtEmprestimo DESC";
            string param2 = "estado = @estado1 OR estado = @estado2 ORDER BY dtEmprestimo DESC";

            if (estado == Convert.ToInt32(1))
            {
                est = param1;
            }
            else
            {
                est = param2;
            }

            

            #region: "Queries"
            pedido.CommandType = CommandType.Text;
            string pedidoParaSQL = "SELECT * FROM emp WHERE " + est ;

            if (c == null && l == null && f == null && dt == DateTime.Today.AddDays(1))
            {
                pedido.CommandText = pedidoParaSQL;
            }
            else if (dt != DateTime.Today.AddDays(1) && c != null)
            {
                pedidoParaSQL = "SELECT * FROM emp WHERE " +
                                "uidCliente = @uidCliente AND " +
                                "dtEmprestimo = @dtEmp AND " +
                                est;
                pedido.CommandText = pedidoParaSQL;
                pedido.Parameters.AddWithValue("@uidCliente", c.GuidPessoa);
                pedido.Parameters.AddWithValue("@dtEmp", dt);
            }
            else if (dt != DateTime.Today.AddDays(1) && f != null)
            {
                pedidoParaSQL = "SELECT * FROM emp WHERE " +
                                "uidFuncionario = @uidFuncionario AND " +
                                "dtEmprestimo = @dtEmp AND " +
                                est;
                pedido.CommandText = pedidoParaSQL;
                pedido.Parameters.AddWithValue("@uidFuncionario", f.GuidPessoa);
                pedido.Parameters.AddWithValue("@dtEmp", dt);
            }
            else if (dt != DateTime.Today.AddDays(1))
            {
                pedidoParaSQL = "SELECT * FROM emp WHERE " +
                                "dtEmprestimo = @dtEmp AND " +
                                est;
                pedido.CommandText = pedidoParaSQL;
                pedido.Parameters.AddWithValue("@dtEmp", dt);
            }
            else if (c != null && f != null)
            {
                pedidoParaSQL = "SELECT * FROM emp WHERE " +
                                "uidCliente = @uidCliente AND " +
                                "uidFuncionario = @uidFuncionario AND " +
                                est;
                pedido.CommandText = pedidoParaSQL;
                pedido.Parameters.AddWithValue("@uidCliente", c.GuidPessoa);
                pedido.Parameters.AddWithValue("@uidFuncionario", f.GuidPessoa);
            }
            else if (c != null)
            {
                pedidoParaSQL = "SELECT * FROM emp WHERE " +
                                "uidCliente = @uidCliente AND " +
                                est;
                pedido.CommandText = pedidoParaSQL;
                pedido.Parameters.AddWithValue("@uidCliente", c.GuidPessoa);
            }
            else if (l != null)
            {
                pedidoParaSQL = "SELECT * FROM emp WHERE " +
                                "uidLivro = @uidLivro AND " +
                                est;
                pedido.CommandText = pedidoParaSQL;
                pedido.Parameters.AddWithValue("@uidLivro", l.GuidLivro);
            }
            else if (f != null)
            {
                pedidoParaSQL = "SELECT * FROM emp WHERE " +
                                "uidFuncionario = @uidFuncionario AND " +
                                est;
                pedido.CommandText = pedidoParaSQL;
                pedido.Parameters.AddWithValue("@uidFuncionario", f.GuidPessoa);
            }
            #endregion

            pedido.Parameters.AddWithValue("@estado1", "Em dia.");
            pedido.Parameters.AddWithValue("@estado2", "Atrasado.");
            pedido.Parameters.AddWithValue("@estado3", "Devolvido.");
            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = numero;
            numero.Open();
            telefone.Fill(listaBrutaEmp);
            numero.Close();

            foreach (DataRow linha in listaBrutaEmp.Rows)
            {
                Emprestimo emp = new Emprestimo(linha["uidEmprestimo"].ToString());
                //Ler Clientes, Livros e Funcionarios do emprestimo
                Cliente_Helper_CRUD chc = new Cliente_Helper_CRUD(_ligacao);
                emp.ClienteEmp = chc.read(linha["uidCliente"].ToString());
                Funcionario_Helper_CRUD fhc = new Funcionario_Helper_CRUD(_ligacao);
                emp.FuncionarioEmp = fhc.read(linha["uidFuncionario"].ToString());
                Livro_Helper_CRUD lhc = new Livro_Helper_CRUD(_ligacao);
                emp.LivroEmp = lhc.read(linha["uidLivro"].ToString());
                //Resto da informacao
                emp.Data = linha["dtRegisto"].ToString();
                emp.DataEmp = Convert.ToDateTime(linha["dtEmprestimo"]);
                if (estado == Convert.ToInt32(1))
                {
                    emp.DataDevolucao = Convert.ToDateTime(linha["dtDevolucao"]);
                }
                
                emp.Estado = linha["estado"].ToString();
                //Adiciona a lista de saida
                listaADevolver.Add(emp);
            }
            return listaADevolver;
        }

        public Emprestimo read(string uidEmprestimo)
        {
            Emprestimo outEmprestimo;
            DataTable listaBrutaEmprestimo = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection ConexaoBD = new SqlConnection(_ligacao);

            pedido.CommandType = CommandType.Text;
            string comandoParaSQL = "SELECT* FROM emp WHERE uidEmprestimo = @uidEmprestimo";
            pedido.CommandText = comandoParaSQL;
            pedido.Parameters.AddWithValue("@uidEmprestimo", uidEmprestimo);
            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = ConexaoBD;
            ConexaoBD.Open();
            telefone.Fill(listaBrutaEmprestimo);
            ConexaoBD.Close();

            //Se existir, só pode haver uma fatura com o uid indicado
            if (listaBrutaEmprestimo.Rows.Count == 1)
            {
                //Criar um objecto Fatura a partir do registo da tabela
                DataRow linha = listaBrutaEmprestimo.Rows[0];
                outEmprestimo = new Emprestimo(linha["uidEmprestimo"].ToString());
                //Ler o Cliente completo dessa fatura
                Cliente_Helper_CRUD ch = new Cliente_Helper_CRUD(_ligacao);
                outEmprestimo.ClienteEmp = ch.read(linha["uidCliente"].ToString());
                Funcionario_Helper_CRUD fhc = new Funcionario_Helper_CRUD(_ligacao);
                outEmprestimo.FuncionarioEmp = fhc.read(linha["uidFuncionario"].ToString());
                Livro_Helper_CRUD lhc = new Livro_Helper_CRUD(_ligacao);
                outEmprestimo.LivroEmp = lhc.read(linha["uidLivro"].ToString());
                //E preencher o resto da informação
                outEmprestimo.Data = linha["dtRegisto"].ToString();
                outEmprestimo.DataEmp = Convert.ToDateTime(linha["dtEmprestimo"]);
                outEmprestimo.Estado = linha["estado"].ToString();
            }
            else
            {
                outEmprestimo = null;
            }
            return outEmprestimo;
        }



        public string atualizar(Emprestimo emp)
        {
            string erros = "";
            string sqlInstrucao = "INSERT INTO emp (uidCliente, uidFuncionario, uidLivro, dtEmprestimo, estado)" +
                                  " VALUES (@uidCliente, @uidFuncionario, @uidLivro, @dtEmprestimo, @estado);";
            string sqlInstrucao2 = "UPDATE Livros SET status = @status, dtEmprestimo = @emp WHERE guidLivro = @guidLivro";
            
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(_ligacao);
                cmd.CommandText = sqlInstrucao;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@uidCliente", emp.ClienteEmp.GuidPessoa);
                cmd.Parameters.AddWithValue("@uidFuncionario", emp.FuncionarioEmp.GuidPessoa);
                cmd.Parameters.AddWithValue("@uidLivro", emp.LivroEmp.GuidLivro);
                cmd.Parameters.AddWithValue("@dtEmprestimo", emp.DataEmp);
                cmd.Parameters.AddWithValue("@emp", emp.DataEmp);

                if (DateTime.Today > emp.DataEmp.AddDays(30))
                {
                    emp.Estado = "Atrasado.";
                }
                else
                {
                    emp.Estado = "Em dia.";
                }
                
                cmd.Parameters.AddWithValue("@estado", emp.Estado);
                cmd.Parameters.AddWithValue("@status", Convert.ToInt32(0));
                cmd.Parameters.AddWithValue("@guidLivro", emp.LivroEmp.GuidLivro);
                cmd.Connection.Open();                          
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlInstrucao2;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();                         
                cmd.Connection.Dispose();                      
                erros = "";
            }
            catch (Exception ex)
            {
                erros = ex.Message;
            }
            return erros;
        }




        public string apagar(Emprestimo emp)
        {
            string erros = "";
            string sqlInstrucao = "UPDATE emp SET estado = @estado, dtDevolucao = @devolucao WHERE uidEmprestimo = @uidEmprestimo";
            string sqlInstrucao2 = "UPDATE Livros SET status = @status, dtEmprestimo = @dtEmprestimo WHERE guidLivro = @guidLivro";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = new SqlConnection(_ligacao);
                cmd.CommandText = sqlInstrucao;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@estado", "Devolvido.");
                cmd.Parameters.AddWithValue("@devolucao", DateTime.Today);
                cmd.Parameters.AddWithValue("@uidEmprestimo", emp.uidEmprestimo);
                cmd.Parameters.AddWithValue("@status", Convert.ToInt32(1));
                cmd.Parameters.AddWithValue("@guidLivro", emp.LivroEmp.GuidLivro);
                cmd.Parameters.AddWithValue("@dtEmprestimo", "");
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlInstrucao2;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                erros = "";
            }
            catch (Exception ex)
            {
                erros = ex.Message;
            }
            return erros;
        }
    }
}
