//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mTransport.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Materiel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materiel()
        {
            this.HistoriquePannes = new HashSet<HistoriquePanne>();
        }
    
        public int Id { get; set; }
        public string Designation { get; set; }
        public Nullable<decimal> QteStock { get; set; }
        public string Etat { get; set; }
        public Nullable<System.DateTime> DatePeremption { get; set; }
        public bool Supprime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriquePanne> HistoriquePannes { get; set; }
    }
}
