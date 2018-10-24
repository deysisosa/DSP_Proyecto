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
    public partial class Productos : Form
    {
        Conexion cn = new Conexion();
        public Productos()
        {
            InitializeComponent();

            DataTable dt1 = Categorias();
            cbCategoria.DataSource = dt1;
            cbCategoria.ValueMember = "id_categoria";
            cbCategoria.DisplayMember = "Nombre_categoria";

            DataTable dt2 = Proveedores();
            cbProveedor.DataSource = dt2;
            cbProveedor.ValueMember = "id_proveedor";
            cbProveedor.DisplayMember = "Nombre_Proveedor";

            DataTable dt3 = Ubicacion();
            cbUbicacion.DataSource = dt3;
            cbUbicacion.ValueMember = "id_bodega";
            cbUbicacion.DisplayMember = "Nombre_Bodega";

            DataTable dt4 = Estado();
            cbEstado.DataSource = dt4;
            cbEstado.ValueMember = "id_estado";
            cbEstado.DisplayMember = "Estado";

        }

        private void txtContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            txtNombre.Enabled = false;
            txtExistencias.Enabled = false;
            txtPrecio1.Enabled = false;
            txtPrecio2.Enabled = false;
            txtDescrip.Enabled = false;
            cbCategoria.Enabled = false;
            txtPedido.Enabled = false;
            cbProveedor.Enabled = false;
            cbUbicacion.Enabled = false;
            cbEstado.Enabled = false;
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            cargardatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtExistencias.Enabled = true;
            txtPrecio1.Enabled = true;
            txtPrecio2.Enabled = true;
            txtDescrip.Enabled = true;
            cbCategoria.Enabled = true;
            txtPedido.Enabled = true;
            cbProveedor.Enabled = true;
            cbUbicacion.Enabled = true;
            cbEstado.Enabled = true;
            btnAgregar.Enabled = true;
            btnNuevo.Enabled = false;
            cbCategoria.ResetText();
            cbProveedor.ResetText();
            cbUbicacion.ResetText();
            cbEstado.ResetText();
        }
        private DataTable Categorias()
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select id_categoria,nombre_categoria from categoria", cn.conex());
            datos.Fill(tabla);
            return tabla;
        }
        private DataTable Proveedores()
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select id_proveedor,Nombre_Proveedor from proveedores", cn.conex());
            datos.Fill(tabla);
            return tabla;
        }
        private DataTable Ubicacion()
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select id_bodega,Nombre_Bodega from bodega", cn.conex());
            datos.Fill(tabla);
            return tabla;
        }
        private DataTable Estado()
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select Id_Estado,Estado from estado", cn.conex());
            datos.Fill(tabla);
            return tabla;
        }

        private void txtPrecio1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPrecio2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public void cargardatos()
        {
            tablaP.AllowUserToAddRows = false;
            DataTable tabla = new DataTable();
            SqlDataAdapter datos = new SqlDataAdapter("select p.id_producto,p.Nombre_producto,p.Existencias,p.Precio_venta,p.Precio_Compra,p.Descripcion,c.Nombre_categoria,p.N_pedido,pr.Nombre_Proveedor,b.Nombre_Bodega,e.Estado from Productos p inner join Categoria c on p.id_categoria=c.id_categoria inner join Proveedores pr on p.id_proveedor=pr.id_proveedor inner join bodega b on p.ubicacion=b.id_bodega inner join estado e on p.estado=e.Id_Estado", cn.conex());
            datos.Fill(tabla);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = tabla;
            tabla.Columns[0].ColumnName = "ID Producto";
            tabla.Columns[1].ColumnName = "Nombre";
            tabla.Columns[2].ColumnName = "Existencias";
            tabla.Columns[3].ColumnName = "Precio Venta";
            tabla.Columns[4].ColumnName = "Precio Compra";
            tabla.Columns[5].ColumnName = "Descripcion";
            tabla.Columns[6].ColumnName = "Categoria";
            tabla.Columns[7].ColumnName = "Pedido";
            tabla.Columns[8].ColumnName = "Proveedor";
            tabla.Columns[9].ColumnName = "Ubicacion";
            tabla.Columns[10].ColumnName = "Estado";
            tablaP.DataSource = bSource;

        }
        public bool Existe(string nombre)
        {
            {
                string consulta = "SELECT COUNT(*) FROM productos WHERE Nombre_producto=@nombre";
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
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" & txtExistencias.Text !="" & txtPrecio1.Text !="" & txtPrecio2.Text !="" & txtDescrip.Text !="" & cbCategoria.Text !="" & txtPedido.Text!="" & cbUbicacion.Text!="" & cbEstado.Text!="")
            {
                try
                {
                    if (!Existe(txtNombre.Text))
                    {

                        SqlCommand cm = new SqlCommand();
                        cm.CommandText = "insert into productos values('" + txtNombre.Text + "','" + txtExistencias.Text + "','" + txtPrecio1.Text + "','" + txtPrecio2.Text + "','" + txtDescrip.Text + "','" + cbCategoria.SelectedValue + "','" + txtPedido.Text + "','" + cbProveedor.SelectedValue + "','" + cbUbicacion.SelectedValue + "','" + cbEstado.SelectedValue + "')";
                        cm.Connection = cn.conex();
                        cm.ExecuteNonQuery();
                        txtNombre.Clear();
                        cargardatos();
                        cn.desconectar();
                        txtNombre.Enabled = false;
                        txtExistencias.Enabled = false;
                        txtPrecio1.Enabled = false;
                        txtPrecio2.Enabled = false;
                        txtDescrip.Enabled = false;
                        cbCategoria.Enabled = false;
                        txtPedido.Enabled = false;
                        cbProveedor.Enabled = false;
                        cbUbicacion.Enabled = false;
                        cbEstado.Enabled = false;
                        btnAgregar.Enabled = false;
                        btnNuevo.Enabled = true;
                        MessageBox.Show("El producto ha sido registrado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El producto ya existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cn.desconectar();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;

                }
            }
            else { MessageBox.Show("Debe Completar todos los campos", "¡ATENCION!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }
    }
}
