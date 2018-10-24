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
    public partial class Bodega : Form
    {
        Conexion cn = new Conexion();
        public Bodega()
        {
            InitializeComponent();
        }

        private void Bodega_Load(object sender, EventArgs e)
        {
            cargardatos();
            txtNombre.Enabled = false;
            comboBox1.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            Agregar.Enabled = false;
            DataTable dt = Estantes();

            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "id_estante";
            comboBox1.DisplayMember = "n_estante";
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            Close();
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
        private DataTable Estantes()
        {
            TablaB.AllowUserToAddRows = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select id_estante,n_estante from estante", cn.conex());
            datos.Fill(tabla);
            return tabla;
        }
        public void cargardatos()
        {
            TablaB.AllowUserToAddRows = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter(" select b.id_bodega,b.Nombre_Bodega,e.n_estante from bodega b inner join estante e on b.id_estante=e.id_estante", cn.conex());
            datos.Fill(tabla);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = tabla;
            tabla.Columns[0].ColumnName = "ID Bodega";
            tabla.Columns[1].ColumnName = "Nombre";
            tabla.Columns[2].ColumnName = "Estante";
            txtNombre.Enabled = false;
            Agregar.Enabled = false;
            comboBox1.Enabled = false;
            TablaB.DataSource = bSource;

        }

        private void TablaB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = true;
            Agregar.Enabled = false;
            comboBox1.Enabled = true;
            id.Text = Convert.ToString(this.TablaB.CurrentRow.Cells[0].Value);
            txtNombre.Text = Convert.ToString(this.TablaB.CurrentRow.Cells[1].Value);
            comboBox1.Text = Convert.ToString(this.TablaB.CurrentRow.Cells[2].Value);
        }

        public bool Existe(string nombre)
        {
            {
                string consulta = "SELECT COUNT(*) FROM bodega WHERE Nombre_Bodega=@nombre";
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
                        cm.CommandText = "insert into bodega values('" + txtNombre.Text + "','" + comboBox1.SelectedValue + "')";
                        cm.Connection = cn.conex();
                        cm.ExecuteNonQuery();
                        txtNombre.Clear();
                        cargardatos();
                        cn.desconectar();
                        txtNombre.Enabled = false;
                        Agregar.Enabled = false;
                        btnNuevo.Enabled = true;
                        MessageBox.Show("La bodega ha sido registrada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     


                    }
                    else
                    {
                        MessageBox.Show("La bodega ya se encuentra registrada", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cn.desconectar();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;

                }
            }
            else { MessageBox.Show("Debe ingresar el nombre de la bodega y seleccionar un estante", "¡ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Actualizar datos de bodega?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "update bodega set Nombre_Bodega='" + txtNombre.Text + "',id_estante='" + comboBox1.SelectedValue + "' where id_bodega='" + id.Text + "'";
                SqlCommand cm = new SqlCommand(consulta, cn.conex());
                cm.ExecuteNonQuery();
                cn.desconectar();
                cargardatos();
                txtNombre.Clear();
                id.Clear();
                comboBox1.ResetText();
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                MessageBox.Show("Bodega actualizada correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "delete from bodega where id_bodega='" + id.Text + "'";
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
