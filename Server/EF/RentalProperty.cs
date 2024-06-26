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
    
    public partial class RentalProperty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RentalProperty()
        {
            this.PropertyTransaction = new HashSet<PropertyTransaction>();
        }
    
        public int IdRentalProperty { get; set; }
        public int IdProperty { get; set; }
        public Nullable<int> IdClientOwner { get; set; }
        public Nullable<int> IdRealtor { get; set; }
        public decimal Rent { get; set; }
        public int IdPaymentPeriod { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual PaymentPeriod PaymentPeriod { get; set; }
        public virtual Property Property { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyTransaction> PropertyTransaction { get; set; }
        public virtual Realtor Realtor { get; set; }
    }
}
