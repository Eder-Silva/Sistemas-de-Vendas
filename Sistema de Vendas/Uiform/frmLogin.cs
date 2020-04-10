using Sistema_de_Vendas.BLLClasses;
using Sistema_de_Vendas.DALdados;
using System;
using System.Windows.Forms;

namespace Sistema_de_Vendas.Uiform
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //botão fechar
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtusername.Text.Trim();
            l.password = txtsenha.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            bool Success = dal.loginCheck(l);
            if(Success == true)
            {
                MessageBox.Show("ACESSO PERMITIDO");
                loggedIn = l.username;
                switch (l.user_type)
                {
                    case "Administrador":
                        {
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;

                    case "Usuario":
                        {
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;

                    default:
                        {
                            MessageBox.Show("FAVOR CONFERIR USUÁRIO E SENHA");
                        }
                        break;


                }
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL CADASTRAR USUARIO");
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
