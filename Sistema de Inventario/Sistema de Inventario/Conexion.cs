using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Inventario
{
    class Conexion
    {
        SqlConnection conexion = new SqlConnection("Server=LENOVO\\SQL ; database=inventario ; integrated security = true");

        public void conectar()
        {
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void desconectar()
        {
            conexion.Close();
        }

        public SqlConnection conex()
        {
            SqlConnection conexi = conexion;
            return conexi;
        }

    
    }
   
}
