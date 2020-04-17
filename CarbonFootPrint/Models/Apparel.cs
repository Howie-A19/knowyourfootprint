//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarbonFootPrint.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Apparel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apparel()
        {
            this.food_apparels_cloth_recycle_tips = new HashSet<food_apparels_cloth_recycle_tips>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Material_One { get; set; }
        public string Material_Two { get; set; }
        public Nullable<float> Materail_One_Usage { get; set; }
        public Nullable<float> Material_Two_Usage { get; set; }
        public float Total_Carbon_Footprint { get; set; }
        public string Image_Path { get; set; }
        public string Suggestions { get; set; }
        public int Category_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<food_apparels_cloth_recycle_tips> food_apparels_cloth_recycle_tips { get; set; }
        public virtual Category Category { get; set; }
    }
}