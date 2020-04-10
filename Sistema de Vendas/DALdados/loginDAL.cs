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
    class loginDAL
    {
        static string myConnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        public bool loginCheck(loginBLL l)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConnstring);

            try
            {
                string sql = "SELECT * FROM tbl_usuario WHERE username=@username AND password=@password AND user_type=@user_type";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                //SqlDataAdapter - preencher o DataSet e atualizar o banco de dados do SQL Server.
                // SQL Data Adapter para reter os dados do banco de dados temporariamente
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //DataTable - Representa uma tabela de dados na memória.
                // Criando DataTable para manter o valor do dAtabase
                //Um DataTable é como um container que podemos usar para armazenar informações de 
                //praticamente qualquer fonte de dados, sendo composto por uma coleção de linhas (rows)
                //e colunas (columns)
                DataTable dt = new DataTable();

                //Um SqlCommand usado durante o Fill(DataSet) 
                //para selecionar registros do banco de dados para posicionamento no DataSet.
                //FILL - Preenche um DataSet ou DataTable.
                adapter.Fill(dt);

                if (dt.Rows.Count>0)
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
    }
}
