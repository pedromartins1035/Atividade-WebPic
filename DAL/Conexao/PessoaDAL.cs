using DAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPicKnockout.Models;

namespace DAL
{
    public class PessoaDAL
    {
        public List<Pessoa> ListarPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "SELECT * FROM Pessoa";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pessoa pessoa = new Pessoa
                            {
                                Codigo = Convert.ToInt32(reader["Codigo"]),
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                DataNasc = Convert.ToDateTime(reader["DataNasc"]),
                                EstadoCivil = reader["EstadoCivil"].ToString(),
                                CPF = reader["CPF"].ToString(),
                                RG = reader["RG"].ToString()
                            };
                            pessoas.Add(pessoa);
                        }
                    }
                }
            }
            return pessoas;
        }

        public void AdicionarPessoa(Pessoa pessoa)
        {
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "INSERT INTO Pessoa (Nome, Sobrenome, DataNasc, EstadoCivil, CPF, RG) VALUES (@Nome, @Sobrenome, @DataNasc, @EstadoCivil, @CPF, @RG)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", pessoa.Sobrenome);
                    cmd.Parameters.AddWithValue("@DataNasc", pessoa.DataNasc);
                    cmd.Parameters.AddWithValue("@EstadoCivil", pessoa.EstadoCivil);
                    cmd.Parameters.AddWithValue("@CPF", pessoa.CPF);
                    cmd.Parameters.AddWithValue("@RG", pessoa.RG);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarPessoa(Pessoa pessoa)
        {
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "UPDATE Pessoa SET Nome = @Nome, Sobrenome = @Sobrenome, DataNasc = @DataNasc, EstadoCivil = @EstadoCivil, CPF = @CPF, RG = @RG WHERE Codigo = @Codigo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", pessoa.Sobrenome);
                    cmd.Parameters.AddWithValue("@DataNasc", pessoa.DataNasc);
                    cmd.Parameters.AddWithValue("@EstadoCivil", pessoa.EstadoCivil);
                    cmd.Parameters.AddWithValue("@CPF", pessoa.CPF);
                    cmd.Parameters.AddWithValue("@RG", pessoa.RG);
                    cmd.Parameters.AddWithValue("@Codigo", pessoa.Codigo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirPessoa(int codigo)
        {
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "DELETE FROM Pessoa WHERE Codigo = @Codigo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Pessoa DetalhesPessoa(int codigo)
        {
            Pessoa pessoa = new Pessoa();
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "SELECT * FROM Pessoa WHERE Codigo = @Codigo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pessoa.Codigo = Convert.ToInt32(reader["Codigo"]);
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.DataNasc = Convert.ToDateTime(reader["DataNasc"]);
                            pessoa.EstadoCivil = reader["EstadoCivil"].ToString();
                            pessoa.CPF = reader["CPF"].ToString();
                            pessoa.RG = reader["RG"].ToString();
                        }
                    }
                }
            }
            return pessoa;
        }
    }
}
