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
    
    public partial class Chauffeur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chauffeur()
        {
            this.AffectationVehicules = new HashSet<AffectationVehicule>();
        }
    
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string NomChauffeur { get; set; }
        public string PrenomChauffeur { get; set; }
        public string AdresseChauffeur { get; set; }
        public string Telephone1Chauffeur { get; set; }
        public string Telephone2Chauffeur { get; set; }
        public string PhotoChauffeur { get; set; }
        public Nullable<System.DateTime> DateEmbauche { get; set; }
        public string PersonneAPrevenir { get; set; }
        public bool Supprime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AffectationVehicule> AffectationVehicules { get; set; }
    }
}
