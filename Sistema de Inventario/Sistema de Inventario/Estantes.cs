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
    public partial class Estantes : Form
    {
        Conexion cn = new Conexion();
        public Estantes()
        {
            InitializeComponent();

        }

        private void Estantes_Load(object sender, EventArgs e)
        {
            cargardatos();
            
            txtNombre.Enabled = false;
            comboBox1.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            Agregar.Enabled = false;
            DataTable dt = Categorias();
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "id_categoria";
            comboBox1.DisplayMember = "Nombre_categoria";
        }
        public void cargardatos()
        {
            TablaE.AllowUserToAddRows = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter(" select e.id_estante,e.n_estante,c.Nombre_categoria from estante e inner join Categoria c on e.id_categoria=c.id_categoria", cn.conex());
            datos.Fill(tabla);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = tabla;
            tabla.Columns[0].ColumnName = "ID Estante";
            tabla.Columns[1].ColumnName = "Nombre";
            tabla.Columns[2].ColumnName = "Categoria";
            txtNombre.Enabled = false;
            Agregar.Enabled = false;
            comboBox1.Enabled = false;
            TablaE.DataSource = bSource;

        }
        private void Cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private DataTable Categorias()
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select id_categoria,nombre_categoria from categoria", cn.conex());
            datos.Fill(tabla);
            return tabla;
        }
      
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            txtNombre.Enabled = true;
            comboBox1.Enabled = true;
            Agregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            id.Clear();
            txtNombre.Clear();
            comboBox1.ResetText();
            
        }
        public bool Existe(string nombre)
        {
            {
                string consulta = "SELECT COUNT(*) FROM estante WHERE n_estante=@nombre";
                SqlCommand cmd = new SqlCommand(consulta, cn.conex());
                cmd.Parameters.AddWithValue("@nombre", nombre);
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
            if (txtNombre.Text != "" & comboBox1.Text !="")
            {
                try
                {
                    if (!Existe(txtNombre.Text))
                    {
                       
                        SqlCommand cm = new SqlCommand();
                        cm.CommandText = "insert into estante values('" + txtNombre.Text + "','" + comboBox1.SelectedValue + "')";
                        cm.Connection = cn.conex();
                        cm.ExecuteNonQuery();
                        txtNombre.Clear();
                        cargardatos();
                        cn.desconectar();
                        txtNombre.Enabled = false;
                        Agregar.Enabled = false;
                        btnNuevo.Enabled = true;
                        MessageBox.Show("El estante ha sido registrado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El estante ya se encuentra registrado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cn.desconectar();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                    
                }
            }
            else { MessageBox.Show("Debe ingresar el nombre del estante y seleccionar una categoria", "¡ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            
        }

        private void TablaE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = true;
            Agregar.Enabled = false;
            comboBox1.Enabled = true;
            id.Text = Convert.ToString(this.TablaE.CurrentRow.Cells[0].Value);
            txtNombre.Text = Convert.ToString(this.TablaE.CurrentRow.Cells[1].Value);
            comboBox1.Text = Convert.ToString(this.TablaE.CurrentRow.Cells[2].Value);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Actualizar datos de estante?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "update estante set n_estante='" + txtNombre.Text + "',id_categoria='"+comboBox1.SelectedValue+"' where id_estante='" + id.Text + "'";
                SqlCommand cm = new SqlCommand(consulta, cn.conex());
                cm.ExecuteNonQuery();
                cn.desconectar();
                cargardatos();
                txtNombre.Clear();
                id.Clear();
                comboBox1.ResetText();
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                MessageBox.Show("Estante actualizado correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);               

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "delete from estante where id_estante='" + id.Text + "'";
                SqlCommand cm = new SqlCommand(consulta, cn.conex());
                cm.ExecuteNonQuery();
                cn.desconectar();
                cargardatos();
                txtNombre.Clear();
                id.Clear();
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                comboBox1.ResetText();
                MessageBox.Show("Registro eliminado correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
        }
    }
}
