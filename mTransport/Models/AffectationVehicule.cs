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
    
    public partial class AffectationVehicule
    {
        public int Id { get; set; }
        public int IdVehicule { get; set; }
        public int IdChauffeur { get; set; }
        public System.DateTime DateDebutAffectation { get; set; }
        public Nullable<System.DateTime> DateFinAffectation { get; set; }
        public bool Supprime { get; set; }
    
        public virtual Chauffeur Chauffeur { get; set; }
        public virtual Vehicule Vehicule { get; set; }
    }
}