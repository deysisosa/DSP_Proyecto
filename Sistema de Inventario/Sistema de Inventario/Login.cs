using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Inventario
{
    public partial class Login : Form
    {
        Conexion cn = new Conexion();
        
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inicio inicio = new Inicio();
            inicio.Show();
        }

       

        private void btn_Ingresar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "" && txtPass.Text != "")
            {
                cn.conectar();
                string consulta = "select * from usuarios where Nombre_Usuario='"+txtUser.Text+"'";
                SqlCommand pass = new SqlCommand(consulta,cn.conex());
                SqlDataReader leer = pass.ExecuteReader();
                if (leer.Read() == true)
                {
                    if (txtPass.Text == leer["Contraseña"].ToString())
                    {
                     
                        this.Hide();
                        Panel_Control panel = new Panel_Control(txtUser.Text);
                        panel.Show();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPass.Clear();
                    }
                }
                else {
                    MessageBox.Show("EL usuario es incorrecto o no existe","VERIFICAR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtUser.Clear();
                    txtPass.Clear();
                }
                
            }
            else
            {
                MessageBox.Show("Debe completar los campos","¡ATENCION!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            }
            cn.desconectar();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            btn_Salir.PerformClick();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
