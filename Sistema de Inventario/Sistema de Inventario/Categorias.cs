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
    public partial class Categorias : Form
    {
        Conexion cn = new Conexion();
        public Categorias()
        {
            InitializeComponent();
            
        }

        private void Categorias_Load(object sender, EventArgs e)
        {
            cargardatos();
            Modificar.Enabled = false;
            Eliminar.Enabled = false;
        
        }
        public void cargardatos(){
            TablaC.AllowUserToAddRows = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select* from Categoria", cn.conex());
            datos.Fill(tabla);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = tabla;
            tabla.Columns[0].ColumnName = "ID Categoria";
            tabla.Columns[1].ColumnName = "Categoria";
            txtNombre.Enabled = false;
            Agregar.Enabled = false;
            TablaC.DataSource = bSource;            
            
        }
        private void Cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            Agregar.Enabled = true;
            btnNuevo.Enabled = false;
            Modificar.Enabled = false;
            Eliminar.Enabled = false;
            id.Clear();
            txtNombre.Clear();
        }

        public bool Existe(string usuario)
        {
            {
                string consulta = "SELECT COUNT(*) FROM categoria WHERE Nombre_categoria=@nombre";
                SqlCommand cmd = new SqlCommand(consulta, cn.conex());
                cmd.Parameters.AddWithValue("@nombre", usuario);
                cn.conectar();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;

            }

        }
        private void Agregar_Click(object sender, EventArgs e)
        {
              if (txtNombre.Text != "")
           
            {
                try
                {
                    if (!Existe(txtNombre.Text))
                    {
                        SqlCommand cm = new SqlCommand();
                        cm.CommandText = "insert into categoria values('" + txtNombre.Text + "')";
                        cm.Connection = cn.conex();
                        cm.ExecuteNonQuery();
                        txtNombre.Clear();
                        cargardatos();
                        cn.desconectar();
                        txtNombre.Enabled = false;
                        Agregar.Enabled = false;
                        btnNuevo.Enabled = true;
                        MessageBox.Show("La categoria ha sido registrada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
                    }
                    else
                    {
                        MessageBox.Show("La categoria que intenta ingresar ya existe","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        cn.desconectar();
                    }
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        default:
                            MessageBox.Show("Error desconocido");
                            break;
                    }

                    cn.desconectar();
                }
            }
           else { MessageBox.Show("Debe ingresar el nombre de la categoria", "¡ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                   
        }
        private void TablaC_CellClick(object sender, DataGridViewCellEventArgs e)
            
        {
            txtNombre.Enabled = true;
            Modificar.Enabled = true;
            Eliminar.Enabled = true;
            btnNuevo.Enabled = true;
            Agregar.Enabled = false;
            id.Text = Convert.ToString(this.TablaC.CurrentRow.Cells[0].Value);
            txtNombre.Text = Convert.ToString(this.TablaC.CurrentRow.Cells[1].Value);
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "delete from categoria where id_categoria='" + id.Text + "'";
                SqlCommand cm = new SqlCommand(consulta, cn.conex());
                cm.ExecuteNonQuery();
                cn.desconectar();
                MessageBox.Show("Registro eliminado correctamente","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cargardatos();
                txtNombre.Clear();
                id.Clear();
                Eliminar.Enabled = false;
                Modificar.Enabled = false;
            }
            

        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Actualizar datos de categoria?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "update categoria set Nombre_categoria='"+txtNombre.Text+"' where id_categoria='"+id.Text+"'";
                SqlCommand cm = new SqlCommand(consulta, cn.conex());
                cm.ExecuteNonQuery();
                cn.desconectar();
                MessageBox.Show("Categoria actualizada correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargardatos();
                txtNombre.Clear();
                id.Clear();
                Modificar.Enabled = false;
                Eliminar.Enabled = false;
            }
        }
     }
    
}
