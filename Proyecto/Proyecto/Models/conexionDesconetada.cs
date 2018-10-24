using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Proyecto.Models
{
    public class conexionDesconetada
    {
        //Declaramos loss objetos que utilizaremos en toda nuestra clase
        string coneccionString;
        SqlConnection conexionSQL;
        public DataSet DataSetPrincipal;
        SqlDataAdapter DataAdapterEspecífico;
        public conexionDesconetada()
        {
            //Configuramos la conexion y obtenemos la cadena de conexion desde el Web.config
            coneccionString =
           ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            conexionSQL = new SqlConnection(coneccionString);
            DataSetPrincipal = new DataSet();
            conexionSQL.Open();
            /*Inicializamos el adaptador específico que se utilizará en la gestión de
            registros de producto*/
            DataAdapterEspecífico = new SqlDataAdapter();
            DataAdapterEspecífico.SelectCommand = new SqlCommand("SELECT * FROM Products",
           conexionSQL);
        }
        public void conectar()
        {
            conexionSQL.Open();
        }
        public void desconectar()
        {
            conexionSQL.Close();
        }
        public void copiarBaseDatos()
        {
            //Cargamos las tablas al dataset a través del método cargarTabla
            cargarTabla("Categoria", "id_categoria");
            cargarTabla("estante", "id_estante");
            cargarTabla("bodega", "id_bodega");
            cargarTabla("Productos", "id_producto");
            cargarTabla("estado", "id_estado");
            cargarTabla("Proveedores", "id_proveedor");
            cargarTabla("detalle_productos", "id_detalle_producto");
            cargarTabla("pedido", "id_pedido");
            cargarTabla("Usuarios", "Nombre_Usuario");
            cargarTabla("Tipo_Usuario", "id_tipo");
            /*Establecemos la relacion de llaves foraneas entre las tablas,
            verifique la sintaxis de cada una de ellas*/
            ForeignKeyConstraint fk1_cat_pro;
            fk1_cat_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Categoria"].Columns["id_categoria"],
           DataSetPrincipal.Tables["Productos"].Columns["id_categoria"]);
            DataSetPrincipal.Tables["Productos"].Constraints.Add(fk1_cat_pro);
            ForeignKeyConstraint fk1_sup_pro;
            fk1_cat_est = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Suppliers"].Columns["SupplierID"],
           DataSetPrincipal.Tables["Products"].Columns["SupplierID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_sup_pro);
            fk1_cat_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Categories"].Columns["CategoryID"],
           DataSetPrincipal.Tables["Products"].Columns["CategoryID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_cat_pro);
            ForeignKeyConstraint fk1_sup_pro;
            fk1_sup_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Suppliers"].Columns["SupplierID"],
           DataSetPrincipal.Tables["Products"].Columns["SupplierID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_sup_pro);
            fk1_cat_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Categories"].Columns["CategoryID"],
           DataSetPrincipal.Tables["Products"].Columns["CategoryID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_cat_pro);
            ForeignKeyConstraint fk1_sup_pro;
            fk1_sup_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Suppliers"].Columns["SupplierID"],
           DataSetPrincipal.Tables["Products"].Columns["SupplierID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_sup_pro);
            fk1_cat_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Categories"].Columns["CategoryID"],
           DataSetPrincipal.Tables["Products"].Columns["CategoryID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_cat_pro);
            ForeignKeyConstraint fk1_sup_pro;
            fk1_sup_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Suppliers"].Columns["SupplierID"],
           DataSetPrincipal.Tables["Products"].Columns["SupplierID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_sup_pro);
            fk1_cat_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Categories"].Columns["CategoryID"],
           DataSetPrincipal.Tables["Products"].Columns["CategoryID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_cat_pro);
            ForeignKeyConstraint fk1_sup_pro;
            fk1_sup_pro = new
           ForeignKeyConstraint(DataSetPrincipal.Tables["Suppliers"].Columns["SupplierID"],
           DataSetPrincipal.Tables["Products"].Columns["SupplierID"]);
            DataSetPrincipal.Tables["Products"].Constraints.Add(fk1_sup_pro);
        }
        public void cargarTabla(String nombreTabla, String primaryKey)
        {
            nombreTabla = nombreTabla.Trim();
            //Si la tabla no existe en el contexto actual, procederemos a adicionarla
            if (DataSetPrincipal.Tables.IndexOf(nombreTabla) == -1)
            {
                /*Configuramos un control dataAdapter para que funcione como puente entre
                dataset y base de datos */
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + nombreTabla;
                dataAdapter.SelectCommand.Connection = conexionSQL;
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                //Populamos el dataset con la tabla que acabamos de obtener
                dataAdapter.FillSchema(DataSetPrincipal, SchemaType.Source, nombreTabla);
                dataAdapter.Fill(DataSetPrincipal, nombreTabla);
                dataAdapter.SelectCommand.Dispose();
                dataAdapter.Dispose();
                //Obtenemos la tabla actual y establecemos el campo que se utilizará como llave primaria
            DataTable tablaActual;
                tablaActual = DataSetPrincipal.Tables[nombreTabla];
                DataColumn[] columnasPrincipales = new DataColumn[1];
                columnasPrincipales[0] = tablaActual.Columns[primaryKey];
                tablaActual.PrimaryKey = columnasPrincipales;
            }
        }
        public int insertarProducto(DataSet ds, string Categorias, string Proveedores,
       string ProductName, string QuantityPerUnit, string UnitPrice, string UnitsInStock, string
       UnitsOnOrder)
        {
            DataTable table = ds.Tables["Products"];
            DataRow row = table.NewRow();
            row["ProductName"] = ProductName;
            row["SupplierID"] = Proveedores;
            row["CategoryID"] = Categorias;
            row["QuantityPerUnit"] = QuantityPerUnit;
            row["UnitsInStock"] = UnitsInStock;
            row["UnitPrice"] = UnitPrice;
            row["UnitsOnOrder"] = UnitsOnOrder;
            table.Rows.Add(row);
            if (ds.HasChanges())
            {
                SqlCommandBuilder comando = new SqlCommandBuilder(DataAdapterEspecífico);
                int filasAfectadas = DataAdapterEspecífico.Update(ds, "Products");
                ds.AcceptChanges();
                return filasAfectadas;
            }
            return 0;
        }
        public Product mostrardatos(int productID, DataSet ds)
        {
            DataView vistaFiltro = new DataView(ds.Tables["Products"]);
            vistaFiltro.RowFilter = "ProductID = " + productID;
            Product itemproduct = new Product();
            foreach (DataRowView dr in vistaFiltro)
            {
                itemproduct.ProductID = dr["ProductID"].ToString();
                itemproduct.ProductName = dr["ProductName"].ToString();
                itemproduct.SupplierID = dr["SupplierID"].ToString();
                itemproduct.CategoryID = dr["CategoryID"].ToString();
                itemproduct.QuantityPerUnit = dr["QuantityPerUnit"].ToString();
                itemproduct.UnitPrice = dr["UnitPrice"].ToString();
                itemproduct.UnitsInStock = dr["UnitsInStock"].ToString();
                itemproduct.UnitsOnOrder = dr["UnitsOnOrder"].ToString();
            }
            return itemproduct;
        }
        public int actualizarProducto(DataSet ds, int ProductID, string Categorias, string
       Proveedores, string ProductName, string QuantityPerUnit, string UnitPrice, string
       UnitsInStock, string UnitsOnOrder)
        {
            DataRow fila2Update;
            DataTable tablaProductos;
            tablaProductos = ds.Tables["Products"];
            try
            {
                fila2Update = tablaProductos.Rows.Find(ProductID);
                fila2Update["ProductName"] = ProductName;
                fila2Update["SupplierID"] = int.Parse(Proveedores);
                fila2Update["CategoryID"] = int.Parse(Categorias);
                fila2Update["QuantityPerUnit"] = QuantityPerUnit;
                fila2Update["UnitPrice"] = double.Parse(UnitPrice);
                fila2Update["UnitsInStock"] = int.Parse(UnitsInStock);
                fila2Update["UnitsOnOrder"] = int.Parse(UnitsOnOrder);
                if (DataSetPrincipal.HasChanges())
                {
                    SqlCommandBuilder comando = new
                   SqlCommandBuilder(DataAdapterEspecífico);
                    int filasAfectadas = DataAdapterEspecífico.Update(DataSetPrincipal,
                   "Products");
                    DataSetPrincipal.AcceptChanges();
                    return filasAfectadas;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int eliminarProducto(DataSet ds, int ProductID)
        {
            DataRow fila2Delete;
            DataTable tablaProductos;
            tablaProductos = ds.Tables["Products"];
            try
            {
                fila2Delete = tablaProductos.Rows.Find(ProductID);
                fila2Delete.Delete();
                if (DataSetPrincipal.HasChanges())
                {
                    SqlCommandBuilder comando = new
                   SqlCommandBuilder(DataAdapterEspecífico);
                    int filasAfectadas = DataAdapterEspecífico.Update(ds, "Products");
                    ds.AcceptChanges();
                    return filasAfectadas;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}