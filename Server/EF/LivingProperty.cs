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
    
    public partial class LivingProperty
    {
        public int IdLivingProperty { get; set; }
        public int IdProperty { get; set; }
        public int IdLivingPropertyType { get; set; }
        public decimal SharedAreaSquareMeters { get; set; }
        public decimal LivingAreaSquareMeters { get; set; }
        public decimal KitchenAreaSquareMeters { get; set; }
        public int RoomQuantity { get; set; }
        public decimal CeilingHeight { get; set; }
        public Nullable<int> FloorNumber { get; set; }
        public int FloorsAmount { get; set; }
        public bool HasHeating { get; set; }
        public bool HasParking { get; set; }
        public byte BathroomQuantity { get; set; }
    
        public virtual LivingPropertyType LivingPropertyType { get; set; }
        public virtual Property Property { get; set; }
    }
}
