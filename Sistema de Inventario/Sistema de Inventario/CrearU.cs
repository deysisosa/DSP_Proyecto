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
    public partial class CrearU : Form
    {
        public CrearU()
        {
            InitializeComponent();
        }
         Conexion cn = new Conexion();
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Hide();
        }


        public bool Existe(string usuario)
        {
            {
                string consulta = "SELECT COUNT(*) FROM usuarios WHERE Nombre_Usuario=@usuario";
                SqlCommand cmd = new SqlCommand(consulta, cn.conex());
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cn.conectar();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;
                
            }
            
        }
        
       

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

              if (txtNombre.Text != "" & txtApellido.Text != "" & txtUsuario.Text != "" & txtPass.Text != "")
           
            {
                try
                {
                    if (!Existe(txtUsuario.Text))
                    {
                        int user = 2;
                        SqlCommand cm = new SqlCommand();
                        cm.CommandText = "insert into usuarios values('" + txtNombre.Text + "','" + txtApellido.Text + "','" + txtUsuario.Text + "','" + txtPass.Text + "','"+user+"')";
                        cm.Connection = cn.conex();
                        cm.ExecuteNonQuery();
                        limpiar();
                        MessageBox.Show("El usuario se ha registrado exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cn.desconectar();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("El usuario ya se encuentra registrado","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        cn.desconectar();
                    }
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 8152:
                            MessageBox.Show("El usuario no debe ser mayor a 10 caracteres \n" + ex.Message + "","ATENCION",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            break;
                        default:
                            MessageBox.Show("Error desconocido");
                            break;
                    }

                    cn.desconectar();
                }
            }
           else { MessageBox.Show("Debe completar todos los campos", "¡ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                   
        }

        private void limpiar() {
            txtNombre.Clear();
            txtApellido.Clear();
            txtUsuario.Clear();
            txtPass.Clear();
        }

        private void CrearU_Load(object sender, EventArgs e)
        {

        }
    }
}
