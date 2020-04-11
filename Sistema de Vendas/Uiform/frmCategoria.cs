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
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        categoriaBLL c = new categoriaBLL();
        categoriaDAL dal = new categoriaDAL();
        userDAL udal = new userDAL();

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            //REALIZA ATUALIZAÇÃO NA TABELA AO  CARREGAR A TELA
            DataTable dt = dal.Select();
            dgvCategoria.DataSource = dt;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            //pegando o valor dos campos dor form e enviando para o bd
            c.titlie = txtTitulo.Text;
            c.description = txtdescricao.Text;
            c.added_date = DateTime.Now;                      

            //pegando o valor do id do usuario logado
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            c.added_by = usr.id;
            bool success = dal.Insert(c);
            //inserindo os dados no bd
            if (success == true)
            {
                MessageBox.Show("PRODUTO CADASTRADO COM SUCESSO!");
                Limpar();

                //REALIZA ATUALIZAÇÃO NA TABELA AO INSERIR UM NOVO DADO
                DataTable dt = dal.Select();
                dgvCategoria.DataSource = dt;
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL CADASTRAR PRODUTO!");
            }

            
        }
        //metodo para limpartodos os campos depois de inserir os valores no bd
        private void Limpar()
        {
            txtCategoriaId.Text = "";
            txtTitulo.Text = "";
            txtdescricao.Text = "";
            txtPesquisa.Text = "";            

        }

        private void bbtnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void dgvCategoria_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*ao clicar no datagrid os dados aparecerão dentro dos campos*/
            int rowIndex = e.RowIndex;
            txtCategoriaId.Text = dgvCategoria.Rows[rowIndex].Cells[0].Value.ToString();
            txtTitulo.Text = dgvCategoria.Rows[rowIndex].Cells[1].Value.ToString();
            txtdescricao.Text = dgvCategoria.Rows[rowIndex].Cells[2].Value.ToString();
           
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            c.id = Convert.ToInt32(txtCategoriaId.Text);
            c.titlie = txtTitulo.Text;
            c.description = txtdescricao.Text;           
            c.added_date = DateTime.Now;

            //pegando o id do usuario logado
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            c.added_by = usr.id;
            

            bool success = dal.Update(c);
            if (success == true)
            {
                MessageBox.Show("CATEGORIA ATUALIZADA COM SUCESSO!");
                Limpar();

                //REALIZA ATUALIZAÇÃO NA TABELA AO ATUALIZAR UM NOVO DADO
                DataTable dt = dal.Select();
                dgvCategoria.DataSource = dt;
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL ATUALIZAR A CATEGORIA!");
            }

        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            c.id = Convert.ToInt32(txtCategoriaId.Text);

            bool success = dal.Delete(c);
            if (success == true)
            {
                MessageBox.Show("PRUDUTO DELETADO COM SUCESSO!");
                Limpar();
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL DELETAR PRODUTO!");
            }
            DataTable dt = dal.Select();
            dgvCategoria.DataSource = dt;
        }
    }
    }

