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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        categoriaDAL cdal = new categoriaDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable categoriaDT = cdal.Select();
            cmbCategoria.DataSource = categoriaDT;
            cmbCategoria.DisplayMember = "titlie";
            cmbCategoria.ValueMember = "titlie";

            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            p.name = txtProductName.Text;
            p.category = cmbCategoria.Text;
            p.description = txtDescricaoProducts.Text;
            p.rate = decimal.Parse(txtValorProducts.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;

            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            bool Success = pdal.Insert(p);

            if (Success == true)
            {
                MessageBox.Show("PRODUTO CADASTRADO COM SUCESSO!");
                Limpar();

                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL CADASTRAR PRODUTO!");
            }
        }
        public void Limpar()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtDescricaoProducts.Text = "";
            txtValorProducts.Text = "";
            txtPesquisaProducts.Text = "";


        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtProductID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtProductName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategoria.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescricaoProducts.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();            
            txtValorProducts.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            Convert.ToInt32(txtProductID.Text);
            p.name = txtProductName.Text;            
            p.description = txtDescricaoProducts.Text;
            p.category = cmbCategoria.Text;
            p.rate = decimal.Parse(txtValorProducts.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;

            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            bool Success = pdal.Update(p);
            if (Success == true)
            {
                MessageBox.Show("PRODUTO ATUALIZADO COM SUCESSO!");
                Limpar();

                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL ATUALIZAR PRODUTO!");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            p.id = Convert.ToInt32(txtProductID.Text);

            bool success = pdal.Delete(p);
            if (success == true)
            {
                MessageBox.Show("PRUDUTO DELETADO COM SUCESSO!");
                Limpar();

                //atualiza a gridView
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("NÃO FOI POSSÍVEL DELETAR PRODUTO!");
            }
        }

        private void txtPesquisaProducts_TextChanged(object sender, EventArgs e)
        {
            {

                // Obter as keywords(palavras-chave) do formulário
                string keywords = txtPesquisaProducts.Text;
                if (keywords != null)
                {

                    // Pesquise os produtos
                    DataTable dt = pdal.Search(keywords);
                    dgvProducts.DataSource = dt;
                }
                else
                {

                    // Exibir todos os produtos
                    DataTable dt = pdal.Search();
                    dgvProducts.DataSource = dt;
                }
            }
        }

        private void bbtnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }
    }
}
