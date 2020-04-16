﻿using Sistema_de_Vendas.BLLClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Vendas.DALdados
{
    class productsDAL
    {
        static string myConnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region selecionar dados do database
        //ConfigurationManager - Fornece acesso a arquivos de configuração para aplicativos cliente.
        //ConnectionStrings - Obtém os dados de ConnectionStringsSection da configuração padrão do aplicativo atual.
        //.ConnectionString - Obtém ou define a cadeia de caracteres usada para abrir um banco de dados do SQL Server.

        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myConnstring);
            //DataTable - Representa uma tabela de dados na memória.
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_produto";
                //SqlCommand - execução em um banco de dados SQL Server. 
                SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlDataAdapter - preencher o DataSet e atualizar o banco de dados do SQL Server.
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                //FILL - Preenche um DataSet ou DataTable.
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //MessageBox - Exibe uma janela de mensagem, também conhecida como uma caixa de diálogo que exibe uma mensagem ao usuário.
                //Show() - Exibe uma caixa de mensagem.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion

        #region inserir  dados no bd
        public bool Insert(productsBLL p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "INSERT INTO tbl_produto(name, category, description, preco, qtd, added_date, added_by) " +
                             "VALUES(@name, @category, @description, @preco, @qtd, @added_date, @added_by) ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@preco", p.rate);
                cmd.Parameters.AddWithValue("@qtd", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);


                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Atualizar os dados do bd
        public bool Update(productsBLL p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "UPDATE tbl_produto SET name=@name, " +
                                                    "category=@category, " +
                                                    "description=@description, " +
                                                    "preco=@preco, " +
                                                    "qtd=@qtd, " +
                                                    "added_date=@added_date, " +
                                                    "added_by=@added_by " +                                                    
                                                    "WHERE id=@id";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@preco", p.rate);
                cmd.Parameters.AddWithValue("@qtd", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion

        #region deletar os dados do bd
        public bool Delete(productsBLL p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "DELETE FROM tbl_produto WHERE id=@id";
                //passando valor usando cmd
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", p.id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        internal DataTable Search()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Método de Pesquisa
        public DataTable Search(string keywords)
        {
            //Conexão SQL para conexão com banco de dados
            SqlConnection conn = new SqlConnection(myConnstring);

            //DataTable - Representa uma tabela de dados na memória.
            // Criando DataTable para manter o valor do dAtabase
            DataTable dt = new DataTable();

            try
            {
                // consulta SQL para pesquisar usuarios
                string sql = "SELECT * FROM tbl_produto WHERE id LIKE '%" + keywords + "%' " +
                                                                "OR name LIKE '%" + keywords + "%' " +
                                                                "OR category LIKE '%" + keywords + "%' " +
                                                                "OR qty LIKE '%" + keywords + "%' " +
                                                                "OR description LIKE '%" + keywords + "%'     ";

                // Comando SQL para executar Consulta
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SqlDataAdapter - preencher o DataSet e atualizar o banco de dados do SQL Server.
                // SQL Data Adapter para reter os dados do banco de dados temporariamente
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Abrir conexão com o banco de dados
                conn.Open();

                //FILL - Preenche um DataSet ou DataTable.
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //MessageBox - Exibe uma janela de mensagem, também conhecida como uma caixa de diálogo que exibe uma mensagem ao usuário.
                //Show() - Exibe uma caixa de mensagem.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
    }
}
