//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stagiaire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stagiaire()
        {
            this.AbsenceS = new HashSet<Absence>();
            this.Evaluations = new HashSet<Evaluation>();
        }
    
        public string CIN { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string CNE { get; set; }
        public Nullable<System.DateTime> Date_Naissance { get; set; }
        public string Numero_telephone { get; set; }
        public string Adresse { get; set; }
        public Nullable<int> Idfil { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Absence> AbsenceS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual Filiere Filiere { get; set; }
    }
}
