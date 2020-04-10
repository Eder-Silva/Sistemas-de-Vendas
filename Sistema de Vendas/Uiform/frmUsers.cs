using Sistema_de_Vendas.BLLClasses;
using Sistema_de_Vendas.DALdados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Vendas.Uiform
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }
        UserBLL u = new UserBLL();
        userDAL dal = new userDAL();
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
            dgvUser.Columns[0].HeaderText = "ID";
            dgvUser.Columns[1].HeaderText = "NOME"; 
            dgvUser.Columns[2].HeaderText = "SOBRENOME";
            dgvUser.Columns[3].HeaderText = "E-MAIL";
            dgvUser.Columns[4].HeaderText = "USUÁRIO";
            dgvUser.Columns[5].HeaderText = "SENHA";
            dgvUser.Columns[6].HeaderText = "TELEFONE";
            dgvUser.Columns[7].HeaderText = "ENDEREÇO";
            dgvUser.Columns[8].HeaderText = "SEXO";
            dgvUser.Columns[9].HeaderText = "TIPO USUÁRIO";
            dgvUser.Columns[10].HeaderText = "DATA/HORA";
            dgvUser.Columns[11].HeaderText = "CADASTRADO POR";
            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void addressUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
           

            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContato.Text;
            u.address = txtAddress.Text;
            u.genero = cmbGenero.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;

            string loggedUser = frmLogin.loggedIn;
            UserBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;

            bool success = dal.Insert(u);
            if(success == true)
            {
                MessageBox.Show("USUÁRIO CADASTRADO COM SUCESSO!");
                Limpar();
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL CADASTRAR USUÁRIO!");
             }
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }
        private void Limpar()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtContato.Text = "";
            txtAddress.Text = "";
            cmbGenero.Text = "";
            cmbUserType.Text = "";

        }

        private void dgvUser_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*ao clicar no datagrid os dados aparecerão dentro dos campos*/
            int rowIndex = e.RowIndex;
            txtUserID.Text = dgvUser.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUser.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUser.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUser.Rows[rowIndex].Cells[3].Value.ToString();
            txtUserName.Text = dgvUser.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUser.Rows[rowIndex].Cells[5].Value.ToString();
            txtContato.Text = dgvUser.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUser.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGenero.Text = dgvUser.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUser.Rows[rowIndex].Cells[9].Value.ToString();
            
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            u.id = Convert.ToInt32(txtUserID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUserName.Text;
            u.password = txtPassword.Text;
            u.contact = txtContato.Text;
            u.address = txtAddress.Text;
            u.genero = cmbGenero.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            bool success = dal.Update(u);
            if (success == true)
            {
                MessageBox.Show("USUÁRIO ATUALIZADO COM SUCESSO!");
                Limpar();
                
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL ATUALIZAR USUÁRIO!");
            }
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }

        private void bbtnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            u.id = Convert.ToInt32(txtUserID.Text);
            
            bool success = dal.Delete(u);
            if (success == true)
            {
                MessageBox.Show("USUÁRIO DELETADO COM SUCESSO!");
               Limpar();
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL DELETAR USUÁRIO!");
            }
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            // Obter as keywords(palavras-chave) do formulário
            string keywords = txtSearch.Text;
            if(keywords!= null)
            {

                // Pesquise os produtos
                DataTable dt = dal.Search(keywords);
                dgvUser.DataSource = dt;
            }
            else
            {

                // Exibir todos os produtos
                DataTable dt = dal.Search();
                dgvUser.DataSource = dt;
            }
        }
    }
}
