using Sistema_de_Vendas.Uiform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Vendas
{
    public partial class frmAdminDashboard : Form
    {
        public frmAdminDashboard()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers user = new frmUsers();
            user.Show();
        }

        private void faturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void frmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            //quando for fechado a tela o painel administrativo, aparecerá a tela de login 
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
            //iniciando i timer
            timer1.Start();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoria categoria = new frmCategoria();
            categoria.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //inserindo o horario ao usuario logar
            DateTime datetime = DateTime.Now;
            this.lblDataEHora.Text = datetime.ToString();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts product = new frmProducts();
            product.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }
    }
}
