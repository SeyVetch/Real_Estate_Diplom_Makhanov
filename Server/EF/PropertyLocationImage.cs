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
    
    public partial class PropertyLocationImage
    {
        public int IdPropertyLocationImage { get; set; }
        public int IdPropertyLocation { get; set; }
        public string Image { get; set; }
    
        public virtual PropertyLocation PropertyLocation { get; set; }
    }
}
