//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class PropertyTransaction
    {
        public int IdPropertyTransaction { get; set; }
        public int IdClientBuyer { get; set; }
        public Nullable<int> IdPurchasableProperty { get; set; }
        public Nullable<int> IdRentalProperty { get; set; }
        public bool IsMortgaged { get; set; }
        public System.DateTime DateClosure { get; set; }
        public Nullable<int> IdAgent { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Client Client { get; set; }
        public virtual PurchasableProperty PurchasableProperty { get; set; }
        public virtual RentalProperty RentalProperty { get; set; }
    }
}
