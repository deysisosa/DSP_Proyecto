//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class detalle_productos
    {
        public int id_detalle_producto { get; set; }
        public string id_producto { get; set; }
        public string Nombre_producto { get; set; }
        public Nullable<int> Existencia { get; set; }
        public Nullable<double> Precio_venta { get; set; }
        public Nullable<double> Precio_compra { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public Nullable<int> N_pedido { get; set; }
        public Nullable<int> id_proveedor { get; set; }
    
        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }
    }
}