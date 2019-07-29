using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DomainModels
{
    class Livro_Helper_CRUD
    {
        private string _ligacao;

        public Livro_Helper_CRUD(string ligacaoAUtilizar)
        {
            _ligacao = ligacaoAUtilizar;
        }

        public List<Livro> list(int estadoAVer)
        {
            List<Livro> listaADevolver = new List<Livro>();
            DataTable listaBrutaLivros = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection numero = new SqlConnection(_ligacao);

            pedido.CommandType = CommandType.Text;
            pedido.CommandText = "SELECT * FROM Livros WHERE status = " + estadoAVer.ToString();

            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = numero;
            numero.Open();
            telefone.Fill(listaBrutaLivros);
            numero.Close();

            foreach (DataRow linha in listaBrutaLivros.Rows)
            {
                Livro l = new Livro(linha["guidLivro"].ToString());
                l.Nome = linha["nome"].ToString();
                l.Autor = linha["autor"].ToString();
                l.Ano = Convert.ToInt32(linha["anoLancamento"]);
                l.Genero = linha["genero"].ToString();
                l.Editora = linha["editora"].ToString();
                l.Data = linha["dtRegisto"].ToString();
                l.Estado = linha["status"].ToString();
                l.Emprestimo = linha["dtEmprestimo"].ToString();
                l.Emprestimo = l.Emprestimo.Substring(0, 10);
                listaADevolver.Add(l);
            }


            return listaADevolver;
        }

        public string atualizar(Livro l)
        {
            string erros = "";
            string sql = "";
            if (l.GuidLivro == "")
            {
                //Criar Livro
                sql = "INSERT INTO Livros (nome, autor, anoLancamento, genero, editora, dtEmprestimo, status)";
                sql += "VALUES (@nome,@autor, @ano, @gen, @editora, @emp, @status)";
            }
            else
            {
                //Atualza Livro
                sql = "UPDATE Livros SET nome = @nome, autor = @autor, anoLancamento = @ano, genero = @gen, editora = @editora, dtEmprestimo = @emp, status = @status WHERE guidLivro = @guidLivro";
            }
            try
            {
                SqlCommand pedido = new SqlCommand();
                SqlConnection numero = new SqlConnection(_ligacao);
                pedido.Connection = numero;
                pedido.CommandType = CommandType.Text;
                pedido.CommandText = sql;

               
                pedido.Parameters.AddWithValue("@nome", l.Nome);
                pedido.Parameters.AddWithValue("@autor", l.Autor);
                pedido.Parameters.AddWithValue("@ano", l.Ano);
                pedido.Parameters.AddWithValue("@gen", l.Genero);
                pedido.Parameters.AddWithValue("@editora", l.Editora);
                pedido.Parameters.AddWithValue("@emp", l.Emprestimo);
                pedido.Parameters.AddWithValue("@status", 1);

                if (l.GuidLivro != "")
                {
                    pedido.Parameters.AddWithValue("@guidLivro", l.GuidLivro);
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

        public string apagar(Livro l)
        {
            string erros = "";
            string sql = "";
            if (l.GuidLivro != "")
            {
                //Criar Cliente
                sql = "DELETE FROM Livros WHERE guidLivro = @guidLivro";

                try
                {
                    SqlCommand pedido = new SqlCommand();
                    SqlConnection numero = new SqlConnection(_ligacao);
                    pedido.Connection = numero;
                    pedido.CommandType = CommandType.Text;
                    pedido.CommandText = sql;
                    pedido.Parameters.AddWithValue("@guidLivro", l.GuidLivro);

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
                erros = "Livro Inexistente";
            }

            return erros;
        }

        public Livro read(string uidLivroALer)
        {
            Livro outLivro;
            DataTable listaBrutaLivros = new DataTable();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand pedido = new SqlCommand();
            SqlConnection ConexaoBD = new SqlConnection(_ligacao);

            pedido.CommandType = CommandType.Text;
            pedido.CommandText = "SELECT * FROM Livros WHERE guidLivro=@guidLivro";
            pedido.Parameters.AddWithValue("@guidLivro", uidLivroALer);
            telefone.SelectCommand = pedido;
            telefone.SelectCommand.Connection = ConexaoBD;
            ConexaoBD.Open();
            telefone.Fill(listaBrutaLivros);
            ConexaoBD.Close();

            //Se existir, só pode haver um cliente com o uid indicado
            if (listaBrutaLivros.Rows.Count == 1)
            {
                //Criar um objecto Cliente a partir do registo da tabela
                DataRow linha = listaBrutaLivros.Rows[0];
                outLivro = new Livro(
                        linha["guidLivro"].ToString(),
                        linha["nome"].ToString(),
                        linha["autor"].ToString(),
                        Convert.ToInt32(linha["anoLancamento"]),
                        linha["genero"].ToString(),
                        linha["editora"].ToString(),
                        linha["status"].ToString(),
                        linha["dtRegisto"].ToString(),
                        linha["dtEmprestimo"].ToString()
                );
            }
            else
            {
                outLivro = null;
            }
            return outLivro;
        }
    }
}
