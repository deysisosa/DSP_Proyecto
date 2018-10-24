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
    public partial class Proveedores : Form
    {
        Conexion cn = new Conexion();
        public Proveedores()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtProveedor.Enabled = true;
            txtContacto.Enabled = true;
            txtPais.Enabled = true;
            txtT1.Enabled = true;
            txtT2.Enabled = true;
            txtC1.Enabled = true;
            txtC2.Enabled = true;
            btbAgregar.Enabled = true;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            limpiar();
        
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            txtProveedor.Enabled = false;
            txtContacto.Enabled = false;
            txtPais.Enabled = false;
            txtT1.Enabled = false;
            txtT2.Enabled = false;
            txtC1.Enabled = false;
            txtC2.Enabled = false;
            btbAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            cargardatos();
        }
        public void cargardatos()
        {
            tablaP.AllowUserToAddRows = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select * from Proveedores", cn.conex());
            datos.Fill(tabla);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = tabla;
            tabla.Columns[0].ColumnName = "ID Proveedor";
            tabla.Columns[1].ColumnName = "Proveedor";
            tabla.Columns[2].ColumnName = "Nombre Contacto";
            tabla.Columns[3].ColumnName = "Pais";
            tabla.Columns[4].ColumnName = "Telefono 1";
            tabla.Columns[5].ColumnName = "Telefono 2";
            tabla.Columns[6].ColumnName = "Correo 1";
            tabla.Columns[7].ColumnName = "Correo 2";
            tablaP.DataSource = bSource;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tablaP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnNuevo.Enabled = false;
            btnNuevo.Enabled = true;
            txtProveedor.Enabled = true;
            txtContacto.Enabled = true;
            txtPais.Enabled = true;
            txtT1.Enabled = true;
            txtT2.Enabled = true;
            txtC1.Enabled = true;
            txtC2.Enabled = true;
            btnNuevo.Enabled = true;
            btbAgregar.Enabled = false;
            txtID.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[0].Value);
            txtProveedor.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[1].Value);
            txtContacto.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[2].Value);
            txtPais.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[3].Value);
            txtT1.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[4].Value);
            txtT2.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[5].Value);
            txtC1.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[6].Value);
            txtC2.Text = Convert.ToString(this.tablaP.CurrentRow.Cells[7].Value);
            
        }
        private void limpiar()
        {
            txtID.Clear();
            txtProveedor.Clear();
            txtContacto.Clear();
            txtPais.Clear();
            txtT1.Clear();
            txtT2.Clear();
            txtC1.Clear();
            txtC2.Clear();
        }

        public bool Existe(string nombre)
        {
            {
                string consulta = "SELECT COUNT(*) FROM proveedores WHERE Nombre_Proveedor=@nombre";
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
        private void btbAgregar_Click(object sender, EventArgs e)
        {
            if (txtProveedor.Text != "" & txtContacto.Text!="" & txtPais.Text!="" & txtT1.Text!="" & txtC1.Text!="")
            {
                try
                {
                    if (!Existe(txtProveedor.Text))
                    {
                        SqlCommand cm = new SqlCommand();
                        cm.CommandText = "insert into Proveedores values('" + txtProveedor.Text + "','"+txtContacto.Text+"','"+txtPais.Text+"','"+txtT1.Text+"','"+txtT2.Text+"','"+txtC1.Text+"','"+txtC2.Text+"')";
                        cm.Connection = cn.conex();
                        cm.ExecuteNonQuery();
                        limpiar();
                        txtProveedor.Enabled = false;
                        txtContacto.Enabled = false;
                        txtPais.Enabled = false;
                        txtT1.Enabled = false;
                        txtT2.Enabled = false;
                        txtC1.Enabled = false;
                        txtC2.Enabled = false;
                        btbAgregar.Enabled = false;
                        cargardatos();
                        cn.desconectar();
                        btnNuevo.Enabled = true;
                        MessageBox.Show("El proveedor ha sido registrado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("El proveedor que intenta ingresar ya existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else { 
                MessageBox.Show("Debe llenar los datos, en datos de contacto es necesario un telefono y correo", "¡ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "delete from Proveedores where id_proveedor='" + txtID.Text + "'";
                SqlCommand cm = new SqlCommand(consulta, cn.conex());
                cm.ExecuteNonQuery();
                limpiar();
                cn.desconectar();
                txtProveedor.Enabled = false;
                txtContacto.Enabled = false;
                txtPais.Enabled = false;
                txtT1.Enabled = false;
                txtT2.Enabled = false;
                txtC1.Enabled = false;
                txtC2.Enabled = false;
                cargardatos();
                MessageBox.Show("Registro eliminado correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
            }
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Actualizar datos de proveedor?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                cn.conectar();
                String consulta = "update Proveedores set nombre_Proveedor='" + txtProveedor.Text + "', Nombre_Contacto='"+txtContacto.Text+"', Pais='"+txtPais.Text+"', telefono1='"+txtT1.Text+"', telefono2='"+txtT2.Text+"', correo1='"+txtC1.Text+"', correo2='"+txtC2.Text+"' where id_proveedor='" + txtID.Text + "'";
                SqlCommand cm = new SqlCommand(consulta, cn.conex());
                cm.ExecuteNonQuery();
                cn.desconectar();
                MessageBox.Show("Proveedor actualizado correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProveedor.Enabled = false;
                txtContacto.Enabled = false;
                txtPais.Enabled = false;
                txtT1.Enabled = false;
                txtT2.Enabled = false;
                txtC1.Enabled = false;
                txtC2.Enabled = false;
                cargardatos();
                limpiar();
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }
    }
}
