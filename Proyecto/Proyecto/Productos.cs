//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto
{
    using System;
    using System.Collections.Generic;
    
    public partial class Productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productos()
        {
            this.detalle_productos = new HashSet<detalle_productos>();
        }
    
        public string id_producto { get; set; }
        public string Nombre_producto { get; set; }
        public Nullable<int> Existencias { get; set; }
        public Nullable<double> Precio_venta { get; set; }
        public Nullable<double> Precio_compra { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public Nullable<int> N_pedido { get; set; }
        public Nullable<int> id_proveedor { get; set; }
        public Nullable<int> ubicacion { get; set; }
        public Nullable<int> estado { get; set; }
    
        public virtual bodega bodega { get; set; }
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_productos> detalle_productos { get; set; }
        public virtual estado estado1 { get; set; }
        public virtual Proveedores Proveedores { get; set; }
    }
}
