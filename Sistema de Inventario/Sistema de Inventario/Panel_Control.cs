using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Inventario
{
    public partial class Panel_Control : Form
    {
        public Panel_Control(String Usuario)
        {
          
            InitializeComponent();
            this.lblUsuario.Text = Usuario;
            Tiempo.Enabled = true;
            Show();
            MessageBox.Show("Bienvenido Usuario: " + lblUsuario.Text + "", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
           
        }

        private void Panel_Control_Load(object sender, EventArgs e)
        {
           
            
        }

        private void Tiempo_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void Panel_Control_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {

                e.Cancel = true;

            }     
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Saldra de la aplicacion?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void formabierto(Form hijo, Form padre)
        {
            bool cargado = false;
            foreach (Form llamado in padre.MdiChildren)
            {
                if (llamado.Text == hijo.Text)
                {
                    cargado = true;
                    break;
                }
            }
            if (!cargado)
            {
                hijo.MdiParent = padre;
                hijo.Show();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Categorias categoria = new Categorias();
            formabierto(categoria, this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Proveedores proveedor = new Proveedores();
            formabierto(proveedor, this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Estantes estante = new Estantes();
            formabierto(estante, this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bodega bodega = new Bodega();
            formabierto(bodega, this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Productos producto = new Productos();
            formabierto(producto, this);
        }

       
    }
}
