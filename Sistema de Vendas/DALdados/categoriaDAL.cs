using Sistema_de_Vendas.BLLClasses;
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
    class categoriaDAL
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
                String sql = "SELECT * FROM tbl_categories";
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
        public bool Insert(categoriaBLL c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "INSERT INTO tbl_categories(titlie, description, added_date, added_by) " +
                             "VALUES(@titlie, @description, @added_date, @added_by) ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@titlie", c.titlie);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                

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
        public bool Update(categoriaBLL c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "UPDATE tbl_categories SET titlie=@titlie, " +
                                                    "description=@description, " +
                                                    "added_date=@added_date, " +
                                                    "added_by=@added_by " +                                                                      
                                                    "WHERE id=@id";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@titlie", c.titlie);
                cmd.Parameters.AddWithValue("@description", c.description);                
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

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
        public bool Delete(categoriaBLL c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "DELETE FROM tbl_categories WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", c.id);

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
    }
}
