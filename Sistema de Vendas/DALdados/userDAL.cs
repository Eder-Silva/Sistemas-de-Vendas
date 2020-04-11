using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Sistema_de_Vendas.BLLClasses;

namespace Sistema_de_Vendas.DALdados
{
    class userDAL
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
                String sql = "SELECT * FROM tbl_usuario";
                //SqlCommand - execução em um banco de dados SQL Server. 
                SqlCommand cmd = new SqlCommand(sql,conn);
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
        public bool Insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "INSERT INTO tbl_usuario(first_name, last_name, email, username, password, contact, address, genero, user_type, added_date, added_by) VALUES(@first_name, @last_name, @email, @username, @password, @contact, @address, @genero, @user_type, @added_date, @added_by) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@genero", u.genero);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

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
            catch(Exception ex)
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
        public bool Update(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "UPDATE tbl_usuario SET first_name=@first_name, " +
                    "last_name=@last_name, " +
                    "email=@email, username=@username, " +
                    "password=@password, " +
                    "contact=@contact," +
                    "address=@address, " +
                    "genero=@genero," +
                    "user_type=@user_type, " +
                    "added_date=@added_date, " +
                    "added_by=@added_by " +
                    "WHERE id=@id";


                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@genero", u.genero);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

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
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);
            try
            {
                string sql = "DELETE FROM tbl_usuario WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);                
                cmd.Parameters.AddWithValue("@id", u.id);

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
                string sql = "SELECT * FROM tbl_usuario WHERE id LIKE '%" + keywords + "%' OR first_name LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%' OR username LIKE '%" + keywords + "%'   ";

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

        #region pegando o valor de ID de administrador ou usuario queestiver logado e enviar para o banco
        public userBLL GetIDFromUsername(string username)
        {
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myConnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_usuario WHERE username='" + username + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString()); 
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return u;
        }

        #endregion
    }

}
