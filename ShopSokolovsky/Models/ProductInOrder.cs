//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopSokolovsky.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductInOrder
    {
        public int idProduct { get; set; }
        public int idOrder { get; set; }
        public int amountProduct { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
