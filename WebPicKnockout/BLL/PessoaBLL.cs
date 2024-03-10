using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebPicKnockout.DAL;
using WebPicKnockout.Models;

namespace WebPicKnockout.BLL
{
    public class PessoaBLL
    {
        public List<Pessoa> ListarPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "SELECT * FROM tbPessoa";
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
                string query = "INSERT INTO tbPessoa (nome, sobrenome, dataNasc, estadoCivil, cpf, rg) VALUES (@nome, @sobrenome, @dataNasc, @estadoCivil, @cpf, @rg)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@sobrenome", pessoa.Sobrenome);
                    cmd.Parameters.AddWithValue("@dataNasc", pessoa.DataNasc);
                    cmd.Parameters.AddWithValue("@estadoCivil", pessoa.EstadoCivil);
                    cmd.Parameters.AddWithValue("@cpf", pessoa.CPF);
                    cmd.Parameters.AddWithValue("@rg", pessoa.RG);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarPessoa(Pessoa pessoa)
        {
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "UPDATE tbPessoa SET nome = @nome, sobrenome = @sobrenome, dataNasc = @dataNasc, estadoCivil = @estadoCivil, cpf = @cpf, rg = @rg WHERE codigo = @codigo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@sobrenome", pessoa.Sobrenome);
                    cmd.Parameters.AddWithValue("@dataNasc", pessoa.DataNasc);
                    cmd.Parameters.AddWithValue("@estadoCivil", pessoa.EstadoCivil);
                    cmd.Parameters.AddWithValue("@cpf", pessoa.CPF);
                    cmd.Parameters.AddWithValue("@rg", pessoa.RG);
                    cmd.Parameters.AddWithValue("@codigo", pessoa.Codigo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirPessoa(int codigo)
        {
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "DELETE FROM tbPessoa WHERE codigo = @codigo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Pessoa DetalhesPessoa(int? codigo)
        {
            Pessoa pessoa = new Pessoa();
            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "SELECT * FROM tbPessoa WHERE codigo = @codigo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pessoa.Codigo = Convert.ToInt32(reader["codigo"]);
                            pessoa.Nome = reader["nome"].ToString();
                            pessoa.Sobrenome = reader["sobrenome"].ToString();
                            pessoa.DataNasc = Convert.ToDateTime(reader["dataNasc"]);
                            pessoa.EstadoCivil = reader["estadoCivil"].ToString();
                            pessoa.CPF = reader["cpf"].ToString();
                            pessoa.RG = reader["rg"].ToString();
                        }
                    }
                }
            }
            return pessoa;
        }

        public bool VerificarExistenciaCPF(string CPF)
        {
            bool existe = false;

            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "SELECT COUNT(*) FROM tbPessoa WHERE cpf = @cpf";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@cpf", CPF);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }
            }

            return existe;
        }

        public bool VerificarExistenciaRG(string rg)
        {
            bool existe = false;

            using (SqlConnection con = ConexaoBD.GetConexao())
            {
                string query = "SELECT COUNT(*) FROM tbPessoa WHERE rg = @rg";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@rg", rg);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }
            }

            return existe;
        }


        #region Método para validar CPF e RG

        public bool ValidaCPF(string CPF)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;

            int soma;
            int resto;

            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "").Replace("-", "");

            if (CPF.Length != 11)
                return false;

            switch (CPF)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "2222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            tempCpf = CPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return CPF.EndsWith(digito);
        }

        public bool ValidaRG(string RG) //uma verificação simples só pra falar que teve algo, ja que o rg não possui formatação logica nem calculo por digito verificador
        {
            // Remove espaços em branco e formatações do RG
            RG = RG.Trim();
            RG = RG.Replace(".", "").Replace("-", "");

            // Verifica se o RG possui apenas números
            if (!Regex.IsMatch(RG, @"^\d+$"))
                return false;

            // Verifica o tamanho do RG
            if (RG.Length != 9)
                return false;

            // Lista de RGs inválidos (opcional, adicione conforme necessário)
            string[] rgInvalidos = { "000000000", "111111111", "222222222", "333333333", "444444444", "555555555", "666666666", "777777777", "888888888", "999999999" };

            // Verifica se o RG é inválido
            if (rgInvalidos.Contains(RG))
                return false;

            return true;
        }


        #endregion
    }
}